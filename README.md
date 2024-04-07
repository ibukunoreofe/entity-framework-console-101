# .NET Console Application with Entity Framework Core

This project is a .NET 8 console application that demonstrates how to perform CRUD operations using Entity Framework Core (EF Core), the latest version of Microsoft's recommended data access technology for .NET applications. The application manages a `Users` table in a SQL Server database, showcasing the use of EF Core for database interactions.

## Features

- Add new users to the database with random data
- List all users from the database
- Search for a user by ID and display their details
- Update a user's email by ID
- Delete a user by ID

## Prerequisites

- .NET 8 SDK
- SQL Server (Local or Remote)
- Visual Studio Code, Visual Studio, or another IDE that supports .NET development

## Getting Started

### Setting Up the Database

1. Ensure SQL Server is installed and accessible on your machine or remotely.
2. The application uses EF Core migrations to create the necessary table(s). Once the project is set up, you'll generate and apply migrations to set up your database schema.

### Configuring the Application

1. **Clone the repository** to your local machine.
2. **Open the project** in your preferred IDE.
3. **Install EF Core**: The project requires EF Core packages to be installed. These dependencies are typically included in the project file (.csproj) and will be restored when you build the project for the first time.

### Environment Configuration

1. **Database Connection String**: For security and flexibility, the database connection string should be stored in an external configuration file (e.g., `appsettings.json`) or as an environment variable. Ensure you configure this before running the application.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;"
      }
    }
    ```

   Replace `myServerAddress` and `myDataBase` with your SQL Server details. For production environments, consider using secure credentials and SSL.

2. **Update and Seed the Database**: Run the EF Core migrations to create your database schema:

    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

## Running the Application

- Navigate to the project directory in a terminal or command prompt.
- Execute `dotnet run` to build and run the application.
- Follow the on-screen prompts to interact with the database through the console application.

## Contributing

We welcome contributions to this project! Whether you find a bug, have a suggestion for an improvement, or want to contribute code, please feel free to open an issue or submit a pull request.

## Notes

- ADO.NET Entity Data Model requires .NET Framework

Encrypt=False in the connection string is to prevent this error. "A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)"

Additional Options
-Context: Specifies the name of the DbContext class to be generated. If omitted, a name is inferred from the database name.
-Schemas: Specifies the schemas of the tables to generate entity types for.
-Tables: Specifies the tables to generate entity types for. If omitted, all tables are included.
-DataAnnotations: Use this flag to specify that the scaffolding should use data annotations for configuration where possible, instead of Fluent API.
-Force: Forces overwriting of existing files.
-NoOnConfiguring: This option will skip generating the OnConfiguring method in the DbContext, which might be desirable if you plan to configure the context elsewhere or for security reasons to not include the connection string in your DbContext.

Use comma to separate when listing schemas or tables

```shell
Scaffold-DbContext -Connection "Server=xxxxxxxx.com\\xxxxxxxx,15433;Database=xxxxxxxxDB;User Id=sa;Password=xxxxxxxx;Encrypt=False" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir ModelsBase -Context ErDbContext -Schemas dbo,common -Force
```


## License

This project is open source and available under the [MIT License](LICENSE).
