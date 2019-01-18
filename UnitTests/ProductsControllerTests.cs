using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleMarket.Business;
using SampleMarket.Controllers;
using SampleMarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
	public class ProductsControllerTests
	{
		#region Test GetAll Method
		[Fact]
		public async Task GetAll_DatabaseHasProducts_OkResult()
		{
			// Assemble
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			productProxy.Setup(p => p.GetAllProducts(It.IsAny<bool>()))
				.ReturnsAsync(CreateProducts(3));
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.GetAll();

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public async Task GetAll_DatabaseEmpty_NotFoundResult()
		{
			// Assemble
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.GetAll();

			// Assert
			Assert.IsType<NotFoundResult>(response.Result);
		}
		#endregion

		#region Test GetAvailable Method
		[Fact]
		public async Task GetAvailable_DatabaseHasProductsWithInventory_OkResult()
		{
			// Assemble
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			productProxy.Setup(p => p.GetAllProducts(It.IsAny<bool>()))
				.ReturnsAsync(CreateProducts(3));
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.GetAvailable();

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public async Task GetAvailable_DatabaseEmpty_NotFoundResult()
		{
			// Assemble
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.GetAvailable();

			// Assert
			Assert.IsType<NotFoundResult>(response.Result);
		}
		#endregion

		#region Test Get Method
		[Fact]
		public async Task Get_DatabaseHasProductId_OkResult()
		{
			// Assemble
			int id = 1;
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			productProxy.Setup(p => p.GetProduct(It.IsAny<int>()))
				.ReturnsAsync(CreateProduct(id));
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.Get(id);

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public async Task Get_DatabaseDoesntHaveProductId_NotFoundResult()
		{
			// Assemble
			int id = 1;
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.Get(id);

			// Assert
			Assert.IsType<NotFoundObjectResult>(response.Result);
		}
		#endregion

		#region Test Purchase Method
		[Fact]
		public async Task Purchase_DatabaseHasProductId_OkResult()
		{
			// Assemble
			int id = 1;
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			productProxy.Setup(p => p.Purchase(It.IsAny<int>()))
				.ReturnsAsync(CreateProduct(id));
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.Purchase(id);

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public async Task Purchase_DatabaseDoesntHaveProductId_NotFoundResult()
		{
			// Assemble
			int id = 1;
			Mock<IProductBO> productProxy = new Mock<IProductBO>();
			var controller = new ProductsController(productProxy.Object);

			// Act
			var response = await controller.Purchase(id);

			// Assert
			Assert.IsType<NotFoundObjectResult>(response.Result);
		}
		#endregion

		#region Private Helper Methods
		/// <summary>
		/// Creates sample products
		/// </summary>
		/// <param name="count">Number of products to create</param>
		/// <param name="available">If they should have inventory or not</param>
		/// <returns>List of products</returns>
		private List<Product> CreateProducts(int count)
		{
			var products = new List<Product>();
			for (int i = 0; i < count; i++)
			{
				products.Add(CreateProduct(i));
			}
			return products;
		}

		/// <summary>
		/// Creates a product with the id
		/// </summary>
		/// <param name="id">Product id</param>
		/// <returns>Product object</returns>
		private Product CreateProduct(int id)
		{
			return new Product
			{
				Id = id,
				Title = string.Format("Product{0}", id),
				Price = 100 + id,
				InventoryCount = 10 + id
			};
		}
		#endregion
	}
}
