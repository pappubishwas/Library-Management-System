using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMSapi.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "BookSearches",
                columns: table => new
                {
                    SearchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    SearchQuery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSearches", x => x.SearchId);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.InventoryId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookCategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableCopies = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_BookCategories_BookCategoryId",
                        column: x => x.BookCategoryId,
                        principalTable: "BookCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowedBooks",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LateFee = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowedBooks", x => x.BorrowId);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowedBooks_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "CategoryId", "CategoryName", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { 1, "computer", "Covers the design, analysis, and application of algorithms in computer science.", "algorithm" },
                    { 2, "computer", "Focuses on the syntax, semantics, and implementation of various programming languages.", "programming languages" },
                    { 3, "computer", "Discusses computer networks, protocols, and communication systems.", "networking" },
                    { 4, "computer", "Explores physical components and architecture of computer systems.", "hardware" },
                    { 5, "mechanical", "Covers the design, function, and operation of mechanical systems and machines.", "machine" },
                    { 6, "mechanical", "Explains the principles of energy transfer in mechanical systems.", "transfer of energy" },
                    { 7, "mathematics", "Focuses on the study of change through derivatives, integrals, and limits.", "calculus" },
                    { 8, "mathematics", "Covers mathematical structures, equations, and relationships between variables.", "algebra" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "Email", "FirstName", "IsActive", "LastName", "MobileNumber", "Password", "Role", "Username" },
                values: new object[] { 1, new DateTime(2024, 12, 6, 17, 29, 8, 710, DateTimeKind.Local).AddTicks(8822), "admin@gmail.com", "Admin", true, "", "1234567890", "admin1999", "Admin", "admin87" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "AvailableCopies", "BookCategoryId", "Genre", "ISBN", "PublicationDate", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Thomas Corman", 5, 1, "Education", "978-0262033848", new DateTime(2009, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Introduction to Algorithm" },
                    { 2, "Robert Sedgewick & Kevin Wayne", 3, 1, "Education", "978-0134319650", new DateTime(2011, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Algorithms" },
                    { 3, "Steve Skiena", 4, 1, "Education", "978-1848000698", new DateTime(2008, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "The Algorithm Design Manual" },
                    { 4, "Adnan Aziz", 6, 1, "Education", "978-1466208681", new DateTime(2010, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Algorithms For Interviews" },
                    { 5, "Eric Matthes", 7, 2, "Programming", "978-1593279288", new DateTime(2019, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Python Crash Course: A Hands-On, Project-Based Introduction to Programming" },
                    { 6, "Joshua Bloch", 5, 2, "Programming", "978-0134685991", new DateTime(2018, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Effective Java" },
                    { 7, "James F Kurose and Keith W Ross", 2, 3, "Networking", "978-0133594140", new DateTime(2017, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "A Top-Down Approach: Computer Networking" },
                    { 8, "Ramesh Gaonkar", 3, 4, "Hardware", "978-9380358173", new DateTime(2012, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Microprocessor Architecture, Programming, and Applications with the 8085" },
                    { 9, "Richard G. Budynas and Keith J. Nisbett", 4, 5, "Mechanical", "978-0073398204", new DateTime(2014, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Shigley's Mechanical Engineering Design" },
                    { 10, "Frank M. White", 6, 6, "Mechanical", "978-0073398273", new DateTime(2015, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Fluid Mechanics" },
                    { 11, "James Stewart", 2, 7, "Mathematics", "978-1285741550", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Calculus: Early Transcendentals" },
                    { 12, "Euclid", 10, 8, "Mathematics", "978-1888009194", new DateTime(1956, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Euclid's Elements" },
                    { 13, "Jon Kleinberg & Éva Tardos", 3, 1, "Education", "978-0321295354", new DateTime(2005, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Algorithm Design" },
                    { 14, "Kathy Sierra and Bert Bates", 4, 2, "Programming", "978-0596009205", new DateTime(2005, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Head First Java" },
                    { 15, "Behrouz A. Forouzan", 5, 3, "Networking", "978-0073376226", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Data Communications and Networking" },
                    { 16, "David Money Harris & Sarah Harris", 6, 4, "Hardware", "978-0123944245", new DateTime(2012, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Digital Design and Computer Architecture" },
                    { 17, "Erik Oberg", 3, 5, "Mechanical", "978-0831130916", new DateTime(2012, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Machinery's Handbook" },
                    { 18, "Claus Borgnakke and Richard E. Sonntag", 2, 6, "Mechanical", "978-1118321775", new DateTime(2012, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Fundamentals of Thermodynamics" },
                    { 19, "Louis Leithold", 4, 7, "Mathematics", "978-0065012310", new DateTime(1990, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "The Calculus with Analytic Geometry" },
                    { 20, "Stephen Hawking", 7, 8, "Science", "978-0553380163", new DateTime(1998, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "A Brief History of Time" },
                    { 21, "Gayle Laakmann McDowell", 6, 1, "Education", "978-0984782857", new DateTime(2015, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Cracking the Coding Interview" },
                    { 22, "Andrew Hunt and David Thomas", 3, 2, "Programming", "978-0201616224", new DateTime(1999, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "The Pragmatic Programmer" },
                    { 23, "Andrew S. Tanenbaum", 4, 3, "Networking", "978-0132126953", new DateTime(2010, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Computer Networks" },
                    { 24, "David A. Patterson & John L. Hennessy", 5, 4, "Hardware", "978-0124077263", new DateTime(2013, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Computer Organization and Design" },
                    { 25, "John J. Craig", 2, 5, "Mechanical", "978-0201543612", new DateTime(1986, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Introduction to Robotics: Mechanics and Control" },
                    { 26, "Robert W. Fox, Alan T. McDonald, and Philip J. Pritchard", 3, 6, "Mechanical", "978-1118139448", new DateTime(2011, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Introduction to Fluid Mechanics" },
                    { 27, "Marijn Haverbeke", 8, 7, "Programming", "978-1593279509", new DateTime(2018, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "Eloquent JavaScript" },
                    { 28, "Donald E. Knuth", 2, 8, "Mathematics", "978-0201896831", new DateTime(1997, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Available", "The Art of Computer Programming" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCategoryId",
                table: "Books",
                column: "BookCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_MemberId",
                table: "BorrowedBooks",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookSearches");

            migrationBuilder.DropTable(
                name: "BorrowedBooks");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "BookCategories");
        }
    }
}
