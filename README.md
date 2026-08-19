# Phonebook

A console-based phonebook application built with C# and Entity Framework Core. The application stores contacts in a local SQLite database, supports contact CRUD operations, groups contacts into predefined categories, validates user input, and provides console workflows for composing email and SMS messages for saved contacts.

The project is based on [The C# Academy Phone Book project](https://thecsharpacademy.com/project/16/phonebook) and extends the core exercise with a layered application structure, categories, NUnit validation tests, and email/SMS composition workflows.

> **Important:** The current email and SMS features do **not** connect to an SMTP server, email API, SMS gateway, or other external delivery provider. They validate the recipient against saved contacts and create an in-memory message object, but no message is actually transmitted.

## Technologies

- **C# / .NET 9**
- **Entity Framework Core 9.0.18**
- **SQLite**
- **Spectre.Console 0.57.2**
- **NUnit 4.2.2**
- **NUnit3TestAdapter 4.6.0**
- **Microsoft.NET.Test.Sdk 17.12.0**
- **coverlet.collector 6.0.2**

## Features

### Contact management

The application provides console-based CRUD operations for contacts:

- Add a contact
- Display all contacts
- Display a single contact by ID
- Update an existing contact
- Delete a contact

Each contact contains:

- First name
- Last name
- Email address
- Phone number
- Category

Contact data is persisted through Entity Framework Core using SQLite.

### Categories

Contacts can be assigned to one of four predefined categories:

- Family
- Friends
- Work
- Other

The categories are seeded into the database and can be used to:

- Assign a category when creating a contact
- Display a contact's category
- Browse contacts belonging to a selected category

Category creation, editing, and deletion are not implemented.

> **Current limitation:** The update flow asks the user to select a category, but `ContactRepository.Update` currently persists only the contact's first name, last name, email, and phone number. A changed category is therefore not saved by the current implementation.

### Input validation

Validation is handled by the `Validators` helper.

Implemented checks include:

- Required contact fields
- Positive integer contact IDs
- Email format validation
- Phone number format validation
- Non-empty email body validation
- Non-empty SMS body validation

The current phone-number rule accepts:

- An optional leading `+`
- 8 to 15 digits
- A first digit from `1` to `9`

Spaces, dashes, parentheses, letters, numbers shorter than 8 digits, and numbers longer than 15 digits are rejected.

### Email workflow

The main menu contains a **Send an email** option.

The workflow:

1. Prompts for a destination email address.
2. Prompts for an optional title and a required body.
3. Validates that the body is not empty.
4. Validates the email address.
5. Checks that a saved contact exists with that email address.
6. Creates an `Email` record containing the title and body.
7. Displays a success message in the console.

No SMTP client or external email provider is implemented, so the message is not delivered outside the application.

### SMS workflow

The main menu also contains a **Send an SMS message** option.

The workflow:

1. Prompts for a destination phone number.
2. Prompts for the message text.
3. Validates that the message is not empty.
4. Validates the phone-number format.
5. Checks that a saved contact exists with that phone number.
6. Creates an `SMS` record containing the text.
7. Displays a success message in the console.

No SMS gateway or external messaging provider is implemented, so the message is not delivered outside the application.

## Architecture

The project uses a layered console-application structure rather than placing database, UI, and business logic in a single class.

```text
Console UI
    |
    v
Controllers
    |
    v
Services
    |
    v
Repository interfaces
    |
    v
EF Core repositories
    |
    v
PhonebookContext
    |
    v
SQLite
```

### Views

The `Views` layer contains the Spectre.Console user interface.

Responsibilities include:

- Displaying menus
- Prompting for contact data
- Displaying contact tables
- Displaying errors and success messages
- Collecting email and SMS input

View interfaces such as `IContactsView`, `ICategoriesView`, `IEmailView`, and `ISMSView` separate the UI contract from its concrete implementation.

### Controllers

Controllers coordinate user interaction and application operations.

- `AppController` routes main-menu actions.
- `ContactsController` coordinates contact CRUD operations.
- `CategoriesController` handles category browsing.
- `EmailController` coordinates the email composition workflow.
- `SMSMsgController` coordinates the SMS composition workflow.

Controllers catch operational exceptions and pass user-facing messages to the views.

### Services

Services contain application-level logic between controllers and repositories.

- `ContactsService`
  - Validates contact data
  - Maps `ContactInfo` input to the EF Core `Contact` model
  - Resolves a selected category
  - Coordinates CRUD operations
  - Looks up contacts by email address or phone number
- `CategoriesService`
  - Retrieves categories
  - Retrieves contacts belonging to a category
- `EmailService`
  - Rejects empty message bodies
  - Verifies that the destination email belongs to a saved contact
  - Creates an in-memory `Email` record
- `SMSMsgService`
  - Rejects empty SMS bodies
  - Verifies that the destination phone number belongs to a saved contact
  - Creates an in-memory `SMS` record

### Repositories

Database access is separated behind repository interfaces.

`IContactRepository` defines:

- Add
- Get all
- Get one
- Update
- Delete

`ICategoryRepository` defines:

- Get all categories
- Get contacts by category

The concrete repositories use asynchronous EF Core operations such as `AddAsync`, `ToListAsync`, `FirstOrDefaultAsync`, and `SaveChangesAsync`.

### Dependency wiring

Dependencies are created manually in `Program.cs`.

The application constructs the views, repositories, services, and controllers and passes dependencies through constructors. No dependency-injection container is used.

## Database

The application uses a local SQLite database located at:

```text
Phonebook-EF/Data/phonebook.db
```

`PhonebookContext` exposes:

```text
Contacts
Categories
```

The current schema contains:

### Contacts

- `Id` — primary key
- `FirstName`
- `LastName`
- `Email`
- `PhoneNumber`
- `CategoryId` — nullable foreign key to `Categories`

### Categories

- `Id` — primary key
- `Title`

A contact can reference a category through `CategoryId`.

## Entity Framework Core Migrations

The repository contains two migrations:

```text
20260806081102_InitialSeed
20260813094003_AddCategories
```

### `InitialSeed`

Creates the original `Contacts` table with:

- ID
- First name
- Last name
- Email
- Phone number

### `AddCategories`

Adds:

- The `Categories` table
- Nullable `CategoryId` on `Contacts`
- An index on `Contacts.CategoryId`
- A foreign-key relationship from `Contacts.CategoryId` to `Categories.Id`

### Automatic migration on startup

The application calls:

```csharp
PhonebookContext.InitializeDatabase();
```

which executes:

```csharp
context.Database.Migrate();
```

As a result, pending migrations are applied when the application starts.

Database initialization is wrapped in a `try/catch`; if initialization fails, the application prints the error and exits instead of continuing with an unavailable database.

## Seed Data

EF Core seeding is configured with `UseSeeding`.

When the corresponding tables are empty, the application seeds four categories:

```text
Family
Friends
Work
Other
```

and three sample contacts:

```text
Alice Johnson
Bob Smith
Clara Nguyen
```

The seed routine only inserts categories or contacts when no records of that type already exist.

> **Note:** The seeded sample phone numbers contain dashes (for example, `555-0101`), while the current phone validator accepts only 8–15 digits with an optional leading `+`. Consequently, those seeded phone numbers do not satisfy the application's current phone-number validation rule.

## Tests

Validation tests are stored in the separate `Phonebook.Tests` project and use NUnit with parameterized `TestCaseData`.

The current test suite covers:

- Missing or whitespace contact fields
- Missing category values
- Valid and invalid positive contact IDs
- Valid and invalid email formats
- Valid and invalid phone-number formats
- Empty and non-empty email bodies
- Empty and non-empty SMS bodies

The tests are focused on the static validation methods. Repository, service, controller, EF Core integration, and console-view behavior are not currently covered by automated tests.

Run the tests with:

```bash
dotnet test Phonebook.Tests/Phonebook.Tests.csproj
```

## Project Structure

```text
Phonebook/
├── Phonebook-EF/
│   ├── Controllers/
│   │   ├── AppController.cs
│   │   ├── CategoriesController.cs
│   │   ├── ContactsController.cs
│   │   ├── EmailController.cs
│   │   └── SMSMsgController.cs
│   ├── Data/
│   │   ├── PhonebookContext.cs
│   │   ├── SeedData.cs
│   │   └── phonebook.db
│   ├── Enums/
│   │   ├── CategoriesMenuOption.cs
│   │   ├── ContactsMenuOption.cs
│   │   └── MainMenuOption.cs
│   ├── Helpers/
│   │   ├── Formatters.cs
│   │   └── Validators.cs
│   ├── Migrations/
│   │   ├── 20260806081102_InitialSeed.cs
│   │   ├── 20260813094003_AddCategories.cs
│   │   └── PhonebookContextModelSnapshot.cs
│   ├── Models/
│   │   ├── Category.cs
│   │   ├── Contact.cs
│   │   ├── Email.cs
│   │   └── SMS.cs
│   ├── Repositories/
│   │   ├── Interfaces/
│   │   │   ├── ICategoryRepository.cs
│   │   │   └── IContactRepository.cs
│   │   ├── CategoryRepository.cs
│   │   └── ContactRepository.cs
│   ├── Services/
│   │   ├── CategoriesService.cs
│   │   ├── ContactsService.cs
│   │   ├── EmailService.cs
│   │   └── SMSMsgService.cs
│   ├── Views/
│   │   ├── Interfaces/
│   │   │   ├── IAppView.cs
│   │   │   ├── ICategoriesView.cs
│   │   │   ├── IContactsView.cs
│   │   │   ├── IEmailView.cs
│   │   │   └── ISMSView.cs
│   │   ├── AppView.cs
│   │   ├── CategoriesView.cs
│   │   ├── ContactsView.cs
│   │   ├── EmailView.cs
│   │   └── SMSView.cs
│   ├── Phonebook-EF.csproj
│   ├── Phonebook-EF.sln
│   └── Program.cs
├── Phonebook.Tests/
│   ├── Phonebook.Tests.csproj
│   └── ValidationTests.cs
└── .gitignore
```

## How to Run

### Prerequisites

Install the **.NET 9 SDK**.

### Clone the repository

```bash
git clone https://github.com/felikshetalia/Phonebook.git
cd Phonebook
```

### Restore dependencies

```bash
dotnet restore Phonebook-EF/Phonebook-EF.csproj
```

### Run the application

```bash
dotnet run --project Phonebook-EF/Phonebook-EF.csproj
```

On startup, the application locates the `Phonebook-EF` project directory, creates the `Data` directory if necessary, opens the SQLite database, applies pending EF Core migrations, and runs the configured seed routine.

## Working with Migrations

Migrations are normally applied automatically when the application starts.

If you want to manage them manually, install the EF Core CLI tool if it is not already available:

```bash
dotnet tool install --global dotnet-ef
```

Apply existing migrations:

```bash
dotnet ef database update --project Phonebook-EF/Phonebook-EF.csproj
```

Create a new migration after changing the EF Core model:

```bash
dotnet ef migrations add MigrationName --project Phonebook-EF/Phonebook-EF.csproj
```

## Current Limitations

The following behavior is intentionally documented to match the current code rather than the broader project challenge:

- Email messages are composed and validated locally but are not transmitted.
- SMS messages are composed and validated locally but are not transmitted.
- Categories are predefined and seeded; category CRUD is not implemented.
- A category selected during contact update is not currently persisted by `ContactRepository.Update`.
- Automated tests cover validator methods only.
- Seeded sample phone numbers use a format that does not pass the current phone-number validator.

## Project Reference

This application was developed as an implementation of The C# Academy's [Phone Book](https://thecsharpacademy.com/project/16/phonebook) project.
