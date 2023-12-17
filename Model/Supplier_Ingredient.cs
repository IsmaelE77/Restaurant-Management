namespace Restaurant_Management.Model;

public class Supplier_Ingredient
{
    public int? Id { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; } 
    public int Quantity { get; set; }
    public int Supplier_Id { get; set; }
    public int Ingredient_Id { get; set; }
    public override string ToString() =>
        $"Ingredient(Id: {Id}, Price: {Price}, Quantity: {Quantity}, Supplier_Id: {Supplier_Id}, " +
        $"Ingredient_Id: {Ingredient_Id})"; 
}
