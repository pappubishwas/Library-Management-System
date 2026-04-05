# Library Management System

A comprehensive, full-stack Library Management System designed to handle book inventory, member borrowing, role-based access control, and financial transactions. 

This project was built to demonstrate clean architecture, secure API design, and complex relational data modeling.

## 🚀 Tech Stack

* Backend: .NET 6 (or later) with C#
* Frontend: Angular 17 (or later)
* Database: SQL Server 2022 (or MongoDB 6.0)
* Authentication: JWT (JSON Web Tokens) / OAuth 2.0

## ✨ Key Features

* Role-Based Access Control (RBAC): Secure login and distinct permission levels for Admins, Librarians, Members, and Guests.
* Book & Inventory Management: Full CRUD operations for books, tracking availability, conditions, and categorization by genre.
* Borrowing & Reservations: Members can borrow books, track due dates, and reserve unavailable items with automated queue management.
* Financial Transactions: Built-in tracking for late fees, membership payments, and book purchases.
* Reporting & Analytics: Dashboards to monitor popular books, active members, and library financial health.
* Notifications: Alerts for overdue books, reserved availability, and membership renewals.

## 🗄️ Database Schema Overview

The system utilizes a highly relational database structure, including:
* Users (UserId, Username, Role, PasswordHash)
* Books (BookId, Title, ISBN, AvailableCopies)
* Members (MemberId, MembershipType, Status)
* BorrowedBooks (BorrowId, BorrowDate, DueDate, LateFee)
* Transactions (TransactionId, Amount, TransactionType)
* Reservations & Notifications

## 🛠️ Local Setup & Installation

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download)
* [Node.js & npm](https://nodejs.org/)
* [Angular CLI](https://angular.io/cli) (npm install -g @angular/cli)
* SQL Server instance running

### Backend Setup (.NET API)
1. Navigate to the backend server directory.
2. Update the appsettings.json file with your database connection string and JWT secret key.
3. Apply database migrations:
   bash
   dotnet ef database update
   
4. Run the API:
   bash
   dotnet run
   

### Frontend Setup (Angular)
1. Navigate to the frontend client directory.
2. Install dependencies:
   bash
   npm install
   
3. Start the development server:
   bash
   ng serve
   
4. Open your browser and navigate to http://localhost:4200.

## 🛡️ API Endpoints & Testing

The backend exposes a RESTful API. Key endpoint categories include:
* POST /api/auth/login - Authenticate and receive JWT.
* GET /api/books - Retrieve catalog (filterable and searchable).
* POST /api/borrow - Process a book borrowing request (Member role required).
* GET /api/reports/summary - Fetch dashboard analytics (Admin/Librarian role required).

*(Note: A full Postman collection or Swagger UI is available by navigating to https://localhost:<port>/swagger when the backend is running).*

## 👨‍💻 Author
Pappu Bishwas GitHub: [@pappubishwas](https://github.com/pappubishwas)

