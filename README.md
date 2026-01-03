# npgsql_bug3698

## Run the solution
* There are 2 ASP.NET WebApi project, `dotnet9` and `dotnet10`.
* Under each project, open the `appsettings.json` configuration file and update the default connection string.
* Run the `dotnet9` project first to migrate the database.
* Using the UI, seed the database.
* Query the seeded data (no exceptions expected).
* Stop and run the `dotnet10` project.
* Query the seeded data (exception expected).