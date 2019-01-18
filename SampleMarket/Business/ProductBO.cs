using Microsoft.EntityFrameworkCore;
using SampleMarket.Data;
using SampleMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMarket.Business
{
	public class ProductBO : IProductBO
	{
		SampleMarketDbContext _context;

		/// <summary>
		/// Creates a ProductBO with database context
		/// </summary>
		/// <param name="context">Database context</param>
		public ProductBO(SampleMarketDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <summary>
		/// Gets the product with the id
		/// As id is a primary key, only one will be returned
		/// </summary>
		/// <param name="id">Product Id</param>
		/// <returns>Product</returns>
		public async Task<Product> GetProduct(int id)
		{
			// Get product where id matches passed id
			return await _context.Products.FindAsync(id);
		}

		/// <summary>
		/// Gets all products
		/// </summary>
		/// <param name="requireInventory">Whether inventory can be empty</param>
		/// <returns>List of products</returns>
		public async Task<IList<Product>> GetProducts(bool requireInventory = false)
		{
			List<Product> products = null;
			if (requireInventory)
			{
				// Get all products where inventory is greater than 0
				//products = _context.Products.Where(p => p.InventoryCount > 0).ToList();
				products = await _context.Products.Where(p => p.InventoryCount > 0).ToListAsync();
			}
			else
			{
				// Get all products
				products = await _context.Products.ToListAsync();
			}
			return products;
		}

		/// <summary>
		/// Lowers the inventory count of a product
		/// </summary>
		/// <param name="id">Product id</param>
		/// <returns>The updated product</returns>
		public async Task<Product> Purchase(int id)
		{
			// Get product with id
			var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
			if (product != null)
			{
				// Attach product
				// This allows us to update a single field
				// Otherwise, the database would mark ALL fields as changed
				_context.Attach(product);

				// Subtract 1 from inventory count
				product.InventoryCount--; 

				// Save changes
				await _context.SaveChangesAsync();
			}
			// return updated product
			return product;
		}
	}
}
