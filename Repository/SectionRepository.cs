namespace Restaurant_Management.Repository;

public class SectionRepository(string connectionString) : ISection
{
    public bool Add(Section section)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into \"Section\"(Name) Values('{section.Name}')", con);
        return cmd.ExecuteNonQuery() > 0;
    }
    public int? GetId(string name)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select Id from \"Section\" where Name='{name}'", con);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
    public Section? Get(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select Name from \"Section\" where Id='{Id}'", con);
        return new(Id, cmd.ExecuteScalar()?.ToString());
    }

    public IEnumerable<Section> GetAll()
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand("Select * from \"Section\"", con);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            yield return new Section(id, name);
        }
    }
    public bool Remove(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from \"Section\" where Id='{Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Section section)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update \"Section\" Set Name='{section.Name}' where Id='{section.Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }
}