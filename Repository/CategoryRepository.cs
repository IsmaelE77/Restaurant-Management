namespace Restaurant_Management.Repository;

public class CategoryRepository(string _connectionString) : ICategory
{
    public bool Add(Category category)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("INSERT INTO \"Category\" (Name) VALUES (:Name) RETURNING Id INTO :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Name", category.Name));
        cmd.Parameters.Add(new OracleParameter(":Id", OracleDbType.Int32, ParameterDirection.ReturnValue));
        var result = cmd.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)cmd.Parameters[":Id"].Value;
        category.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }


    public int? GetId(string name)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT Id FROM \"Category\" WHERE Name = :Name", con);
        cmd.Parameters.Add(new OracleParameter(":Name", name));
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public Category? Get(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT Name FROM \"Category\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        var categoryName = cmd.ExecuteScalar()?.ToString() ?? "";
        return new Category { Id = id, Name = categoryName };
    }

    public IEnumerable<Category> GetAll()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT Id, Name FROM \"Category\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapCategoryFromReader(reader);
    }

    public bool Remove(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("DELETE FROM \"Category\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Category category)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("UPDATE \"Category\" SET Name = :Name WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Name", category.Name));
        cmd.Parameters.Add(new OracleParameter(":Id", category.Id));
        return cmd.ExecuteNonQuery() > 0;
    }

    private static Category MapCategoryFromReader(OracleDataReader reader)
    {
        return new Category
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = Convert.ToString(reader["Name"]) ?? "",
        };
    }
}
