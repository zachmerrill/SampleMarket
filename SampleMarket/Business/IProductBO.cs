using SampleMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMarket.Business
{
	public interface IProductBO
	{
		IList<Product> GetProducts(bool inventoryRequired);
		Product GetProduct(int id);
		bool Purchase(int id);
	}
}
