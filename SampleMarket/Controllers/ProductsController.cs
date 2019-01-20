using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleMarket.Business;
using SampleMarket.Models;

namespace SampleMarket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductBO _productBO;

		#region Constructors

		/// <summary>
		/// Instantiates a products controller
		/// </summary>
		/// <param name="productBO">Product business object</param>
		public ProductsController(IProductBO productBO)
		{
			_productBO = productBO;
		}

		#endregion

		#region HttpGet

		/// <summary>
		/// GET: api/Products
		/// Gets all products with no filtering
		/// </summary>
		/// <returns>List of found products</returns>
		[HttpGet(Name = "GetProducts")]
		public async Task<ActionResult<IList<Product>>> GetAll()
		{
			var products = await _productBO.GetAllProducts();
			if (products == null || products.Count == 0)
			{
				return NotFound();
			}
			return Ok(products);
		}

		/// <summary>
		/// GET: api/Products/Available
		/// Gets all products with no filtering
		/// </summary>
		/// <returns>List of found products</returns>
		[HttpGet("available", Name="ProductsAvailable")]
		public async Task<ActionResult<IList<Product>>> GetAvailable()
		{
			var products = await _productBO.GetAllProducts(true);
			if(products == null || products.Count == 0)
			{
				return NotFound();
			}
			return Ok(products);
		}


		/// <summary>
		/// GET: api/Products/3
		/// Gets the product with the id
		/// </summary>
		/// <param name="id">Product id</param>
		/// <returns>Product or invalid id</returns>
		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<ActionResult<Product>> Get(int id)
		{
			var product = await _productBO.GetProduct(id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		#endregion

		#region HttpPost

		/// <summary>
		/// Post: api/Products/5/purchase
		/// Puts an update to the inventory count of a product
		/// </summary>
		/// <param name="id">Product id</param>
		/// <returns>Product or invalid id</returns>
		[HttpPost("{id}/purchase", Name = "PurchaseProduct")]
		public async Task<ActionResult<Product>> Purchase(int id)
		{
			var product = await _productBO.Purchase(id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		#endregion

	}
}
