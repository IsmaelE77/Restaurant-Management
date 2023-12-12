using Microsoft.VisualBasic;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace Restaurant_Management.Repository;

public class SectionRepository(string connectionString) : ISection
{
    public int Add(Section section)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into \"Section\"(Name) Values('{section.Name}')", con);
        return cmd.ExecuteNonQuery();
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
    public int RemoveById(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from \"Section\" where Id='{Id}'", con);
        return cmd.ExecuteNonQuery();
    }

    public int Remove(Section section)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from \"Section\" where Id='{section.Id}' and Name='{section.Name}'", con);
        return cmd.ExecuteNonQuery();
    }

    public int Update(Section section)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update \"Section\" Set Name='{section.Name}' where Id='{section.Id}'", con);
        return cmd.ExecuteNonQuery();
    }
}
