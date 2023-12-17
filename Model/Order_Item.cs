namespace Restaurant_Management.Model;

public class Order_Item
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int Order_Id { get; set; }
    public int Item_Id { get; set; }
    public override string ToString() => 
        $"OrderItem(Id: {Id}, Quantity: {Quantity}, UnitPrice: {UnitPrice}, Order_Id: {Order_Id}, Item_Id: {Item_Id})";
}
