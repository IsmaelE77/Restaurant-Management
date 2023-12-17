namespace Restaurant_Management.Repository;

public class Item_IngredientRepository(string _connectionString) : IItem_Ingredient
{
    public bool Add(Item_Ingredient item)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into \"Item_Ingredient\"(Item_Id, Ingredient_Id) Values('{item.Item_Id}', '{item.Ingredient_Id}') RETURNING Id into :Id", con);
        OracleParameter IdParam = new(":Id", OracleDbType.Int32)
        {
            Value = ParameterDirection.ReturnValue
        };
        cmd.Parameters.Add(IdParam);
        int result = cmd.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)cmd.Parameters[":Id"].Value;
        item.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Item_Ingredient? Get(int Id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Item_Ingredient\" where Id='{Id}'", con);
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapItem_IngredientFromReader(reader) : null;
    }

    public IEnumerable<Item_Ingredient> GetAll()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Item_Ingredient\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapItem_IngredientFromReader(reader);
    }

    public bool Remove(int Id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from \"Item_Ingredient\" where Id='{Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Item_Ingredient item)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update \"Item_Ingredient\" Set Ingredient_Id='{item.Ingredient_Id}', Item_Id='{item.Item_Id}' where Id='{item.Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }

    public IEnumerable<Item_Ingredient> GetItem_IngredientForIngredients(int Ingredient_Id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Item_Ingredient\" where Ingredient_Id='{Ingredient_Id}'", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapItem_IngredientFromReader(reader);
    }

    public IEnumerable<Item_Ingredient> GetItem_IngredientForItems(int Item_Id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Item_Ingredient\" where Item_Id='{Item_Id}'", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapItem_IngredientFromReader(reader);
    }

    private static Item_Ingredient MapItem_IngredientFromReader(OracleDataReader reader)
    {
        return new Item_Ingredient
        {
            Id = Convert.ToInt32(reader["Id"]),
            Item_Id = reader.GetInt32(reader.GetOrdinal("Item_Id")),
            Ingredient_Id = reader.GetInt32(reader.GetOrdinal("Ingredient_Id"))
        };
    }
}
