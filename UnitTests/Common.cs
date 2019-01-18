using SampleMarket.Models;
using System;
using System.Collections.Generic;

namespace UnitTests
{
	public static class Common
	{
		#region Product Helper Methods
		/// <summary>
		/// Creates sample products
		/// </summary>
		/// <param name="count">Number of products to create</param>
		/// <param name="available">If they should have inventory or not</param>
		/// <returns>List of products</returns>
		public static List<Product> CreateProducts(int count)
		{
			var products = new List<Product>();
			for (int i = 0; i < count; i++)
			{
				products.Add(CreateProduct(i));
			}
			return products;
		}

		/// <summary>
		/// Creates a product with the id
		/// </summary>
		/// <param name="id">Product id</param>
		/// <returns>Product object</returns>
		public static Product CreateProduct(int id)
		{
			return new Product
			{
				Id = id,
				Title = string.Format("Product{0}", id),
				Price = 100 + id,
				InventoryCount = 10 + id
			};
		}
		#endregion

		#region CartItem Helper Methods
		/// <summary>
		/// Creates a list of sample CartItems
		/// </summary>
		/// <param name="count">Number of items to create</param>
		/// <returns>List of CartItems</returns>
		public static List<CartItem> CreateCartItems(int count)
		{
			var cartItems = new List<CartItem>();
			for (int i = 0; i < count; i++)
			{
				cartItems.Add(CreateCartItem(i));
			}
			return cartItems;
		}

		/// <summary>
		/// Creates a sample CartItem
		/// </summary>
		/// <param name="id">CartItem id</param>
		/// <returns>CartItem object</returns>
		public static CartItem CreateCartItem(int id)
		{
			return new CartItem
			{
				Id = id,
				ProductId = 1 + id,
				Quantity = 100 + id,
				CartId = new Guid()
			};
		}
		#endregion
	}
}
