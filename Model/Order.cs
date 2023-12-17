namespace Restaurant_Management.Model;

public class Order
{
    public int? Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public int Employee_Id { get; set; }
    public int Table_Id { get; set; }
    public int? Receipt_Id { get; set; }
    public List<Order_Item> Order_Items { get; set; } = [];

    public override string ToString() =>
        $"Order(Id: {Id}, Date: {Date}, Price: {Price}, Employee_Id: {Employee_Id}, Table_Id: {Table_Id}, " +
               $"Receipt_Id: {Receipt_Id}, Order_Items: {Order_Items.ConvertToString()})";
}
