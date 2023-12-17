namespace Restaurant_Management.Model;

public class Ingredient
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public List<Supplier_Ingredient> Suppliers_Ingredients { get; set; } = [];
    public List<Item_Ingredient> Item_Ingredients { get; set; } = [];
    public override string ToString() =>
        $"Ingredient(Id: {Id}, Name: {Name}, Quantity: {Quantity}, Suppliers_Ingredients {Suppliers_Ingredients.ConvertToString()})";
}
