namespace Restaurant_Management.Repository;

public class TableRepository(string _connectionString) : ITable
{
    public bool Add(Table table)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("INSERT INTO \"Table\"(\"Number\", Status) VALUES (:pNumber, :Status) RETURNING Id INTO :Id", con);
        cmd.Parameters.Add(new OracleParameter(":pNumber", table.Number));
        cmd.Parameters.Add(new OracleParameter(":Status", table.Status));
        cmd.Parameters.Add(new OracleParameter(":Id", OracleDbType.Int32, ParameterDirection.ReturnValue));
        int result = cmd.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)cmd.Parameters[":Id"].Value;
        table.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Table? Get(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT \"Number\", Status FROM \"Table\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapTableFromReader(reader) : null;
    }

    public IEnumerable<Table> GetAll()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT Id, \"Number\", Status FROM \"Table\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapTableFromReader(reader);
    }

    public IEnumerable<Table> GetAvailableTables()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT Id, \"Number\", Status FROM \"Table\" WHERE Status = 'Available'", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int currentId = reader.GetInt32(reader.GetOrdinal("Id"));
            var number = reader.GetInt32(reader.GetOrdinal("\"Number\""));
            var status = reader.GetString(reader.GetOrdinal("Status"));
            yield return new Table { Id = currentId, Number = number, Status = status };
        }
    }

    public bool IsAvailable(int id)
    {
        var table = Get(id);
        return string.Equals(table?.Status, "Available", StringComparison.OrdinalIgnoreCase);
    }

    public bool Remove(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("DELETE FROM \"Table\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Table table)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("UPDATE \"Table\" SET \"Number\" = :Number, Status = :Status WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Number", table.Number));
        cmd.Parameters.Add(new OracleParameter(":Status", table.Status));
        cmd.Parameters.Add(new OracleParameter(":Id", table.Id));
        return cmd.ExecuteNonQuery() > 0;
    }

    private static Table MapTableFromReader(OracleDataReader reader)
    {
        return new Table
        {
            Id = Convert.ToInt32(reader["Id"]),
            Number = Convert.ToInt32(reader["Number"]),
            Status = reader["Status"].ToString(),
        };
    }
}