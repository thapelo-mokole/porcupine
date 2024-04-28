
# **Porcupine API**

This application was designed using a make shift n-layer architecture.
Below i list a some of the technologies that i used, i tried not to add uncessary packages or make use of scaffolding to avoid heavyness.

* Clone this repository: `git clone https://github.com/thapelo-mokole/porcupine.git`.

## Technologies
- .NET 6
- Swagger (Documentation)
- Entity Framework Core (SQL Server)
- AutoMapper

## **Getting Started**

## Database migrations

*Migrations will be applied automatically when the application gets ran for the first time*. 

The default connection server is set to **Server=(LocalDb)\\MSSQLLocalDB**

**However**, If you want to add new migrations to be applied to over the database, you can run the command below in the root folder.

```c#
dotnet ef migrations add Initial --project Porcupine.EntityFrameworkCore -o Migrations --startup-project Porcupine.Web.Host
```

If you want to apply existing migration, you can run the command below in the root folder.
```c#
dotnet ef database update --project Porcupine.EntityFrameworkCore --startup-project Porcupine.Web.Host
```


## Running

To run the application, set the *Porcupine.Web.Host* as the starting project.
