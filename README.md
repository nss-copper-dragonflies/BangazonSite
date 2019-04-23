# Welcome to Bangazon!

## Overview

Bangazon is a commercial site where users can buy and sell products. 

## Setup

> **Pick on person from your team to follow these steps. No one else should touch anything at this point.**


### Git and SQL Server Configuration

1. Clone this repository to your machine.
1. Create a new repository on your team's Github organization named `BangazonSite`.
1. Copy the connection string for your repo.
1. From your project directory, execute the following commands
    ```sh
    git remote remove origin
    git remote add origin <paste your new Github URL here>
    ```
1. Push up the master branch to your new remote origin
1. Create a branch named `initial-setup`.
1. Open Visual Studio and load the solution file

### Seeding the Database

You will want to seed your database with some default values. Open the `ApplicationDbContext.cs` file and scroll all the way to the bottom. You will find the the following code.

```cs
modelBuilder.Entity<PaymentType> ().HasData (...)
```

The `HasData()` method lets you create one, or more, instances of a database model. Those instances will be turned into `INSERT INTO` SQL statements when you generate a migration either through the Package Manager Console with `Add-Migration MigrationName` or through the command line with `dotnet ef migrations add MigrationName`.

The boilerplate project has one user, two payment types, two product types, two products, one order, and two products on the order set up for you already.

Review that code with your team and if the team decides that they want more seeded data, add the new objects now.

### Generating the Database

Once your appsettings are updated and you've entered in some seed data, you should generate your database.

1. Go to the Package Manager Console in Visual Studio.
1. Use the `Add-Migration BangazonTables` command.
1. Once Visual Studio shows you the migration file, execute `Update-Database` to generate your tables.
1. Use the SQL Server Object Explorer to verify that everything worked as expected.


## Features
---

### Products
If the user would like to sell a product they are presented with the option to "Sell a Product" in the menu if they are logged in. All fields must be input with valid information in order for the user to create a new product. When the product has been created the user is redirected to a page that displays the details of the product that was just created.

If a user is viewing the list of Products, they will be able to see the product details from this view. If they wish to view the details of a specific product, they may click the "Details" link for that product and they will be presented with a view containing that product only and the details for it. 

If a user wishes to add a product to their order from the product list or product details views, they may choose the "Add to Order" allowance. 
