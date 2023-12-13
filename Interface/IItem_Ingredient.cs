namespace Restaurant_Management.Interface;

public interface IItem_Ingredient : ICrud<Item_Ingredient>
{
    public IEnumerable<Item_Ingredient> GetItem_IngredientForItems(int Item_Id);
    public IEnumerable<Item_Ingredient> GetItem_IngredientForIngredients(int Ingredient_Id);
}
