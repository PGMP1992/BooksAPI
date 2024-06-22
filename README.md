# EndProj - Books Library Management System

## Overview

EndProj - Books is a web application that allows users to manage their personal library. Users can create accounts, log in, add books manually or by ISBN, edit book details, and mark books as complete. 
The application is built using ASP.NET Core and Entity Framework Core, with a SQL Server database.

## Features

- User Registration and Authentication
- Add Books Manually
- Add Books by ISBN
- Edit Book Details
- Mark Books as Complete
- View Books in a User's Library
- Sort Books by Title, Author, and Completion Status

## Technologies Used

- **Backend**: ASP.NET Core, Entity Framework Core
- **Frontend**: Razor Pages, Bootstrap
- **Database**: SQL Server
- **External Services**: Open Library API for fetching book details by ISBN

## Getting Started
// Using Package Manager - PM 
add-migration initial -Context BooksContext
update-database -Context BooksContext

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/Torsten86/EndProj-Books.git
   cd EndProj-Books

   Usage
User Registration

    Click on "Create New Account" on the home page.
    Enter your desired username and password.
    Click "Create" to register.

Logging In

    Enter your username and password on the login page.
    Click "Log In" to access your library.

Managing Books
    Add a Book Manually:
        Click on "Create New Book" above the table.
        Fill in the book details and click "Save".

    Add a Book by ISBN:
        Click on "Add Book by ISBN" above the table.
        Enter the ISBN and click "Add".

    Edit a Book:
        Click on the "Edit" button next to the book you want to edit.
        Update the book details and click "Save".

    Mark a Book as Complete:
        Click on the "Edit" button next to the book you want to mark as complete.
        Check the "Complete" checkbox and click "Save".

    Delete a Book:
        Click on the "Delete" button next to the book you want to delete.
        Confirm the deletion.

Sorting Books

    Click on the column headers "Title", "Author", or "Complete" to sort the books by that column.

        Add a Book Manually:
        Click on "Create New Book" above the table.
        Fill in the book details and click "Save".

    Add a Book by ISBN:
        Click on "Add Book by ISBN" above the table.
        Enter the ISBN and click "Add".

    Edit a Book:
        Click on the "Edit" button next to the book you want to edit.
        Update the book details and click "Save".

    Mark a Book as Complete:
        Click on the "Edit" button next to the book you want to mark as complete.
        Check the "Complete" checkbox and click "Save".

    Delete a Book:
        Click on the "Delete" button next to the book you want to delete.
        Confirm the deletion.

Sorting Books

    Click on the column headers "Title", "Author", or "Complete" to sort the books by that column.

    Contributing

Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.
License

This project is licensed under the MIT License. See the LICENSE file for details.
