namespace Restaurant_Management.Repository;

public class CategoryRepository(string connectionString) : ICategory
{
    public bool Add(Category category)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into \"Category\"(Name) Values('{category.Name}')", con);
        return cmd.ExecuteNonQuery() > 0;
    }
    public int? GetId(string name)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select Id from \"Category\" where Name='{name}'", con);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
    public Category? Get(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select Name from \"Category\" where Id='{Id}'", con);
        return new(Id, cmd.ExecuteScalar()?.ToString());
    }

    public IEnumerable<Category> GetAll()
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand("Select * from \"Category\"", con);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            yield return new Category(id, name);
        }
    }
    public bool Remove(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from \"Category\" where Id='{Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Category category)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update \"Category\" Set Name='{category.Name}' where Id='{category.Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }
}
