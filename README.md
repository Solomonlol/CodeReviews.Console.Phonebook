# How to run

## Prerequisites

1).NET 10 SDK

2)PostgreSQL running locally (or reachable) on port 5432 (default setting, can be changed)

3)(Optional, for the email feature) a Gmail account with a Google App Password


## Steps

### 1.Clone the repository and open the solution (Solomonlol.Phonebook.slnx).
### 2.Update the connection string in Solomonlol.Phonebook/appsettings.json if your PostgreSQL credentials differ from the default:
   "PostgresConnection": "Host=localhost;Port=5432;Username=postgres;Database=PhonebookDB"

### 3.Run the Frontend project — this is the console app the user interacts with:

   cd Frontend
   
   dotnet run

On first launch, EF Core migrations are applied automatically (db.Database.MigrateAsync()), creating the database and tables if they don't exist yet. 
### 4. If the Users table is empty, the app seeds five demo users (with contacts) automatically. The default password for all seeded users is Password123!. 5. To use the email feature, log in as a user and go to the email menu — you'll be asked for a Gmail address and an app password (not your normal Gmail password). Only Gmail's SMTP server is supported.

# What the app does

Phonebook is a console application for managing personal contact lists, with basic multi-user support:

1) Users — each user has a login, a hashed password, and personal details (name, email, phone).
2) Contacts — each user has their own private list of contacts (name, phone, email, category, etc.). Contacts belong to exactly one user.
3) User management — create, update, and delete user accounts. Updating or deleting a user requires re-entering that user's password as a confirmation step.
4) Login — pick a user and enter their password to "sign in" for the session.
5) Contact management (after login) — create, update, and delete contacts belonging to the logged-in user.
6) Email — after login, select one or more contacts and send them an email message via Gmail's SMTP server, using a Gmail address + app password supplied at runtime.

On the very first run, if the database has no users, the app seeds a handful of demo users and contacts so there's something to explore immediately.

#  Architectural choices

The solution is split into three projects:


1) Solomonlol.Phonebook (Backend): Domain models, EF Core DbContext, repositories, services, validation, mapping, migrations
2) Frontend: Console UI (menus) built with Spectre.Console, wires up DI and drives the app
3) TestPhonebook: Unit tests (currently focused on validation logic)

Key patterns used:

1) Layered architecture — the Frontend never talks to EF Core or the database directly. It calls into Service classes (UserService, ContactService, EmailService), which contain the business logic and validation.
2) Repository + Unit of Work — IRepository<T> provides generic CRUD operations over any entity; UserRepository and ContactRepository extend it with entity-specific queries. IUnitOfWork groups repositories together and exposes a single SaveAsync() so multiple repository operations can be committed as one database transaction.
3) Dependency Injection — the app uses Microsoft.Extensions.Hosting's generic host (Host.CreateDefaultBuilder) to configure and resolve everything: DbContext, repositories, services, menus, and cross-cutting singletons like CurrentUserService.
4) DTOs + AutoMapper — entities (User, Contact) are never passed directly to/from the UI layer. UserDto, ContactDto, CreateUserDto shape the data that crosses layer boundaries, and MappingProfile (AutoMapper) handles the entity ↔ DTO conversion.
5) EF Core + PostgreSQL (Npgsql) as the persistence layer, with migrations checked into the repo and applied automatically at startup.
6) Password hashing via ASP.NET Core Identity's IPasswordHasher<User> rather than storing plaintext passwords.
7) Data Protection API (EmailPasswordProtection) to encrypt the Gmail app password in memory/at rest for the duration it's needed, instead of handling it as plain text.
8) Custom exceptions (NotFoundException, ValidationException) to distinguish "not found" and "invalid input" failure cases from unexpected errors, so the UI layer can catch and display them as friendly messages instead of raw stack traces.
9) CurrentUserService — a singleton that tracks which user is "logged in" for the current console session, since a console app has no concept of an HTTP session/cookie.

# How to use

When user open the app, if there is no records in database exist, app will create few records. Then user can see Main menu

<img width="345" height="178" alt="image" src="https://github.com/user-attachments/assets/3ff2a15c-58b9-4d60-9d25-02c60df8f0e8" />

Exit application just close the app. 

1. User management:

It's a menu where user can create, update and delete user record 

<img width="295" height="189" alt="image" src="https://github.com/user-attachments/assets/804a8b40-aae5-4028-8c6a-4d3a8777d2fc" />

To create user you need enter your data
<img width="820" height="297" alt="image" src="https://github.com/user-attachments/assets/18031b36-049f-4f85-a908-65678bc812b5" />

If you want to delete or update user you need:
1) Choose what user you need
<img width="328" height="237" alt="image" src="https://github.com/user-attachments/assets/187e98ac-bcbf-4a4d-a760-b4da4b0dc37e" />

2)Enter correct user password

if password is incorrect you will see a "Invalid password" message

<img width="379" height="249" alt="image" src="https://github.com/user-attachments/assets/b165112a-ba06-4ce9-94df-196e804f9603" />

2. Log In

In logIn menu you choose user

<img width="365" height="260" alt="image" src="https://github.com/user-attachments/assets/a7763816-0491-4b59-b864-a86c15146357" />

After you enter correct password (for autocreated users it's "Password123!") you will see Log In menu, where you have contact management menu and option to send email message. Option Back just return you in Main menu

<img width="313" height="195" alt="image" src="https://github.com/user-attachments/assets/fb6321ba-16e6-465f-b715-375e0363aeb1" />

1) Contact Management
   It's allow you to create, update and delete contact records of this user
   
   <img width="323" height="216" alt="image" src="https://github.com/user-attachments/assets/0cce078d-8fa7-4be0-ac01-eb6a0b0a3f2b" />
   
3) To send message you need to have gmail and application password(you can create it in your google account). This application didn't work with different mail.
If you try, you will see an error message.

To create a message, you first need to select the recipients.

<img width="1283" height="274" alt="image" src="https://github.com/user-attachments/assets/7add8938-394f-494c-88d4-bb238cec6f57" />

Then enter header and message:

<img width="409" height="94" alt="image" src="https://github.com/user-attachments/assets/29fc3f5b-8da3-47d6-895e-a4f893cbac58" />

If the Gmail details are correct, you will see a message confirming successful sending.

<img width="548" height="227" alt="image" src="https://github.com/user-attachments/assets/15d83871-726a-421c-a589-fbf871cb8423" />


# Reflection

1) I used the Repository and Unit of Work patterns for learning purposes. I learned the difference between making direct calls via EF Core and using a repository.
2) I tried implementing email sending in my application for the first time. 
It was fun, but I see huge room for improvement.
3) I've started to better understand how to use exceptions.
4) I gained some solid experience using Microsoft.Extensions.Hosting.
5) To be continued... 
(yes, it's JoJo reference)
