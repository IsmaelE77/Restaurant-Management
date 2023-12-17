namespace Restaurant_Management.Repository;

public class SectionRepository(string _connectionString) : ISection
{
    public bool Add(Section section)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"INSERT INTO \"Section\" (Name) VALUES (:Name) RETURNING Id INTO :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Name", section.Name));
        cmd.Parameters.Add(new OracleParameter(":Id", OracleDbType.Int32, ParameterDirection.ReturnValue));
        int result = cmd.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)cmd.Parameters[":Id"].Value;
        section.Id = oracleDecimalId.ToInt32();

        return result > 0;
    }

    public int? GetId(string name)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"SELECT Id FROM \"Section\" WHERE Name = :Name", con);
        cmd.Parameters.Add(new OracleParameter(":Name", name));
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public Section? Get(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"SELECT Name FROM \"Section\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        using var reader = cmd.ExecuteReader();
        return MapSectionFromReader(reader);
    }

    public IEnumerable<Section> GetAll()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Section\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapSectionFromReader(reader);
    }

    public bool Remove(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"DELETE FROM \"Section\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Section section)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand($"UPDATE \"Section\" SET Name = :Name WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Name", section.Name));
        cmd.Parameters.Add(new OracleParameter(":Id", section.Id));
        return cmd.ExecuteNonQuery() > 0;
    }
    private static Section MapSectionFromReader(OracleDataReader reader)
    {
        return new Section
        {
            Id = Convert.ToInt32(reader["Id"]),
            Name = reader["Name"].ToString() ?? "",
        };
    }

}