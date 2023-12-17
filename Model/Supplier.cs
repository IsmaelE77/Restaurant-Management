namespace Restaurant_Management.Model;

public class Supplier
{
    public int? Id { get; set; }
    public string Full_Name { get; set; } = string.Empty;
    public string Phone_Number { get; set; } = string.Empty;
    public List<Supplier_Ingredient> Suppliers_Ingredients { get; set; } = [];
    public override string ToString() => 
        $"Supplier(Id: {Id}, Full_Name: {Full_Name}, Phone_Number: {Phone_Number}, " +
        $"Suppliers_Ingredients: {Suppliers_Ingredients.ConvertToString()})";
}
