using SampleMarket.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMarket.Business
{
	public interface ICartBO
	{
		Task<Guid> CreateCart();
		Task<IList<CartItem>> AddItem(Guid id, Product product);
		Task<IList<CartItem>> GetCart(Guid id);
		Task<int> GetSum(Guid id);
		Task<bool> Checkout(Guid id);
		Task<bool> DeleteCart(Guid id);
	}
}
