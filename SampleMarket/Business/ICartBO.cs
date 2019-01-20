using SampleMarket.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMarket.Business
{
	public interface ICartBO
	{
		Guid CreateCart();
		Task<IList<CartItem>> AddItem(Guid id, Product product);
		Task<IList<CartItem>> GetCart(Guid id);
		Task<int?> GetTotal(Guid id);
		Task<int?> Checkout(Guid id);
		Task<bool> DeleteCart(Guid id);
	}
}
