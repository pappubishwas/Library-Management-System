using System.ComponentModel.DataAnnotations;

namespace LMSapi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }=string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public Role Role { get; set; } = Role.Guest;
        public bool IsActive { get; set; } = true;
    }

    public enum Role
    {
        Admin, Librarian, Member, Guest
    }

    public class BookCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string SubCategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public int BookCategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public int AvailableCopies { get; set; }
        public BookStatus Status { get; set; } = BookStatus.Available;

        public BookCategory? BookCategory { get; set; }
    }

    public class BookDTO
    {
        public int BookCategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public int AvailableCopies { get; set; }
    }

    public enum BookStatus
    {
        Available, Borrowed, Reserved
    }


    public class Member
    {
        [Key]
        public int MemberId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string ContactDetails { get; set; } = string.Empty;
        public string MembershipType { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty;
        public DateTime? MembershipDate { get; set; } 
        public int userId { get; set; }
    }



    public class BorrowedBook
    {
        [Key]
        public int BorrowId { get; set; } 
        public int BookId { get; set; } 

        public int MemberId { get; set; } 
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        public float LateFee { get; set; }
        public BorrowedBookStatus Status { get; set; } = BorrowedBookStatus.Borrowed;
        public Member? Member { get; set; }
        public Book? Book { get; set; }
    }

    public enum BorrowedBookStatus
    { 
        Borrowed, Returned
    }

    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; } 
        public int BookId { get; set; } 
        public int MemberId { get; set; } 
        public DateTime ReservationDate { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending; 
    }

    public enum ReservationStatus
    {
        Pending, Completed, Cancelled
     }




    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int MemberId { get; set; } 
        public TransactionType TransactionType { get; set; } = TransactionType.None;
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; } = string.Empty;
    }

    public enum TransactionType
    {
        None,Fine, MembershipFee
    }

    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public string Type { get; set; } = string.Empty; 
        public DateTime GeneratedDate { get; set; }
        public string Data { get; set; } = string.Empty; 
        public int CreatedBy { get; set; } 
    }


    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; } 
        public int BookId { get; set; } 
        public int Quantity { get; set; }
        public Condition Condition { get; set; } = Condition.Good; 
        public string Location { get; set; } = string.Empty;
        public Book? Book { get; set; }
    }

    public enum Condition
    {
        Good, Damaged, Lost
    }

    public class BookSearch
    {
        [Key]
        public int SearchId { get; set; } 
        public int? MemberId { get; set; } 
        public string SearchQuery { get; set; } = string.Empty; 
        public DateTime SearchDate { get; set; }
        public int ResultsCount { get; set; } 
    }


    public class Notification
    {
        [Key]
        public int NotificationId { get; set; } 
        public string Type { get; set; } = string.Empty; 
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; } 
    }

    public class LogSearchRequest
    {
        public int UserId { get; set; }
        public string Query { get; set; }
        public int Count { get; set; }
    }


}
