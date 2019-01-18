using SampleMarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMarket.Business
{
	public interface IProductBO
	{
		Task<IList<Product>> GetAllProducts(bool inventoryRequired = false);
		Task<Product> GetProduct(int id);
		Task<Product> Purchase(int id);
	}
}
