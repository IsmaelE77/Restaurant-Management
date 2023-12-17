namespace Restaurant_Management.Repository;

public class SupplierRepository(string _connectionString, ISupplier_Ingredient _supplier_Ingredient) : ISupplier
{
    public bool Add(Supplier supplier)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("INSERT INTO \"Supplier\" (Full_Name, Phone_Number) VALUES (:FullName, :PhoneNumber) RETURNING Id INTO :Id", con);
        cmd.Parameters.Add(new OracleParameter(":FullName", supplier.Full_Name));
        cmd.Parameters.Add(new OracleParameter(":PhoneNumber", supplier.Phone_Number));
        cmd.Parameters.Add(new OracleParameter(":Id", OracleDbType.Int32, ParameterDirection.ReturnValue));
        int result = cmd.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)cmd.Parameters[":Id"].Value;
        supplier.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Supplier? Get(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Supplier\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapSupplierFromReader(reader, _supplier_Ingredient) : null;
    }

    public Supplier? GetSupplierWithHighestPaymentForYear(int year)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "SELECT \"Supplier\".Id, \"Supplier\".Full_Name, \"Supplier\".Phone_Number, SUM(\"Supplier_Ingredient\".Price) AS TotalPayment " +
            "FROM \"Supplier\" " +
            "JOIN \"Supplier_Ingredient\" ON \"Supplier\".Id = \"Supplier_Ingredient\".Supplier_Id " +
            "WHERE EXTRACT(YEAR FROM \"Supplier_Ingredient\".\"Date\") = :year " +
            "GROUP BY \"Supplier\".Id, \"Supplier\".Full_Name, \"Supplier\".Phone_Number " +
            "ORDER BY TotalPayment DESC " +
            "FETCH FIRST 1 ROW ONLY", connection);
        command.Parameters.Add("year", OracleDbType.Int32).Value = year;
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapSupplierFromReader(reader, _supplier_Ingredient) : null;
    }

    public IEnumerable<Supplier> GetAll()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Supplier\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapSupplierFromReader(reader, _supplier_Ingredient);
    }

    public bool Remove(int id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("DELETE FROM \"Supplier\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", id));
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Supplier supplier)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("UPDATE \"Supplier\" SET Full_Name = :FullName, Phone_Number = :PhoneNumber WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":FullName", supplier.Full_Name));
        cmd.Parameters.Add(new OracleParameter(":PhoneNumber", supplier.Phone_Number));
        cmd.Parameters.Add(new OracleParameter(":Id", supplier.Id));
        foreach (var sup_Ing in supplier.Suppliers_Ingredients)
        {
            _supplier_Ingredient.Update(sup_Ing);
        }
        return cmd.ExecuteNonQuery() > 0;
    }
    private static Supplier MapSupplierFromReader(OracleDataReader reader, ISupplier_Ingredient _supplier_Ingredient)
    {
        return new Supplier
        {
            Id = Convert.ToInt32(reader["Id"]),
            Full_Name = reader["Full_Name"].ToString() ?? "",
            Phone_Number = reader["Phone_Number"].ToString() ?? "",
            Suppliers_Ingredients = _supplier_Ingredient.GetSupplier_IngredientsForSuppliers(Convert.ToInt32(reader["Id"])).ToList()
        };
    }
}
