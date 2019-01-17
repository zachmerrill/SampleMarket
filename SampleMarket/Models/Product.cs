using System.ComponentModel.DataAnnotations;

namespace SampleMarket.Models
{
	/// <summary>
	/// Represents a single product
	/// </summary>
	public class Product
	{
		/// <summary>
		/// Product Id
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Product name
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Product cost
		/// </summary>
		public int Price { get; set; } // Stored as int (cents)
		/// <summary>
		/// Sum of products in inventory
		/// </summary>
		public int InventoryCount { get; set; }
	}
}
