# Sample Market
---
> 🛍️ A simple RESTful shop API 🛍️

`Sample Market` is a RESTful shop API built in .NET Core with scalability and the cloud in mind. 
### Usage
Clone and build the project yourself by following the installation instructions at the bottom.

### 👜 Products
---
>./api/Products/

__Get All Products__

Retrieves all products, regardless of inventory count.
```HTTP
GET https://.../api/Products
Content-Type: application/json; charset=utf-8
```
__Get All *Available* Products__

Retrieves all products, which have available inventory.
```HTTP
GET https://.../api/Products/Available
Content-Type: application/json; charset=utf-8
```
__Get Single Product__

Retrieves a single product with given ID.
```HTTP
GET https://.../api/Products/{product-id}
Content-Type: application/json; charset=utf-8
```
### 🛒 Cart
---
>./api/Cart/

__Create Cart__

Returns a Guid which can be used to represent a cart
```HTTP
POST https://.../api/Cart
Content-Type: application/json; charset=utf-8
```
__Get Cart Contents__

Returns all items within the cart
```HTTP
GET https://.../api/Cart/{cart-id}
Content-Type: application/json; charset=utf-8
```
__Get Total Cost__

Returns the sum of all items in the cart
```HTTP
GET https://.../api/Cart/{cart-id}/Total
Content-Type: application/json; charset=utf-8
```
__Add Item to Cart__

Adds a product to your cart. Only requires the product ID to be sent in the JSON request. 
Returns the current cart.
```HTTP
POST https://.../api/Cart/{cart-id}/Add
Content-Type: application/json; charset=utf-8
Body:
{
    "id": 1
}
```
__Remove Item from Cart__

Removes a product from your cart. Only requires the product ID to be sent in the JSON request. 
Returns the current cart.
```HTTP
DELETE https:/.../api/Cart/{cart-id}/Remove
Content-Type: application/json; charset=utf-8
Body:
{
    "id": 1
}
```
__Checkout__

Deletes the cart and returns the total cost.
```HTTP
POST https://.../api/Cart/{cart-id}/Checkout
Content-Type: application/json; charset=utf-8
```
__Delete Cart__

Deletes the cart.
```HTTP
DELETE https://.../api/Cart/{cart-id}
Content-Type: application/json; charset=utf-8
```
## 💻 Tech
---
`Sample Market` is build with the following technology:

* [.NET Core](https://dotnet.microsoft.com/) - Microsoft's cross-platform, open source, modular implimentation of the .NET framework
* [Azure](https://azure.microsoft.com/) - Microsoft's cloud hosting solution for applications, SQL, etc.
* [SQL Server](https://www.microsoft.com/en-cy/sql-server/) - Database management - also hosted on Azure. 

## ⚡ Installation
---

`Sample Market` requires [.NET Core](https://dotnet.microsoft.com/download) v2.2 to run.

__SQL__

Seed a database with tables and sample data by running `seed.sql`.
In `appsettings.json` add a property for your connection string under AllowedHosts.
```json
"ConnectionStrings": {
    "SampleMarketDbContext": "<connectionstring>"
}
```
The app should now connect to SQL on startup.

__Building & Running__

It's easiest to build and debug from Visual Studio, but you can also use dotnet.

```cmd
> cd SampleMarket
> dotnet build
> dotnet run
```
Now navigate to the API in your web browser.
## 💭 Final Thoughts
---

__💲 Currency__

All montary values are stored as an `int` cents value in the market. That's because [you should never use floats (or doubles) for currency.](https://husobee.github.io/money/float/2016/09/23/never-use-floats-for-currency.html) I assume that whatever UI is going to consume the RESTful service will do that conversion themselves.

__🔒 Security__

The API is configured for HTTPS (which should just be the standard anyway). Also, the deployed code is managed with Azure Active Directory in the cloud so that the connection string doesn't have to deploy with it. Obviously, this doesn't work locally but it's one of the reasons I decided to build it with a cloud focus.

__🌱 Scalability__

(Almost) all endpoints perform asynchronous calls to the database. Although the app is small right now and certainly doesn't get enough requests, this allows a lot of extra room for scalability. Making the API async was a must for me when looking to the cloud.
