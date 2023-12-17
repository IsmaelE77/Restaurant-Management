namespace Restaurant_Management.Repository;

public class ReceiptRepository(string _connectionString, IOrder _order) : IReceipt
{
    public bool Add(Receipt receipt)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("INSERT INTO \"Receipt\" (\"Date\", Sub_Total, Taxes, Discount, Total, " +
            "Table_Id) VALUES (:Id, :Date, :Sub_Total, :Taxes, :Discount, :Total, :Table_Id) RETURNING Id INTO :Id", con);
        OracleParameter dateParam = new(":Date", OracleDbType.TimeStamp)
        {
            Value = receipt.Date
        };
        cmd.Parameters.Add(dateParam);
        cmd.Parameters.Add(":Sub_Total", OracleDbType.Decimal).Value = receipt.Sub_Total;
        cmd.Parameters.Add(":Taxes", OracleDbType.Decimal).Value = receipt.Taxes;
        cmd.Parameters.Add(":Discount", OracleDbType.Decimal).Value = receipt.Discount;
        cmd.Parameters.Add(":Total", OracleDbType.Decimal).Value = receipt.Total;
        cmd.Parameters.Add(":Table_Id", OracleDbType.Int32).Value = receipt.Table_Id;
        int result = cmd.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)cmd.Parameters[":Id"].Value;
        receipt.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }


    public Receipt? Get(int Id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Receipt\" WHERE Id = :Id", con);
        cmd.Parameters.Add(new OracleParameter(":Id", Id));
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapReceiptFromReader(reader, _order) : null;
    }

    public IEnumerable<Receipt> GetAll()
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Receipt\"", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapReceiptFromReader(reader, _order);
    }

    public IEnumerable<Receipt> GetReceiptByBetween2Dates(DateTime from, DateTime to)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Receipt\" WHERE \"Date\" BETWEEN :From AND :To", con);
        cmd.Parameters.Add(new OracleParameter(":From", from));
        cmd.Parameters.Add(new OracleParameter(":To", to));
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapReceiptFromReader(reader, _order);
    }


    public IEnumerable<Receipt> GetReceiptByTable(int Table_Id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("SELECT * FROM \"Receipt\" WHERE Table_Id = :Table_Id", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) yield return MapReceiptFromReader(reader, _order);
    }

    public bool Remove(int Id)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var command = new OracleCommand("DELETE FROM \"Receipt\" WHERE Id = :Id", con);
        command.Parameters.Add(new OracleParameter(":Id", Id));
        return command.ExecuteNonQuery() > 0;
    }

    public bool Update(Receipt receipt)
    {
        using var con = new OracleConnection(_connectionString);
        con.Open();
        using var cmd = new OracleCommand("UPDATE \"Receipt\" SET \"Date\" = :Date, Sub_Total = :Sub_Total, " +
            "Taxes = :Taxes, Discount = :Discount, Total = :Total, Table_Id = :Table_Id WHERE Id = :Id", con);
        OracleParameter dateParam = new(":Date", OracleDbType.TimeStamp)
        {
            Value = receipt.Date
        };
        cmd.Parameters.Add(dateParam);
        cmd.Parameters.Add(":Sub_Total", OracleDbType.Decimal).Value = receipt.Sub_Total;
        cmd.Parameters.Add(":Taxes", OracleDbType.Decimal).Value = receipt.Taxes;
        cmd.Parameters.Add(":Discount", OracleDbType.Decimal).Value = receipt.Discount;
        cmd.Parameters.Add(":Total", OracleDbType.Decimal).Value = receipt.Total;
        cmd.Parameters.Add(":Table_Id", OracleDbType.Int32).Value = receipt.Table_Id;
        cmd.Parameters.Add(":Id", OracleDbType.Int32).Value = receipt.Id;
        foreach (var Order_Item in receipt.Orders)
        {
            _order.Update(Order_Item);
        }
        return cmd.ExecuteNonQuery() > 0;
    }

    private static Receipt MapReceiptFromReader(OracleDataReader reader, IOrder _order)
    {
        return new Receipt
        {
            Id = Convert.ToInt32(reader["Id"]),
            Date = Convert.ToDateTime(reader["\"Date\""]),
            Sub_Total = Convert.ToDecimal(reader["Sub_Total"]),
            Taxes = Convert.ToDecimal(reader["Taxes"]),
            Discount = Convert.ToDecimal(reader["Discount"]),
            Total = Convert.ToDecimal(reader["Total"]),
            Table_Id = Convert.ToInt32(reader["Table_Id"]),
            Orders = _order.GetAllByReceipt(Convert.ToInt32(reader["Id"])).ToList(),
        };
    }
}
