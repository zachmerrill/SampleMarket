using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMarket.Business;

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
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _productBO.GetProducts());
		}

		/// <summary>
		/// GET: api/Products/Available
		/// Gets all products with no filtering
		/// </summary>
		/// <returns>List of found products</returns>
		[HttpGet("available", Name="Available")]
		public async Task<IActionResult> Available()
		{
			return Ok(await _productBO.GetProducts(true));
		}


		/// <summary>
		/// GET: api/Products/3
		/// Gets the product with the id
		/// </summary>
		/// <param name="id">Product id</param>
		/// <returns>Product or invalid id</returns>
		[HttpGet("{id}", Name = "Get")]
		public async Task<IActionResult> Get(int id)
		{
			var product = await _productBO.GetProduct(id);
			if(product == null)
			{
				return NotFound(id);
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
		[HttpPost("{id}/purchase", Name = "Purchase")]
		public async Task<IActionResult> Purchase(int id)
		{
			var product = await _productBO.Purchase(id);
			if (product == null)
			{
				return NotFound(id);
			}
			return Ok(product);
		}

		#endregion

	}
}
