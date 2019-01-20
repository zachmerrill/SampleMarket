using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleMarket.Business;
using SampleMarket.Controllers;
using SampleMarket.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
	public class CartControllerTests
	{
		#region Test Get Method
		[Fact]
		public async Task Get_DatabaseHasCartWithId_OkResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			cartProxy.Setup(c => c.GetCart(It.IsAny<Guid>()))
				.ReturnsAsync(Common.CreateCartItems(3));
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Get(guid);

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public async Task Get_DatabaseDoesntHaveCartWithId_NotFoundResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Get(guid);

			// Assert
			Assert.IsType<NotFoundObjectResult>(response.Result);
		}
		#endregion

		#region Test GetTotalCost Method
		[Fact]
		public async Task GetTotalCost_DatabaseHasCartWithId_OkResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			cartProxy.Setup(c => c.GetTotal(It.IsAny<Guid>()))
				.ReturnsAsync(It.IsAny<int>());
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.GetTotalCost(guid);

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public async Task GetTotalCost_DatabaseDoesntHaveCartWithId_NotFoundResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.GetTotalCost(guid);

			// Assert
			Assert.IsType<NotFoundObjectResult>(response.Result);
		}
		#endregion

		#region Test Create Method
		[Fact]
		public void Create_CartCreatedSuccessful_OkResult()
		{
			// Assemble
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			cartProxy.Setup(c => c.CreateCart())
				.Returns(It.IsAny<Guid>());
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = controller.Create();

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}
		#endregion

		#region Test Add Method
		[Fact]
		public async Task Add_DatabaseHasCartWithId_OkResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			cartProxy.Setup(c => c.AddItem(It.IsAny<Guid>(), It.IsAny<Product>()))
				.ReturnsAsync(Common.CreateCartItems(3));
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Add(guid, Common.CreateProduct(1));

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public async Task Add_DatabaseDoesntHaveCartWithId_NotFoundResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Add(guid, Common.CreateProduct(1));

			// Assert
			Assert.IsType<NotFoundObjectResult>(response.Result);
		}
		#endregion

		#region Test Checkout Method
		[Fact]
		public async Task Checkout_DatabaseHasCartWithId_OkResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			cartProxy.Setup(c => c.Checkout(It.IsAny<Guid>()))
				.ReturnsAsync(It.IsAny<int>());
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Checkout(guid);

			// Assert
			Assert.IsType<OkResult>(response);
		}

		[Fact]
		public async Task Checkout_DatabaseDoesntHaveCartWithId_NotFoundResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Checkout(guid);

			// Assert
			Assert.IsType<NotFoundResult>(response);
		}
		#endregion

		#region Test Checkout Method
		[Fact]
		public async Task Delete_DatabaseHasCartWithId_OkResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			cartProxy.Setup(c => c.DeleteCart(It.IsAny<Guid>()))
				.ReturnsAsync(true);
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Delete(guid);

			// Assert
			Assert.IsType<OkResult>(response);
		}

		[Fact]
		public async Task Delete_DatabaseDoesntHaveCartWithId_NotFoundResult()
		{
			// Assemble
			Guid guid = new Guid();
			Mock<ICartBO> cartProxy = new Mock<ICartBO>();
			var controller = new CartController(cartProxy.Object);

			// Act
			var response = await controller.Delete(guid);

			// Assert
			Assert.IsType<NotFoundResult>(response);
		}
		#endregion

	}
}
