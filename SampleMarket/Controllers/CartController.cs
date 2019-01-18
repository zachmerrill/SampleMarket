﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleMarket.Business;
using SampleMarket.Models;

namespace SampleMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBO _cartBO;

		#region Constructor

		public CartController(ICartBO cartBO)
        {
            _cartBO = cartBO ?? throw new ArgumentNullException(nameof(cartBO));
        }

		#endregion

		#region HttpGet

		/// <summary>
		/// GET: api/Cart/1111-AAAA-2222-BBBB-3333
		/// Gets all cart contents
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <returns>List of cart items</returns>
		[HttpGet("{id}")]
        public async Task<ActionResult<IList<CartItem>>> Get(Guid id)
        {
			var cart = await _cartBO.GetCart(id);

            if (cart == null)
            {
                return NotFound(id);
            }

            return Ok(cart);
        }

		/// <summary>
		/// GET: api/Cart/1111-AAAA-2222-BBBB-3333/Total
		/// Gets the sum of all cart items
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <returns>Sum (Total)</returns>
		[HttpGet("{id}/Total")]
		public async Task<ActionResult<int>> GetTotalCost(Guid id)
		{
			int? sum = await _cartBO.GetSum(id);

			if(sum == null)
			{
				return NotFound(id);
			}

			return Ok(sum);
		}

		#endregion

		#region HttpPost

		/// <summary>
		/// POST: api/Cart
		/// Creates a cart
		/// </summary>
		/// <returns>The cart id</returns>
		[HttpPost]
        public async Task<ActionResult<Guid>> Create()
        {
			return Ok(await _cartBO.CreateCart());
        }

		/// <summary>
		/// POST: api/Cart/1111-AAAA-2222-BBBB-3333
		/// Adds a product to the cart id
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <param name="product">Product json</param>
		/// <returns>Updated cart</returns>
		[HttpPost("{id}")]
		public async Task<ActionResult<IList<CartItem>>> Add(Guid id, [FromBody]Product product)
		{
			var cart = await _cartBO.AddItem(id, product);
			if(cart == null)
			{
				return NotFound(id);
			}
			// Return the updated cart
			return Ok(cart);
		}

		/// <summary>
		/// POST: api/Cart/1111-AAAA-2222-BBBB-3333/Checkout
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <returns>Id</returns>
		[HttpPost("{id}/Checkout")]
		public async Task<ActionResult> Checkout(Guid id)
		{
			bool success = await _cartBO.Checkout(id);
			if (!success)
			{
				return NotFound();
			}
			return Ok();
		}

		#endregion

		#region HttpDelete

		/// <summary>
		/// DELETE: api/Cart/1111-AAAA-2222-BBBB-333
		/// Deletes a cart
		/// </summary>
		/// <param name="id">Cart id</param>
		/// <returns>Id</returns>
		[HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
			bool success = await _cartBO.DeleteCart(id);
			if (!success)
			{
				return NotFound();
			}
			return Ok();
		}

		#endregion
	}
}
