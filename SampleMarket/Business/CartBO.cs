using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleMarket.Data;
using SampleMarket.Models;

namespace SampleMarket.Business
{
	public class CartBO : ICartBO
	{
		private readonly SampleMarketDbContext _context;

		#region Constructor
		/// <summary>
		/// Creates a Cart business object with database context
		/// </summary>
		/// <param name="context">Database context</param>
		public CartBO(SampleMarketDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Add item to cart with id
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <param name="product">Product to add</param>
		/// <returns>List of current cart items</returns>
		public async Task<IList<CartItem>> AddItem(Guid id, Product product)
		{
			List<CartItem> cart = null;
			if(product != null && id != null)
			{
				// Check if that product id exists first
				var foundProduct = await _context.Products.Where(prod => prod.Id == product.Id).SingleOrDefaultAsync();
				if(foundProduct != null && foundProduct.InventoryCount > 0)
				{
					// Retrieve current list of cart items
					// We need this to determine if we're adding quantity or creating new
					// Also, we'll return this list to the client later
					cart = await _context.CartItems.Where(item => item.CartId == id).ToListAsync();

					int index = cart.FindIndex(item => item.ProductId == product.Id);
					if (index == -1) // Not found
					{
						// Create new item
						var item = new CartItem()
						{
							ProductId = product.Id,
							Quantity = 1,
							CartId = id
						};

						// Add cart item to db and our list
						await _context.CartItems.AddAsync(item);
						cart.Add(item);
					}
					else // Found, update quantity
					{
						// Get the item from database
						var existingItem = cart[index];
						var cartItem = await _context.CartItems.SingleOrDefaultAsync(item => item.Id == existingItem.Id);

						// Attach
						// This allows us to update a single field
						// Otherwise, the database would mark ALL fields as changed
						_context.Attach(cartItem);

						// Check if inventory count can support 1 more
						if(cartItem.Quantity + 1 < foundProduct.InventoryCount)
						{
							cartItem.Quantity++;
						}
					}

					// Save changes to database
					await _context.SaveChangesAsync();
				}
			}
			return cart;
		}

		/// <summary>
		/// Removes an item from the cart
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <param name="product">Product to remove</param>
		/// <returns>List of cart items</returns>
		public async Task<IList<CartItem>> RemoveItem(Guid id, Product product)
		{
			List<CartItem> cart = null;
			if (product != null && id != null)
			{
				// Get the item from the database
				var cartItem = await _context.CartItems.SingleOrDefaultAsync(item => item.ProductId == product.Id && item.CartId == id);
				
				if(cartItem != null)
				{
					// Check if quantity is 2 or more
					if (cartItem.Quantity > 1)
					{
						// Attach
						// This allows us to update a single field
						// Otherwise, the database would mark ALL fields as changed
						_context.Attach(cartItem);

						// Subtract 1
						cartItem.Quantity--;
					}
					else // Only 1 item in cart
					{
						// Remove that row entirely
						_context.Remove(cartItem);
					}

					await _context.SaveChangesAsync();
				}

				cart = await _context.CartItems.Where(item => item.CartId == id).ToListAsync();
			}
			return cart;
		}

		/// <summary>
		/// Checks out a cart by getting the sum and then 
		/// removing from the database
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <returns>Total cost of the items</returns>
		public async Task<int?> Checkout(Guid id)
		{
			int? cartTotal = null;
			if (id != null)
			{
				// Get the total that was paid
				cartTotal = await Sum(id);

				// Get all cart items with matching id
				var cart = await _context.CartItems.Where(item => item.CartId == id).ToListAsync();
				//var productIds = cart.Select(c => c.ProductId);

				// Get list of products
				var products = _context.Products.Where(prod => cart.Select(c => c.ProductId).Contains(prod.Id)).ToListAsync();

				// Slow and intensive, but it's the only way
				// with entity framework to update all items
				foreach (var product in products.Result)
				{
					product.InventoryCount -= cart.Find(c => c.ProductId == product.Id).Quantity;
				}

				// Clear up cart from database
				_context.CartItems.RemoveRange(cart);

				// Save
				await _context.SaveChangesAsync();
			}
			return cartTotal;
		}

		/// <summary>
		/// Creates a new Guid representing a cart id
		/// </summary>
		/// <returns>New guid</returns>
		public Guid CreateCart()
		{
			return Guid.NewGuid();
		}

		/// <summary>
		/// Removes all rows with matching cart id
		/// </summary>
		/// <param name="id">Car it</param>
		/// <returns>Boolean indicating success</returns>
		public async Task<bool> DeleteCart(Guid id)
		{
			bool success = false;
			if (id != null)
			{
				// Get all cart items with matching id
				var cart = await _context.CartItems.Where(item => item.CartId == id).ToListAsync();

				// Remove them all
				_context.CartItems.RemoveRange(cart);

				// Save
				int removedRows = await _context.SaveChangesAsync();

				if (removedRows > 0)
				{
					success = true;
				}
			}
			return success;
		}

		/// <summary>
		/// Gets the list of cart items with matching id
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <returns>List of cart items</returns>
		public async Task<IList<CartItem>> GetCart(Guid id)
		{
			List<CartItem> cart = null;
			if(id != null)
			{
				cart = await _context.CartItems.Where(item => item.CartId == id).ToListAsync();
			}
			return cart;
		}

		public async Task<int?> GetTotal(Guid id)
		{
			return await Sum(id);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Sums the cart with the given id
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <returns>Sum</returns>
		private async Task<int?> Sum(Guid id)
		{
			// Linq query to get sum
			var sum = (from c in _context.CartItems
					 where c.CartId == id
					 join p in _context.Products
					 on c.ProductId equals p.Id
					 select c.Quantity * p.Price);
			return await sum.SumAsync();
		}
		#endregion
	}
}
