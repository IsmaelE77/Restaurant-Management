namespace Restaurant_Management.Repository;

public class Supplier_IngredientRepository(string _connectionString) : ISupplier_Ingredient
{
    public bool Add(Supplier_Ingredient item)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand(
            "INSERT INTO \"Supplier_Ingredient\" (Ingredient_Id, Supplier_Id, \"Date\", Quantity, Price) " +
            "VALUES (:IngredientId, :SupplierId, :pDate, :Quantity, :Price) RETURNING Id INTO :Id", con);
        cmd.Parameters.Add(new OracleParameter(":IngredientId", item.Ingredient_Id));
        cmd.Parameters.Add(new OracleParameter(":SupplierId", item.Supplier_Id));
        cmd.Parameters.Add(new OracleParameter(":pDate", OracleDbType.TimeStamp) { Value = item.Date });
        cmd.Parameters.Add(new OracleParameter(":Quantity", item.Quantity));
        cmd.Parameters.Add(new OracleParameter(":Price", item.Price));
        cmd.Parameters.Add(new OracleParameter(":Id", OracleDbType.Int32, ParameterDirection.ReturnValue));
        int result = cmd.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)cmd.Parameters[":Id"].Value;
        item.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Supplier_Ingredient? Get(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Supplier_Ingredient\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapSupplierIngredientFromReader(reader) : null;
    }

    public IEnumerable<Supplier_Ingredient> GetAll()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Supplier_Ingredient\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapSupplierIngredientFromReader(reader);
    }

    public IEnumerable<Supplier_Ingredient> GetSupplier_IngredientsForIngredients(int ingredientId)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Supplier_Ingredient\" WHERE Ingredient_Id = :IngredientId", con);
        cmd.Parameters.Add(new OracleParameter(":IngredientId", ingredientId));
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapSupplierIngredientFromReader(reader);
    }

    public IEnumerable<Supplier_Ingredient> GetSupplier_IngredientsForSuppliers(int supplierId)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Supplier_Ingredient\" WHERE Supplier_Id = :SupplierId", con);
        cmd.Parameters.Add(new OracleParameter(":SupplierId", supplierId));
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapSupplierIngredientFromReader(reader);
    }

    public decimal GetTotal_Pay(int supplierId)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT Price FROM \"Supplier_Ingredient\" WHERE Supplier_Id = :SupplierId", con);
        cmd.Parameters.Add(new OracleParameter(":SupplierId", supplierId));
        using var reader = cmd.ExecuteReader();
        decimal total = 0;
        while (reader.Read())
        {
            total += reader.GetDecimal(reader.GetOrdinal("Price"));
        }
        return total;
    }

    public int GetTotal_Quantity(int ingredientId)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT Quantity FROM \"Supplier_Ingredient\" WHERE Ingredient_Id = :IngredientId", con);
        cmd.Parameters.Add(new OracleParameter(":IngredientId", ingredientId));
        using var reader = cmd.ExecuteReader();
        int total = 0;
        while (reader.Read())
        {
            total += reader.GetInt32(reader.GetOrdinal("Quantity"));
        }
        return total;
    }

    public bool Remove(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("DELETE FROM \"Supplier_Ingredient\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Supplier_Ingredient item)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand(
            "UPDATE \"Supplier_Ingredient\" SET Price = :Price, Quantity = :Quantity, Supplier_Id = :SupplierId, Ingredient_Id = :IngredientId " +
            "WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Price", item.Price));
        cmd.Parameters.Add(new OracleParameter(":Quantity", item.Quantity));
        cmd.Parameters.Add(new OracleParameter(":SupplierId", item.Supplier_Id));
        cmd.Parameters.Add(new OracleParameter(":IngredientId", item.Ingredient_Id));
        cmd.Parameters.Add(new OracleParameter(":Id", item.Id));
        return cmd.ExecuteNonQuery() > 0;
    }
    private static Supplier_Ingredient MapSupplierIngredientFromReader(OracleDataReader reader)
    {
        return new Supplier_Ingredient
        {
            Id = Convert.ToInt32(reader["Id"]),
            Price = Convert.ToDecimal(reader["Price"]),
            Quantity = Convert.ToInt32(reader["Quantity"]),
            Ingredient_Id = Convert.ToInt32(reader["Ingredient_Id"]),
            Supplier_Id = Convert.ToInt32(reader["Supplier_Id"]),
            Date = Convert.ToDateTime(reader["Date"]),
        };
    }
}