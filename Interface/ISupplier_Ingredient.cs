namespace Restaurant_Management.Interface;

public interface ISupplier_Ingredient : ICrud<Supplier_Ingredient>
{
    public decimal GetTotal_Pay(int Supplier_Id);
    public int GetTotal_Quantity(int Ingredient_Id);
    public IEnumerable<Supplier_Ingredient> GetSupplier_IngredientsForSuppliers(int Supplier_Id);
    public IEnumerable<Supplier_Ingredient> GetSupplier_IngredientsForIngredients(int Ingredient_Id);
}