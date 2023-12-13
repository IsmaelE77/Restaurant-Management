namespace Restaurant_Management.Repository;

public class Supplier_IngredientRepository(string connectionString) : ISupplier_Ingredient
{
    public bool Add(Supplier_Ingredient item)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into Supplier_Ingredient(Ingredient_Id, Supplier_Id, Quantity, Price) Values('{item.Ingredient_Id}', '{item.Supplier_Id}', '{item.Quantity}', '{item.Price}') RETURNING Id into :Id", con);
        OracleParameter IdParam = new(":Id", OracleDbType.Int32)
        {
            Value = ParameterDirection.ReturnValue
        };
        cmd.Parameters.Add(IdParam);
        int result = cmd.ExecuteNonQuery();
        item.Id = Convert.ToInt32(IdParam.Value.ToString());
        return result > 0;
    }

    public Supplier_Ingredient? Get(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from Supplier_Ingredient where Id='{Id}'", con);
        using var reader = cmd.ExecuteReader();
        reader.Read();
        var price = reader.GetDecimal(reader.GetOrdinal("Price"));
        var quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
        var sup_Id = reader.GetInt32(reader.GetOrdinal("Supplier_Id"));
        var ing_Id = reader.GetInt32(reader.GetOrdinal("Ingredient_Id"));
        return new(Id, price, quantity, sup_Id, ing_Id);
    }

    public IEnumerable<Supplier_Ingredient> GetAll()
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from Supplier_Ingredient", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var Id = reader.GetInt32(reader.GetOrdinal("Id"));
            var price = reader.GetDecimal(reader.GetOrdinal("Price"));
            var quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
            var sup_Id = reader.GetInt32(reader.GetOrdinal("Supplier_Id"));
            var ing_Id = reader.GetInt32(reader.GetOrdinal("Ingredient_Id"));
            yield return new(Id, price, quantity, sup_Id, ing_Id);
        }
    }

    public IEnumerable<Supplier_Ingredient> GetSupplier_IngredientsForIngredients(int Ingredient_Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from Supplier_Ingredient where Ingredient_Id='{Ingredient_Id}'", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var Id = reader.GetInt32(reader.GetOrdinal("Id"));
            var price = reader.GetDecimal(reader.GetOrdinal("Price"));
            var quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
            var sup_Id = reader.GetInt32(reader.GetOrdinal("Supplier_Id"));
            var ing_Id = reader.GetInt32(reader.GetOrdinal("Ingredient_Id"));
            yield return new(Id, price, quantity, sup_Id, ing_Id);
        }
    }

    public IEnumerable<Supplier_Ingredient> GetSupplier_IngredientsForSuppliers(int Supplier_Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from Supplier_Ingredient where Supplier_Id='{Supplier_Id}'", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var Id = reader.GetInt32(reader.GetOrdinal("Id"));
            var price = reader.GetDecimal(reader.GetOrdinal("Price"));
            var quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
            var sup_Id = reader.GetInt32(reader.GetOrdinal("Supplier_Id"));
            var ing_Id = reader.GetInt32(reader.GetOrdinal("Ingredient_Id"));
            yield return new(Id, price, quantity, sup_Id, ing_Id);
        }
    }

    public decimal GetTotal_Pay(int Supplier_Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select Price from Supplier_Ingredient where Supplier_Id='{Supplier_Id}'", con);
        using var reader = cmd.ExecuteReader();
        decimal total = 0;
        while (reader.Read())
        {
            total += reader.GetDecimal(reader.GetOrdinal("Price"));
        }
        return total;
    }

    public int GetTotal_Quantity(int Ingredient_Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select Quantity from Supplier_Ingredient where Ingredient_Id='{Ingredient_Id}'", con);
        using var reader = cmd.ExecuteReader();
        int total = 0;
        while (reader.Read())
        {
            total += reader.GetInt32(reader.GetOrdinal("Quantity"));
        }
        return total;
    }

    public bool Remove(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from Supplier_Ingredient where Id='{Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Supplier_Ingredient item)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update Supplier_Ingredient  Set Price='{item.Price}', " +
            $"Quantity='{item.Quantity}', Supplier_Id='{item.Supplier_Id}', Ingredient_Id='{item.Ingredient_Id}'" +
            $" where Id='{item.Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }
}