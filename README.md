# Custom / Extended Identity using .NET Core and ADO.NET

By default, the ASP.NET Core Identity system stores user information in a SQL Server database using Entity Framework Core. For many apps, this approach works well. However, there's obviously times when a more custom approach needs to be taken. This can be as simple as a different database structure or just not wanting to mix with entity framework.

I have created this project using .NET Core 3.1. This project demonstrates how to create your own user and role store, giving you the power to retrieve and insert data via ado.net - rather than entity framework. I've isolated the identity sevices in to their own class library. This would enable you to plug it in to any project, without the additonal overhead of the MVC project I've used for the identity account management etc. 

The back-end code for the identity management system can all be found in the IdentityManager class library. You're able to see all in action by running the web project and registering as a new user, or login with an external provider i.e. Facebook. 
