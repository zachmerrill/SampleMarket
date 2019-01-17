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
		public Product GetProduct(int id)
		{
			// Get product where id matches passed id
			return _context.Products.Where(p => p.Id == id).FirstOrDefault();
		}

		/// <summary>
		/// Gets all products
		/// </summary>
		/// <param name="requireInventory">Whether inventory can be empty</param>
		/// <returns>List of products</returns>
		public IList<Product> GetProducts(bool requireInventory)
		{
			List<Product> products = null;
			if (requireInventory)
			{
				// Get all products where inventory is greater than 0
				products = _context.Products.Where(p => p.InventoryCount > 0).ToList();
			}
			else
			{
				// Get all products
				products = _context.Products.ToList();
			}
			return products;
		}

		public bool Purchase(int id)
		{
			throw new NotImplementedException();
		}
	}
}
