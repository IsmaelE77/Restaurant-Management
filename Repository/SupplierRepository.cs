using System.Data;

namespace Restaurant_Management.Repository;

public class SupplierRepository(string connectionString) : ISupplier
{
    public bool Add(Supplier supplier)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into \"Supplier\"(Full_Name, Phone_Number) Values('{supplier.Full_Name}', '{supplier.Phone_Number}') RETURNING Id into :Id", con);
        OracleParameter IdParam = new(":Id", OracleDbType.Int32)
        {
            Value = ParameterDirection.ReturnValue
        };
        cmd.Parameters.Add(IdParam);
        int result = cmd.ExecuteNonQuery();
        supplier.Id = Convert.ToInt32(IdParam.Value.ToString());
        return result > 0;
    }

    public Supplier? Get(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Supplier\" where Id='{Id}'", con);
        using var reader = cmd.ExecuteReader();
        reader.Read();
        var Full_Name = reader.GetString(reader.GetOrdinal("Full_Name"));
        var Phone_Number = reader.GetString(reader.GetOrdinal("Phone_Number"));
        return new(Id, Full_Name, Phone_Number);
    }

    public Supplier? GetSupplierWithHighestPaymentForYear(int year)
    {
        using OracleConnection connection = new OracleConnection(connectionString);
        connection.Open();

        using OracleCommand command = new OracleCommand(
            $"SELECT \"Supplier\".Id, \"Supplier\".Full_Name, \"Supplier\".Phone_Number, SUM(\"Supplier_Ingredient\".Price) AS TotalPayment " +
            $"FROM \"Supplier\" " +
            $"JOIN \"Supplier_Ingredient\" ON \"Supplier\".Id = \"Supplier_Ingredient\".Supplier_Id " +
            $"WHERE EXTRACT(YEAR FROM \"Supplier_Ingredient\".\"Date\") = :year " +
            $"GROUP BY \"Supplier\".Id, \"Supplier\".Full_Name, \"Supplier\".Phone_Number " +
            $"ORDER BY TotalPayment DESC " +
            $"FETCH FIRST 1 ROW ONLY", connection);
        command.Parameters.Add("year", OracleDbType.Int32).Value = year;

        using OracleDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            string fullName = reader.GetString(reader.GetOrdinal("Full_Name"));
            string phoneNumber = reader.GetString(reader.GetOrdinal("Phone_Number"));
            return new(id, fullName, phoneNumber);
        }

        return null;
    }


    public IEnumerable<Supplier> GetAll()
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from \"Supplier\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var Id = reader.GetInt32(reader.GetOrdinal("Id"));
            var Full_Name = reader.GetString(reader.GetOrdinal("Full_Name"));
            var Phone_Number = reader.GetString(reader.GetOrdinal("Phone_Number"));
            yield return new(Id, Full_Name, Phone_Number);
        }
    }

    public bool Remove(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from \"Supplier\" where Id='{Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Supplier supplier)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update \"Table\" Set Full_Name='{supplier.Full_Name}', Phone_Number='{supplier.Phone_Number}'' where Id='{supplier.Id}'", con);
        return cmd.ExecuteNonQuery() > 0;
    }
}
