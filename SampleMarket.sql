/*
    Script: SampleMarket.sql
    Author: Zachary Merrill
    Creation Date: January 17, 2019
*/
CREATE DATABASE SampleMarket_db; -- Create database
USE SampleMarket_db; -- Use newly created database

DROP TABLE IF EXISTS Products; -- We want to overwrite any old tables
/* 
Create Products table to store all our products
Parameters:
    Id => Primary key identifier of each product
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

-- Create some products in that table
INSERT INTO Products (Title, Price, InventoryCount) VALUES 
('iPhone', 1099, 100),
('Google Pixel', 899, 40),
('Galaxy S9', 999, 0);