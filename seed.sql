/*
    Script: seed.sql
    Author: Zachary Merrill
    Creation Date: January 17, 2019
*/
USE SampleMarket_db; -- Use newly created database

DROP TABLE IF EXISTS Products; -- We want to overwrite any old tables
/* 
Create Products table to store all our products
Parameters:
    Id => [Primary Key] Identifier of each product
    Title => Name of the product
    Price => Cost of product stored in cents (integer)
    InventoryCount => Number of available products
*/
CREATE TABLE Products(
    Id int IDENTITY(1,1) PRIMARY KEY,
    Title varchar(255),
    Price int,
    InventoryCount int
);

/* Create some products in that table */
INSERT INTO Products (Title, Price, InventoryCount) VALUES 
('iPhone', 1099, 100),
('Google Pixel', 899, 40),
('Galaxy S9', 999, 0);

/* 
Create cart items table to store items
Parameters:
    Id => [Primary Key] Identifier of each cart_item
    ProductId => [Foreign Key] Identifier of the product
    Quantity => Number of items in cart
    CartId => Cart id associated with this item
*/
CREATE TABLE CartItems (
    Id int IDENTITY(1,1) PRIMARY KEY,
    ProductId int FOREIGN KEY REFERENCES Products(Id),
    Quantity int,
    CartId UNIQUEIDENTIFIER NOT NULL
);