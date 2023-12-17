namespace Restaurant_Management.Model;

public class Item_Ingredient
{
    public int Id { get; set; }
    public int Item_Id { get; set; }
    public int Ingredient_Id { get; set; }
    public override string ToString() =>
        $"Item_Ingredient(Id: {Id}, Item_Id: {Item_Id}, Ingredient_Id: {Ingredient_Id})";
}
