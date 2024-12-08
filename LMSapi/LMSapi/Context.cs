using LMSapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LMSapi
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<BookSearch> BookSearches { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        public Context(DbContextOptions<Context> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Username = "admin87",
                FirstName = "Admin",
                LastName = "",
                Email = "admin@gmail.com",
                MobileNumber = "1234567890",
                IsActive = true,
                Role = Role.Admin,
                Password = "admin1999",
                CreatedOn = DateTime.Now
            });


            modelBuilder.Entity<BookCategory>().HasData(
                new BookCategory
                {
                    CategoryId = 1,
                    CategoryName = "computer",
                    SubCategoryName = "algorithm",
                    Description = "Covers the design, analysis, and application of algorithms in computer science."
                },
                new BookCategory
                {
                    CategoryId = 2,
                    CategoryName = "computer",
                    SubCategoryName = "programming languages",
                    Description = "Focuses on the syntax, semantics, and implementation of various programming languages."
                },
                new BookCategory
                {
                    CategoryId = 3,
                    CategoryName = "computer",
                    SubCategoryName = "networking",
                    Description = "Discusses computer networks, protocols, and communication systems."
                },
                new BookCategory
                {
                    CategoryId = 4,
                    CategoryName = "computer",
                    SubCategoryName = "hardware",
                    Description = "Explores physical components and architecture of computer systems."
                },
                new BookCategory
                {
                    CategoryId = 5,
                    CategoryName = "mechanical",
                    SubCategoryName = "machine",
                    Description = "Covers the design, function, and operation of mechanical systems and machines."
                },
                new BookCategory
                {
                    CategoryId = 6,
                    CategoryName = "mechanical",
                    SubCategoryName = "transfer of energy",
                    Description = "Explains the principles of energy transfer in mechanical systems."
                },
                new BookCategory
                {
                    CategoryId = 7,
                    CategoryName = "mathematics",
                    SubCategoryName = "calculus",
                    Description = "Focuses on the study of change through derivatives, integrals, and limits."
                },
                new BookCategory
                {
                    CategoryId = 8,
                    CategoryName = "mathematics",
                    SubCategoryName = "algebra",
                    Description = "Covers mathematical structures, equations, and relationships between variables."
                }
            );

            modelBuilder.Entity<Book>().HasData(
    new Book { BookId = 1, BookCategoryId = 1, Title = "Introduction to Algorithm", Author = "Thomas Corman", Genre = "Education", ISBN = "978-0262033848", PublicationDate = new DateTime(2009, 7, 31), AvailableCopies = 5, Status = BookStatus.Available },
    new Book { BookId = 2, BookCategoryId = 1, Title = "Algorithms", Author = "Robert Sedgewick & Kevin Wayne", Genre = "Education", ISBN = "978-0134319650", PublicationDate = new DateTime(2011, 3, 15), AvailableCopies = 3, Status = BookStatus.Available },
    new Book { BookId = 3, BookCategoryId = 1, Title = "The Algorithm Design Manual", Author = "Steve Skiena", Genre = "Education", ISBN = "978-1848000698", PublicationDate = new DateTime(2008, 4, 28), AvailableCopies = 4, Status = BookStatus.Available },
    new Book { BookId = 4, BookCategoryId = 1, Title = "Algorithms For Interviews", Author = "Adnan Aziz", Genre = "Education", ISBN = "978-1466208681", PublicationDate = new DateTime(2010, 11, 17), AvailableCopies = 6, Status = BookStatus.Available },
    new Book { BookId = 5, BookCategoryId = 2, Title = "Python Crash Course: A Hands-On, Project-Based Introduction to Programming", Author = "Eric Matthes", Genre = "Programming", ISBN = "978-1593279288", PublicationDate = new DateTime(2019, 5, 3), AvailableCopies = 7, Status = BookStatus.Available },
    new Book { BookId = 6, BookCategoryId = 2, Title = "Effective Java", Author = "Joshua Bloch", Genre = "Programming", ISBN = "978-0134685991", PublicationDate = new DateTime(2018, 1, 6), AvailableCopies = 5, Status = BookStatus.Available },
    new Book { BookId = 7, BookCategoryId = 3, Title = "A Top-Down Approach: Computer Networking", Author = "James F Kurose and Keith W Ross", Genre = "Networking", ISBN = "978-0133594140", PublicationDate = new DateTime(2017, 6, 15), AvailableCopies = 2, Status = BookStatus.Available },
    new Book { BookId = 8, BookCategoryId = 4, Title = "Microprocessor Architecture, Programming, and Applications with the 8085", Author = "Ramesh Gaonkar", Genre = "Hardware", ISBN = "978-9380358173", PublicationDate = new DateTime(2012, 8, 28), AvailableCopies = 3, Status = BookStatus.Available },
    new Book { BookId = 9, BookCategoryId = 5, Title = "Shigley's Mechanical Engineering Design", Author = "Richard G. Budynas and Keith J. Nisbett", Genre = "Mechanical", ISBN = "978-0073398204", PublicationDate = new DateTime(2014, 1, 27), AvailableCopies = 4, Status = BookStatus.Available },
    new Book { BookId = 10, BookCategoryId = 6, Title = "Fluid Mechanics", Author = "Frank M. White", Genre = "Mechanical", ISBN = "978-0073398273", PublicationDate = new DateTime(2015, 5, 8), AvailableCopies = 6, Status = BookStatus.Available },
    new Book { BookId = 11, BookCategoryId = 7, Title = "Calculus: Early Transcendentals", Author = "James Stewart", Genre = "Mathematics", ISBN = "978-1285741550", PublicationDate = new DateTime(2015, 1, 1), AvailableCopies = 2, Status = BookStatus.Available },
    new Book { BookId = 12, BookCategoryId = 8, Title = "Euclid's Elements", Author = "Euclid", Genre = "Mathematics", ISBN = "978-1888009194", PublicationDate = new DateTime(1956, 1, 1), AvailableCopies = 10, Status = BookStatus.Available },
 new Book { BookId = 13, BookCategoryId = 1, Title = "Algorithm Design", Author = "Jon Kleinberg & Éva Tardos", Genre = "Education", ISBN = "978-0321295354", PublicationDate = new DateTime(2005, 9, 8), AvailableCopies = 3, Status = BookStatus.Available },
    new Book { BookId = 14, BookCategoryId = 2, Title = "Head First Java", Author = "Kathy Sierra and Bert Bates", Genre = "Programming", ISBN = "978-0596009205", PublicationDate = new DateTime(2005, 2, 9), AvailableCopies = 4, Status = BookStatus.Available },
    new Book { BookId = 15, BookCategoryId = 3, Title = "Data Communications and Networking", Author = "Behrouz A. Forouzan", Genre = "Networking", ISBN = "978-0073376226", PublicationDate = new DateTime(2012, 1, 1), AvailableCopies = 5, Status = BookStatus.Available },
    new Book { BookId = 16, BookCategoryId = 4, Title = "Digital Design and Computer Architecture", Author = "David Money Harris & Sarah Harris", Genre = "Hardware", ISBN = "978-0123944245", PublicationDate = new DateTime(2012, 8, 28), AvailableCopies = 6, Status = BookStatus.Available },
    new Book { BookId = 17, BookCategoryId = 5, Title = "Machinery's Handbook", Author = "Erik Oberg", Genre = "Mechanical", ISBN = "978-0831130916", PublicationDate = new DateTime(2012, 5, 1), AvailableCopies = 3, Status = BookStatus.Available },
    new Book { BookId = 18, BookCategoryId = 6, Title = "Fundamentals of Thermodynamics", Author = "Claus Borgnakke and Richard E. Sonntag", Genre = "Mechanical", ISBN = "978-1118321775", PublicationDate = new DateTime(2012, 3, 1), AvailableCopies = 2, Status = BookStatus.Available },
    new Book { BookId = 19, BookCategoryId = 7, Title = "The Calculus with Analytic Geometry", Author = "Louis Leithold", Genre = "Mathematics", ISBN = "978-0065012310", PublicationDate = new DateTime(1990, 6, 1), AvailableCopies = 4, Status = BookStatus.Available },
    new Book { BookId = 20, BookCategoryId = 8, Title = "A Brief History of Time", Author = "Stephen Hawking", Genre = "Science", ISBN = "978-0553380163", PublicationDate = new DateTime(1998, 9, 1), AvailableCopies = 7, Status = BookStatus.Available },
    new Book { BookId = 21, BookCategoryId = 1, Title = "Cracking the Coding Interview", Author = "Gayle Laakmann McDowell", Genre = "Education", ISBN = "978-0984782857", PublicationDate = new DateTime(2015, 7, 1), AvailableCopies = 6, Status = BookStatus.Available },
    new Book { BookId = 22, BookCategoryId = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt and David Thomas", Genre = "Programming", ISBN = "978-0201616224", PublicationDate = new DateTime(1999, 10, 20), AvailableCopies = 3, Status = BookStatus.Available },
    new Book { BookId = 23, BookCategoryId = 3, Title = "Computer Networks", Author = "Andrew S. Tanenbaum", Genre = "Networking", ISBN = "978-0132126953", PublicationDate = new DateTime(2010, 7, 28), AvailableCopies = 4, Status = BookStatus.Available },
    new Book { BookId = 24, BookCategoryId = 4, Title = "Computer Organization and Design", Author = "David A. Patterson & John L. Hennessy", Genre = "Hardware", ISBN = "978-0124077263", PublicationDate = new DateTime(2013, 10, 10), AvailableCopies = 5, Status = BookStatus.Available },
    new Book { BookId = 25, BookCategoryId = 5, Title = "Introduction to Robotics: Mechanics and Control", Author = "John J. Craig", Genre = "Mechanical", ISBN = "978-0201543612", PublicationDate = new DateTime(1986, 8, 8), AvailableCopies = 2, Status = BookStatus.Available },
    new Book { BookId = 26, BookCategoryId = 6, Title = "Introduction to Fluid Mechanics", Author = "Robert W. Fox, Alan T. McDonald, and Philip J. Pritchard", Genre = "Mechanical", ISBN = "978-1118139448", PublicationDate = new DateTime(2011, 8, 1), AvailableCopies = 3, Status = BookStatus.Available },
    new Book { BookId = 27, BookCategoryId = 7, Title = "Eloquent JavaScript", Author = "Marijn Haverbeke", Genre = "Programming", ISBN = "978-1593279509", PublicationDate = new DateTime(2018, 12, 4), AvailableCopies = 8, Status = BookStatus.Available },
    new Book { BookId = 28, BookCategoryId = 8, Title = "The Art of Computer Programming", Author = "Donald E. Knuth", Genre = "Mathematics", ISBN = "978-0201896831", PublicationDate = new DateTime(1997, 2, 10), AvailableCopies = 2, Status = BookStatus.Available });

            modelBuilder.Entity<Inventory>().HasData(
    new Inventory { InventoryId = 1, BookId = 1, Quantity = 5, Condition = Condition.Good, Location = "Main Library - Shelf A1" },
    new Inventory { InventoryId = 2, BookId = 2, Quantity = 3, Condition = Condition.Good, Location = "Main Library - Shelf A1" },
    new Inventory { InventoryId = 3, BookId = 3, Quantity = 4, Condition = Condition.Damaged, Location = "Main Library - Shelf A2" },
    new Inventory { InventoryId = 4, BookId = 4, Quantity = 6, Condition = Condition.Good, Location = "Main Library - Shelf A3" },
    new Inventory { InventoryId = 5, BookId = 5, Quantity = 7, Condition = Condition.Good, Location = "Programming Section - Shelf B1" },
    new Inventory { InventoryId = 6, BookId = 6, Quantity = 5, Condition = Condition.Good, Location = "Programming Section - Shelf B2" },
    new Inventory { InventoryId = 7, BookId = 7, Quantity = 2, Condition = Condition.Damaged, Location = "Networking Section - Shelf C1" },
    new Inventory { InventoryId = 8, BookId = 8, Quantity = 3, Condition = Condition.Lost, Location = "Hardware Section - Shelf D1" },
    new Inventory { InventoryId = 9, BookId = 9, Quantity = 4, Condition = Condition.Good, Location = "Mechanical Section - Shelf E1" },
    new Inventory { InventoryId = 10, BookId = 10, Quantity = 6, Condition = Condition.Good, Location = "Mechanical Section - Shelf E2" },
    new Inventory { InventoryId = 11, BookId = 11, Quantity = 2, Condition = Condition.Damaged, Location = "Mathematics Section - Shelf F1" },
    new Inventory { InventoryId = 12, BookId = 12, Quantity = 10, Condition = Condition.Good, Location = "Mathematics Section - Shelf F2" },
    new Inventory { InventoryId = 13, BookId = 13, Quantity = 3, Condition = Condition.Good, Location = "Main Library - Shelf A4" },
    new Inventory { InventoryId = 14, BookId = 14, Quantity = 4, Condition = Condition.Good, Location = "Programming Section - Shelf B3" },
    new Inventory { InventoryId = 15, BookId = 15, Quantity = 5, Condition = Condition.Good, Location = "Networking Section - Shelf C2" },
    new Inventory { InventoryId = 16, BookId = 16, Quantity = 6, Condition = Condition.Good, Location = "Hardware Section - Shelf D2" },
    new Inventory { InventoryId = 17, BookId = 17, Quantity = 3, Condition = Condition.Good, Location = "Mechanical Section - Shelf E3" },
    new Inventory { InventoryId = 18, BookId = 18, Quantity = 2, Condition = Condition.Damaged, Location = "Mechanical Section - Shelf E4" },
    new Inventory { InventoryId = 19, BookId = 19, Quantity = 4, Condition = Condition.Good, Location = "Mathematics Section - Shelf F3" },
    new Inventory { InventoryId = 20, BookId = 20, Quantity = 7, Condition = Condition.Good, Location = "Science Section - Shelf G1" }
);

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Role>().HaveConversion<string>();
            configurationBuilder.Properties<BookStatus>().HaveConversion<string>();
            configurationBuilder.Properties<BorrowedBookStatus>().HaveConversion<string>();
            configurationBuilder.Properties<ReservationStatus>().HaveConversion<string>();
            configurationBuilder.Properties<TransactionType>().HaveConversion<string>();
            configurationBuilder.Properties<Condition>().HaveConversion<string>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
