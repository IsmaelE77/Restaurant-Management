namespace Restaurant_Management.Model;

public class Item
{
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }  
    public DateTime Added { get; set; } 
    public decimal Rating { get; set; }
    public int Category_Id { get; set; }
    public List<Item_Ingredient> Item_Ingredients { get; set; } = [];
    public List<Order_Item> Order_Items { get; set; } = [];
    public override string ToString() =>
        $"Item(Id: {Id}, Title: {Title}, Description: {Description}, Price: {Price}, Was Added in: {Added:yyyy/MM/dd}, " +
        $"Rating: {Rating}, Category_Id: {Category_Id}, Order_Items: {Order_Items.ConvertToString()}, " +
        $"Item_Ingredients: {Item_Ingredients.ConvertToString()})";
}
