# PostOfficeProject
# ASP.NET Core 6.0 project

## Technologies

- ASP.NET Core 6.0
- Entity Framework Core
## Install package
- Microsoft.EntityFrameworkCore.SqlServer --version 6.0.22
- Microsoft.EntityFrameworkCore.Tools --version 6.0.22
- Microsoft.EntityFrameworkCore.Design --version 6.0.22
- Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.0.22

- Microsoft.AspNetCore.Authentication.JwtBearer --6.0.22

## How to configure and run
- Add new Migration initDB in Nuget Console package
- update database

## How to route the Areas MVC
- using route sample: {AreaName}/{Contoller}/{Action}/{id?}
- Example: If you want to go to the Money Order Page of Client Areas, this route will be followed: Client/Client/MoneyOrder
- Explaination: because the Client in Areas Folder which has Area Name: Client, getting into ClientController to get the view of MoneyOrder.cshtml 

  
