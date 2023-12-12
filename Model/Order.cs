namespace Restaurant_Management.Model
{
    public class Order
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Timestamp]
        public DateTime Date { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int TableId { get; set; }
        public int? ReceiptId { get; set; }  

        [Required(ErrorMessage = "At least one order item is required")]
        [MinLength(1, ErrorMessage = "At least one order item is required")]    
        public List<OrderItem> OrderItems {get; set;}

        public override string ToString()
        {
            return $"Order(Id: {Id}, Date: {Date}, Price: {Price}, EmployeeId: {EmployeeId}, TableId: {TableId}, " +
                   $"ReceiptId: {ReceiptId}, OrderItems: {OrderItemsToString()})";
        }

        private string OrderItemsToString()
        {
            if (OrderItems != null && OrderItems.Count > 0)
            {
                var orderItemsString = "[";

                foreach (var orderItem in OrderItems)
                {
                    orderItemsString += orderItem.ToString();
                }

                // Remove the trailing comma and space
                orderItemsString = orderItemsString.TrimEnd(',', ' ') + "]";
                
                return orderItemsString;
            }

            return "[]";
        }
    }
}
