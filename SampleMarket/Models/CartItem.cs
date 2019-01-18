using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMarket.Models
{
	/// <summary>
	/// Represents a single cart item
	/// </summary>
	public class CartItem
	{
		/// <summary>
		/// Cart item id
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Related product id
		/// </summary>
		public int ProductId { get; set; }
		/// <summary>
		/// Quantity
		/// </summary>
		public int Quantity { get; set; }
		/// <summary>
		/// Related cart id
		/// </summary>
		public Guid CartId { get; set; }
	}
}
