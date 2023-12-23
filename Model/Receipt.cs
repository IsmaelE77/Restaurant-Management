namespace Restaurant_Management.Model;

public class Receipt
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public decimal Sub_Total { get; set; } = 0;
    public decimal Taxes { get; set; } = 0;
    public decimal Discount { get; set; } = 0;
    public decimal Total { get; set; } = 0;
    public int Table_Id { get; set; }
    public List<Order> Orders { get; set; } = [];
    public override string ToString() =>
        $"Receipt(Id: {Id}, Date: {Date}, Sub_Total: {Sub_Total}, Taxes: {Taxes}, Discount: {Discount}, " +
        $"Total: {Total}, Table_Id: {Table_Id}, Orders: {Orders.ConvertToString()})";
}