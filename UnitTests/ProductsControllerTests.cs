using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleMarket.Business;
using SampleMarket.Controllers;
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
				.ReturnsAsync(Common.CreateProducts(3));
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
				.ReturnsAsync(Common.CreateProducts(3));
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
				.ReturnsAsync(Common.CreateProduct(id));
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
			Assert.IsType<NotFoundResult>(response.Result);
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
				.ReturnsAsync(Common.CreateProduct(id));
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
			Assert.IsType<NotFoundResult>(response.Result);
		}
		#endregion
	}
}
