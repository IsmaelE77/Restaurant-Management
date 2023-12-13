namespace Restaurant_Management.Repository;

public class TableRepository(string connectionString) : ITable
{
    public bool Add(Table table)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into \"Table\"(\"Number\", Status) Values('{table.Number}', '{table.Status}') RETURNING Id into :Id", con);
        OracleParameter IdParam = new(":Id", OracleDbType.Int32)
        {
            Value = ParameterDirection.ReturnValue
        };
        cmd.Parameters.Add(IdParam);
        int result = cmd.ExecuteNonQuery();
        table.Id = Convert.ToInt32(IdParam.Value.ToString());
        return result > 0;
    }

    public Table? Get(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select \"Number\", Status from \"Table\" where Id='{Id}'", con);
        using var reader = cmd.ExecuteReader();
        reader.Read();
        var number = reader.GetInt32(reader.GetOrdinal("\"Number\""));
        var status = reader.GetInt32(reader.GetOrdinal("Status"));
        return new(number, Id, status);
    }

    public IEnumerable<Table> GetAll()
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Table\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            var number = reader.GetInt32(reader.GetOrdinal("\"Number\""));
            var status = reader.GetInt32(reader.GetOrdinal("Status"));
            yield return new(number, id, status);
        }
    }

    public IEnumerable<Table> GetAvailableTables()
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Table\" where Status='0'", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            var number = reader.GetInt32(reader.GetOrdinal("\"Number\""));
            var status = reader.GetInt32(reader.GetOrdinal("Status"));
            yield return new(number, id, status);
        }
    }

    public bool IsAvailable(int Id)
    {
        var table = Get(Id);
        return table?.Status == 0;
    }

    public bool Remove(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from \"Table\" where Id='{Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Table table)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update \"Table\" Set \"Number\"='{table.Number}', Status='{table.Status}' where Id='{table.Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }
}
