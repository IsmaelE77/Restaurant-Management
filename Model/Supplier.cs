namespace Restaurant_Management.Model;

public class Supplier(int? _Id = null, string _FullName = "", string _PhoneNumber = "")
{
    public int? Id { get; set; } = _Id;
    public string Full_Name { get; set; } = _FullName;
    public string Phone_Number { get; set; } = _PhoneNumber;
    public List<Supplier_Ingredient> Suppliers_Ingredients { get; set; } = [];
    public override string ToString() => $"Supplier(Id: {Id}, Full_Name: {Full_Name}, Phone_Number: {Phone_Number}, " +
        $"Suppliers_Ingredients: {Suppliers_Ingredients.ConvertToString()})";
}
