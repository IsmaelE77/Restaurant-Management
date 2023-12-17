namespace Restaurant_Management.Repository;

public class IngredientRepository(string _connectionString, ISupplier_Ingredient _supplier_Ingredient
    ,IItem_Ingredient _item_Ingredient) : IIngredient
{
    public bool Add(Ingredient ingredient)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using OracleCommand command = new(
            "INSERT INTO \"Ingredient\" (Name, Quantity) VALUES (:name, :quantity) RETURNING Id INTO :Id", connection);
        command.Parameters.Add("name", OracleDbType.Varchar2).Value = ingredient.Name;
        command.Parameters.Add("quantity", OracleDbType.Decimal).Value = ingredient.Quantity;
        OracleParameter IdParam = new(":Id", OracleDbType.Int32)
        {
            Direction = ParameterDirection.ReturnValue
        };
        command.Parameters.Add(IdParam);
        var result = command.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)command.Parameters[":Id"].Value;
        ingredient.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Ingredient? Get(int ingredientId)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using OracleCommand command = new("SELECT * FROM \"Ingredient\" WHERE Id = :ingredientId", connection);
        command.Parameters.Add("ingredientId", OracleDbType.Int32).Value = ingredientId;
        using OracleDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapIngredientFromReader(reader, _supplier_Ingredient, _item_Ingredient) : null;
    }

    public IEnumerable<Ingredient> GetAll()
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using OracleCommand command = new("SELECT * FROM \"Ingredient\"", connection);
        using OracleDataReader reader = command.ExecuteReader();
        while (reader.Read()) yield return MapIngredientFromReader(reader, _supplier_Ingredient, _item_Ingredient);
    }

    public bool Remove(int ingredientId)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using OracleCommand command = new("DELETE FROM \"Ingredient\" WHERE Id = :ingredientId", connection);
        command.Parameters.Add("ingredientId", OracleDbType.Int32).Value = ingredientId;
        return command.ExecuteNonQuery() > 0;
    }

    public bool Update(Ingredient ingredient)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using OracleCommand command = new(
            "UPDATE \"Ingredient\" SET Name = :name, Quantity = :quantity WHERE Id = :ingredientId", connection);
        command.Parameters.Add("name", OracleDbType.Varchar2).Value = ingredient.Name;
        command.Parameters.Add("quantity", OracleDbType.Decimal).Value = ingredient.Quantity;
        command.Parameters.Add("ingredientId", OracleDbType.Int32).Value = ingredient.Id;
        foreach (var sup_Ing in ingredient.Suppliers_Ingredients)
        {
            _supplier_Ingredient.Update(sup_Ing);
        }
        foreach (var Item_Ing in ingredient.Item_Ingredients)
        {
            _item_Ingredient.Update(Item_Ing);
        }
        return command.ExecuteNonQuery() > 0;
    }

    private static Ingredient MapIngredientFromReader(OracleDataReader reader, ISupplier_Ingredient _supplier_Ingredient, 
        IItem_Ingredient _item_Ingredient)
    {
        int Id = Convert.ToInt32(reader["Id"]);
        return new Ingredient
        {
            Id = Id,
            Name = Convert.ToString(reader["Name"]) ?? "",
            Quantity = Convert.ToDecimal(reader["Quantity"]),
            Suppliers_Ingredients = _supplier_Ingredient.GetSupplier_IngredientsForIngredients(Id).ToList(),
            Item_Ingredients = _item_Ingredient.GetItem_IngredientForIngredients(Id).ToList()
        };
    }
}
