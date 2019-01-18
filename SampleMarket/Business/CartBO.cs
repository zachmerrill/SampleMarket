using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleMarket.Models;

namespace SampleMarket.Business
{
	public class CartBO : ICartBO
	{
		public Task<IList<CartItem>> AddItem(Guid id, Product product)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Checkout(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Guid> CreateCart()
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteCart(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IList<CartItem>> GetCart(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<int> GetSum(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
