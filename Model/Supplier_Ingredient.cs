namespace Restaurant_Management.Model;

public class Supplier_Ingredient(int? _Id = null, decimal _Price = 0, int _Quantity = 1, int _Supplier_Id = 0, int _Ingredient_Id = 0)
{
    public int? Id { get; set; } = _Id;
    public decimal Price { get; set; } = _Price;
    public DateTime Date { get; set; } 
    public int Quantity { get; set; } = _Quantity;
    public int Supplier_Id { get; set; } = _Supplier_Id;
    public int Ingredient_Id { get; set; } = _Ingredient_Id;
    public override string ToString() => $"Ingredient(Id: {Id}, Price: {Price}, Quantity: {Quantity}, " +
        $"Supplier_Id: {Supplier_Id}, Ingredient_Id: {Ingredient_Id})";
}
