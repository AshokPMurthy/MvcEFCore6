How to use Entity Framework Core in ASP.NET Core 6.0 MVC | Database First Approach
    URL : https://www.youtube.com/watch?v=QDCAIYs1Ktk


Create an "ASP.Net Core Web App" MVC Project:
    dotnet new mvc

Install Entity Framework :
    dotnet tool install --global dotnet-ef

Add package Entity Framework Design
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet tool install -g dotnet-aspnet-codegenerator
    
Execute the below command to verify the EF Installation:
    dotnet ef

Enter scaffold command :
  dotnet ef dbcontext scaffold "Server=(local)\sqlexpress;Database=Pubs;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models -t publishers -t authors -t titleauthor -t titles --force

For Generating db context only in the folder :
    dotnet ef dbcontext scaffold --context-dir Models

In Program.cs
---------------
    using MvcEFCore6.Models;
    using Microsoft.EntityFrameworkCore;

    // --apm Add Db Context
    builder.Services.AddDbContext<PubsContext> (options => {
        options.UseSqlServer (builder.Configuration.GetConnectionString ("constring"));
    });

In appsettings.json
--------------------
    ,
  "ConnectionStrings": {
    "constring" : "Server=(local)\\sqlexpress;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
