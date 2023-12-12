using System;

namespace Restaurant_Management.Model
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "must have quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value")]
        public int Quantity { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage ="Order Id required")]
        public int OrderId { get; set; }
        [Required(ErrorMessage ="Item Id required")]
        public int ItemId { get; set; }

        public override string ToString()
        {
            return $"OrderItem(Id: {Id}, Quantity: {Quantity}, UnitPrice: {UnitPrice}, OrderId: {OrderId}, ItemId: {ItemId})";
        }

    }
}
