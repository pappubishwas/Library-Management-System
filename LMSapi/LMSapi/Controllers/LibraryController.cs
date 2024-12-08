using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMSapi.Models;
using Microsoft.VisualBasic;
namespace LMSapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        public LibraryController(Context context, EmailService emailService, JwtService jwtService)
        {
            Context = context;
            EmailService = emailService;
            JwtService = jwtService;
        }

        public Context Context { get; }
        public EmailService EmailService { get; }
        public JwtService JwtService { get; }

        [HttpGet("GetUser")]
        public ActionResult GetUser()
        {
            var userList = Context.Users.Select(u => new { u.Username, u.Email }).ToList();
            return Ok( userList );
        }


        [HttpPost("Membership")]
        public ActionResult Membership(Member member)
        {
            

            if (member != null)
            {
     
                

                Context.Members.Add(member);
                Context.SaveChanges();
                return Ok("Your Membership request sent to admin");
            }

            return BadRequest("Bad Request");
        }



        [HttpPost("Register")]
        public ActionResult Register(User user, EmailService emailService)
        {         
            user.IsActive = true;
            user.Role = Role.Guest;
            user.CreatedOn = DateTime.Now;

            Context.Users.Add(user);
            Context.SaveChanges();

            const string subject = "Account Created";

            var body = $"""
<html>
    <body>
        <h1>Hello, {user.FirstName} {user.LastName}</h1>
        <h2>
            Your account has been created. <strong>UserName:</strong> {user.Username}. 
            You can log in to your account using your Gmail or username.
        </h2>
        <p>
            Your account has been created with a default role of <strong>Guest</strong>. 
            To access the services of our library, you need to upgrade to a membership.
        </p>
        <h3>Thank you!</h3>
    </body>
</html>
""";

            EmailService.SendEmail(user.Email, subject, body);
            int userId = Context.Users.Where(u => u.Username == user.Username).Select(u => u.Id).FirstOrDefault();

            SendNotification(userId, "Account Created", "Your account has been Created. For access library facilities get the membership.");
            Context.SaveChanges();
            return Ok(@"Thank you for registering. 
                        Your account has been created. 
                        you can login by email or username.");
        }

        [HttpGet("Login")]
        public ActionResult Login(string email, string password)
        {
            
            if (Context.Users.Any(u => (u.Email.Equals(email) || u.Username.Equals(email)) && u.Password.Equals(password)))
            {
                var user = Context.Users.Single(user => (user.Email.Equals(email) || user.Username.Equals(email))  && user.Password.Equals(password));

                if (!user.IsActive)
                {
                    return Ok("Inactive");
                }

                return Ok(JwtService.GenerateToken(user));
            }
            return Ok("not found");
        }


        [Authorize]
        [HttpGet("GetBooks")]
        public ActionResult GetBooks()
        {
            if (Context.Books.Any())
            {
                return Ok(Context.Books.Include(b => b.BookCategory).ToList());
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet("GetMember")]
        public ActionResult<IEnumerable<Member>> GetMember(int id)
        {
            var member = Context.Members.Where(m => m.userId == id).ToList();
            if (member.Any())
            {
                return Ok(member);
            }
            return NotFound("No member found.");
        }


        [Authorize]
        [HttpPost("OrderBook")]
        public ActionResult OrderBook(int userId, int bookId)
        {
            Console.WriteLine(userId + " " + bookId);
            // Find the member based on the userId
            var member = Context.Members.SingleOrDefault(m => m.userId == userId);
            if (member == null)
            {
                return Ok("Member not found.");
            }
            if (member.Status == "Inactive")
            {
                return Ok("Your membership is not active.Kindly contact to the admin!");
            }

            // Check the borrowing limit
            int maxBorrowLimit = member.MembershipType == "Premium" ? 6 : 3; 
            var borrowedCount = Context.BorrowedBooks.Count(o => o.MemberId == member.MemberId && o.Status == BorrowedBookStatus.Borrowed);

            if (borrowedCount >= maxBorrowLimit)
            {
                return Ok("Borrowing limit reached.");
            }

            // Add a new entry in BorrowedBooks
            var borrowedBook = new BorrowedBook
            {
                BookId = bookId,
                MemberId = member.MemberId,
                BorrowDate = DateTime.Now,
                DueDate = member.MembershipType == "Premium"? DateTime.Now.AddDays(15): DateTime.Now.AddDays(10), 
                ReturnDate = null,
                LateFee = 0,
                Status = BorrowedBookStatus.Borrowed,
            };

            Context.BorrowedBooks.Add(borrowedBook);

            // Update the book's status to 'Borrowed'
            var book = Context.Books.Find(bookId);
            if (book == null)
            {
                return Ok("Book not found.");
            }

            if (book.Status == BookStatus.Borrowed)
            {
                return Ok("Book is already borrowed.");
            }
            book.AvailableCopies =book.AvailableCopies - 1;
            if (book.AvailableCopies < 1)
            {
                book.Status = BookStatus.Borrowed;
            }

            Context.SaveChanges();
            return Ok("Book borrowed successfully.");
        }

        [Authorize]
        [HttpGet("GetBorrowedBookListOfUser")]
        public ActionResult GetBorrowedBookListOfUser(int userId)
        {
            // Check if the member exists
            var member = Context.Members.SingleOrDefault(u => u.userId == userId);
            if (member == null)
            {
                return NotFound($"Membership not found for userId: {userId}");
            }

            // Get the list of BookIds for the member's borrowed books
            var bookIds = Context.BorrowedBooks
                .Where(o => o.MemberId == member.MemberId && o.Status==BorrowedBookStatus.Borrowed)
                .Select(o => o.BookId)
                .ToList();
            var reserveBookIds = Context.Reservations
                  .Where(o => o.MemberId == member.MemberId && o.Status == ReservationStatus.Pending)
                  .Select(o => o.BookId)
                  .ToList();
            var combinedBookIds = bookIds.Concat(reserveBookIds).ToList();
            return Ok(combinedBookIds);
        }

        [Authorize]
        [HttpGet("GetAllMembers")]
        public ActionResult GetAllMembers()
        {
            // Filter and fetch inactive members directly
            var inactiveMembers = Context.Members
                                          .Where(m => m.Status == "Inactive")
                                          .ToList();

            if (inactiveMembers.Any())
            {
                return Ok(inactiveMembers);
            }

            return NotFound("No inactive members found.");
        }

        [Authorize]
        [HttpGet("GetAllBorrowedBooks")]
        public ActionResult GetAllBorrowedBooks()
        {

            return Ok(Context.BorrowedBooks.ToList());
        }

        [Authorize]
        [HttpGet("GetOrdersOFUser")]
        public ActionResult GetOrdersOFUser(int memberId)
        {
            Console.WriteLine("MemberId :  "+memberId);
            var orders = Context.BorrowedBooks
                .Include(o => o.Book)
                .Include(o => o.Member)
                .Where(o => o.MemberId == memberId && o.Status==BorrowedBookStatus.Borrowed)
                .ToList();
            if (orders.Any())
            {
                return Ok(orders);
            }
            else
            {
                return NotFound("Not found this member has no order!");
            }
        }

        [HttpGet("ReturnBook")]
        public ActionResult ReturnBook(int memberId, int bookId, int fine)
        {
            var order = Context.BorrowedBooks.SingleOrDefault(o => o.MemberId == memberId && o.BookId == bookId);
            var member = Context.Members.SingleOrDefault(m => m.MemberId == memberId);
            if (order is not null)
            {
                order.Status = BorrowedBookStatus.Returned;
                order.ReturnDate = DateTime.Now;
                order.LateFee = fine;
                if (fine > 0 && member!=null)
                {
                    var trx = new Transaction()
                    {
                        MemberId = member.MemberId,
                        TransactionType = TransactionType.MembershipFee,
                        Amount = fine,
                        Date = DateTime.Now,
                        Details = "For " + (DateTime.Now - order.DueDate).Days + " days late return fine."
                    };
                    Context.Transactions.Add(trx);
                }
                var book = Context.Books.Single(b => b.BookId == order.BookId);
                book.AvailableCopies += 1;
                book.Status = BookStatus.Available;

                Context.SaveChanges();

                return Ok("returned");
            }
            return Ok("not returned");
        }

        [Authorize]
        [HttpGet("AssignLibrarian")]
        public ActionResult AssignLibrarian(string username)
        {
            Console.WriteLine("username : " + username);
            var user = Context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Role = Role.Librarian;
                Context.SaveChanges();
                return Ok("Successfully, Librarian is assigned!");
            }
            return NotFound("Username not found!");
        }

        [Authorize]
        [HttpPost("AddCategory")]
        public ActionResult AddCategory(BookCategory bookCategory)
        {
            Console.WriteLine(bookCategory);
            var exists = Context.BookCategories.Any(bc => bc.CategoryName == bookCategory.CategoryName && bc.SubCategoryName == bookCategory.SubCategoryName);
            if (exists)
            {
                return Ok("cannot insert");
            }
            else
            {
                Context.BookCategories.Add(new()
                {
                    CategoryName = bookCategory.CategoryName,
                    SubCategoryName = bookCategory.SubCategoryName,
                    Description=bookCategory.Description
                });
                Context.SaveChanges();
                return Ok("INSERTED");
            }
        }


        [Authorize]
        [HttpGet("GetCategories")]
        public ActionResult GetCategories()
        {
            var categories = Context.BookCategories.ToList();
            if (categories.Any())
            {
                return Ok(categories);
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost("AddBook")]
        public ActionResult AddBook(BookDTO bookDto)
        {
            if (bookDto == null)
            {
                return NotFound("Book details is empty");
            }
            var book = new Book
            {
                BookCategoryId = bookDto.BookCategoryId,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Genre = bookDto.Genre,
                ISBN = bookDto.ISBN,
                PublicationDate = bookDto.PublicationDate,
                AvailableCopies = bookDto.AvailableCopies,
                Status = BookStatus.Available 
            };

            SendNotification(1, "New Book added", $"New book( {bookDto.Title} ) added in our library. You can borrow it.");
            Context.Books.Add(book);
            Context.SaveChanges();

            return Ok("Book inserted successfully");
        }


        [Authorize]
        [HttpPost("ReserveBook")]
        public ActionResult ReserveBook(int userId, int bookId)
        {
            var member = Context.Members.SingleOrDefault(m => m.userId == userId);
            if (member == null) return Ok("You are not a member of library");
            var reserve = new Reservation
            {
                BookId = bookId,
                MemberId=member.MemberId,
                ReservationDate=DateTime.Now,

            };


            Context.Reservations.Add(reserve);
            Context.SaveChanges();

            return Ok("Book reserved successfully!");
        }


        [Authorize]
        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook(int id)
        {
            var exists = Context.Books.Any(b => b.BookId == id);
            if (exists)
            {
                var book = Context.Books.Find(id);
                Context.Books.Remove(book!);
                Context.SaveChanges();
                return Ok("deleted");
            }
            return NotFound();
        }



        [Authorize]
        [HttpGet("GetUsers")]
        public ActionResult GetUsers()
        {
            return Ok(Context.Users.ToList());
        }

        [Authorize]
        [HttpGet("GetMembers")]
        public ActionResult GetMembers()
        {
            return Ok(Context.Members.ToList());
        }

        [Authorize]
        [HttpGet("GetReservations")]
        public ActionResult GetReservations()
        {
            return Ok(Context.Reservations.ToList());
        }

        [Authorize]
        [HttpGet("ApproveRequest")]
        public ActionResult ApproveRequest(int memberId)
        {
            var member = Context.Members.SingleOrDefault(u=> u.MemberId == memberId);
            
            if (member is not null)
            {
                if (member.Status == "Inactive")
                {
                    member.Status = "Active";
                    member.MembershipDate=DateTime.Now;
                    SendNotification(member.userId, "Membership", $"Your {member.MembershipType} Membership request is approved.You got one month subscription.");
                    var user = Context.Users.Find(member.userId);
                    if(user is not null)
                    {
                        user.Role = Role.Member;
                        EmailService.SendEmail(user.Email, "Account Approved", $"""
                        <html>
                            <body>
                                <h2>Hi, {user.FirstName} {user.LastName}</h2>
                                <h3>You membership has been approved by admin.</h3>
                                <h3>Now you can borrow the books to your account.</h3>
                            </body>
                        </html>
                    """);
                    }
                    var trx = new Transaction(){
                        MemberId=member.MemberId,
                        TransactionType=TransactionType.MembershipFee,
                        Amount=member.MembershipType=="Regular"? 50: 80,
                        Date=DateTime.Now,
                        Details="Monthly membership fee for "+ member.MembershipType+ " MembershipType!"
                    };
                    Context.Transactions.Add(trx);
                    Context.SaveChanges();
                    

                    return Ok("approved");
                }
            }

            return Ok("not approved");
        }



        [Authorize]
        [HttpGet("GetOrders")]
        public ActionResult GetOrders()
        {

            var orders = Context.BorrowedBooks.Include(o => o.Member).Include(o => o.Book).ToList();
            if (orders.Any())
            {
                return Ok(orders);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpGet("SendEmailForPendingReturns")]
        public ActionResult SendEmailForPendingReturns()
        {
            var orders = Context.BorrowedBooks
                                .Include(o => o.Book)
                                .Include(o => o.Member)
                                .Where(o => o.Status == BorrowedBookStatus.Borrowed)
                                .ToList();

            var emailsWithFine = orders.Where(o => DateTime.Now > o.DueDate).ToList();
            emailsWithFine.ForEach(x => x.LateFee = (DateTime.Now - x.DueDate).Days * 30);

            var firstFineEmails = emailsWithFine.Where(x => x.LateFee == 30).ToList();

            // Process first fine emails
            firstFineEmails.ForEach(x =>
            {
                var body = $"""
        <html>
            <body>
                <h2>Hi, {x.Member?.Name} </h2>
                <h4>Yesterday was your last day to return Book: "{x.Book?.Title}".</h4>
                <h4>From today, every day a fine of 50Rs will be added for this book.</h4>
                <h4>Please return it as soon as possible.</h4>
                <h4>If your fine exceeds 500Rs, your account will be blocked.</h4>
                <h4>Thanks</h4>
            </body>
        </html>
        """;

                var email = ExtractEmail(x.Member?.ContactDetails);
                if (email != null)
                {
                    EmailService.SendEmail(email, "Return Overdue", body);
                    SendNotification(x.Member.userId, "Late Fee", "Your first day fine is 30rs.Everyday fine will be added 30rs. Kindly , return the books by paying the late fee fine. After 10 days late, your membership will be disable. Thank you.");
                }
            });

            var regularFineEmails = emailsWithFine.Where(x => x.LateFee > 30 && x.LateFee <= 500).ToList();

            // Process regular fine emails
            regularFineEmails.ForEach(x =>
            {
                var regularFineEmailsBody = $"""
        <html>
            <body>
                <h2>Hi, {x.Member?.Name}</h2>
                <h4>You have {x.LateFee}Rs fine on Book: "{x.Book?.Title}"</h4>
                <h4>Please pay it as soon as possible.</h4>
                <h4>Thanks</h4>
            </body>
        </html>
        """;

                var email = ExtractEmail(x.Member?.ContactDetails);
                if (email != null)
                {
                    EmailService.SendEmail(email, "Fine To Pay", regularFineEmailsBody);
                    SendNotification(x.Member.userId, "Late Fee", $"Your {(DateTime.Now - x.DueDate).Days} days fine is {x.LateFee}. Every day fine will be added 30rs. Kindly, return the books by paying the late fee fine. If fine exceeds 500rs, your membership will be disabled. Thank you.");
                }
            });

            var overdueFineEmails = emailsWithFine.Where(x => x.LateFee > 500).ToList();

            // Process overdue fine emails
            overdueFineEmails.ForEach(x =>
            {
                var overdueFineEmailsBody = $"""
        <html>
            <body>
                <h2>Hi, {x.Member?.Name}</h2>
                <h4>You have {x.LateFee}Rs fine on Book: "{x.Book?.Title}"</h4>
                <h4>Your account is BLOCKED.</h4>
                <h4>Please pay it as soon as possible to UNBLOCK your account.</h4>
                <h4>Thanks</h4>
            </body>
        </html>
        """;

                var email = ExtractEmail(x.Member?.ContactDetails);
                if (email != null)
                {
                    
                     EmailService.SendEmail(email, "Fine Overdue", overdueFineEmailsBody);
                    SendNotification(x.Member.userId, "Late Fee", $"Your {(DateTime.Now - x.DueDate).Days} days fine is {x.LateFee}. Kindly, return the books by paying the late fee fine. Your fine exceeds 500rs, your membership is disabled. Contact the admin. Thank you.");
                }
            });

            Context.SaveChanges(); 
            return Ok("sent");
        }

        // Extract email from contact details
        private string ExtractEmail(string contactDetails)
        {
            if (!string.IsNullOrEmpty(contactDetails))
            {
                string[] parts = contactDetails.Split(',');
                if (parts.Length > 1)
                {
                    return parts[1].Trim();
                }
            }
            return null;
        }

        // Helper method to send email and add notification
        private void SendNotification(int x, string notificationType, string message)
        {
            Notification notification = new Notification()
            {
                Type = notificationType,
                Message = message,
                Timestamp = DateTime.Now,
                UserId = x
            };

            Context.Notifications.Add(notification);
            
        }



        [Authorize]
        [HttpGet("BlockFineOverdueUsers")]
        public ActionResult BlockFineOverdueUsers()
        {
            var orders = Context.BorrowedBooks
                            .Include(o => o.Book)
                            .Include(o => o.Member)
                            .Where(o => o.Status==BorrowedBookStatus.Borrowed)
                            .ToList();

            var emailsWithFine = orders.Where(o => DateTime.Now > o.DueDate).ToList();
            emailsWithFine.ForEach(x => x.LateFee = (DateTime.Now - x.DueDate).Days * 30);

            var users = emailsWithFine.Where(x => x.LateFee > 500).Select(x => x.Member!).Distinct().ToList();

            if (users is not null && users.Any())
            {
                foreach (var user in users)
                {
                    user.Status = "Blocked";
                    SendNotification(user.userId, "Overdue", "Your account is disabled by admin.Kindly contact to admin for active. Thank you");
                }

                Context.SaveChanges();

                return Ok("blocked");
            }
            else
            {
                return Ok("not blocked");
            }
        }

        [Authorize]
        [HttpGet("GetNotifications")]
        public ActionResult GetNotifications()
        {
            return Ok(Context.Notifications);
        }

        [Authorize]
        [HttpGet("Unblock")]
        public ActionResult Unblock(int userId)
        {
            var user = Context.Members.Find(userId);
            if (user is not null)
            {
                user.Status = "Active";
                SendNotification(user.userId, "Account Unblocked", "Your account is unblocked.You can use your membership");
                Context.SaveChanges();
                return Ok("unblocked");
            }

            return Ok("not unblocked");
        }

        [Authorize]
        [HttpGet("MembershipRenewalNotification")]
        public ActionResult MembershipRenewalNotification()
        {
            // Retrieve members with non-null MembershipDate
            var members = Context.Members
                .Where(m => m.MembershipDate.HasValue)
                .AsEnumerable() 
                .Where(m => (DateTime.Now - m.MembershipDate.Value).Days >= 20)
                .ToList();

            foreach (var m in members)
            {
                // Calculate days remaining before membership expires
                var daysRemaining = 30 - (DateTime.Today - m.MembershipDate.Value).Days;

                // Send the notification
                SendNotification(
                    m.userId,
                    "Membership Renewal",
                    $"Your membership will expire in {daysRemaining} days. Please renew promptly."
                );
            }
            Context.SaveChanges();
            return Ok("Successfully sent notifications!");
        }


        [Authorize]
        [HttpPost("LogSearch")]
        public ActionResult LogSearch([FromBody] LogSearchRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request payload.");

            Console.WriteLine($"UserId: {request.UserId}, Query: {request.Query}, Count: {request.Count}");

            var member = Context.Members.SingleOrDefault(m => m.userId == request.UserId);
            if (member == null)
                return NotFound();

            var bookSearch = new BookSearch
            {
                MemberId = member.MemberId,
                SearchQuery = request.Query,
                SearchDate = DateTime.Now,
                ResultsCount = request.Count
            };

            Context.Add(bookSearch);
            Context.SaveChanges();

            return Ok("Inserted");
        }

        [Authorize]
        [HttpGet("GetAllInventories")]
        public IActionResult GetAllInventories()
        {
            var inventories = Context.Inventories.Include(i => i.Book).ToList();
            return Ok(inventories);
        }




        [HttpPut("{id}")]
        public IActionResult UpdateInventory(int id, [FromBody] Inventory inventory)
        {
            var existingInventory = Context.Inventories.Find(id);
            if (existingInventory == null) return NotFound();

            existingInventory.Quantity = inventory.Quantity;
            existingInventory.Condition = inventory.Condition;
            existingInventory.Location = inventory.Location;

            Context.SaveChanges();
            return Ok("Inventory updated successfully.");
        }
        [Authorize]
        [HttpDelete("DeleteInventories")]
        public ActionResult DeleteInventories(int id)
        {
            var inventory = Context.Inventories.SingleOrDefault(u=>u.InventoryId==id);
            if (inventory == null) return NotFound();

            Context.Inventories.Remove(inventory);
            Context.SaveChanges();
            return Ok("Inventory deleted successfully.");
        }

    }
}
