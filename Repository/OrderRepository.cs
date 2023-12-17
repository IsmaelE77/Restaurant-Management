namespace Restaurant_Management.Repository;

public class OrderRepository(string _connectionString, IOrder_Item _order_Item) : IOrder
{

    public bool Add(Order order)
    {
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        using OracleCommand command = new(
            "INSERT INTO \"Order\"(\"Date\", Price, Employee_Id, Table_Id, Receipt_Id) " +
            "VALUES (:pDate, :pPrice, :pEmployeeId, :pTableId, :pReceiptId) RETURNING Id into :pId", connection);
        OracleParameter dateParam = new(":pDate", OracleDbType.TimeStamp)
        {
            Value = order.Date
        };
        command.Parameters.Add(dateParam);
        command.Parameters.Add(new OracleParameter(":pPrice", order.Price));
        command.Parameters.Add(new OracleParameter(":pEmployeeId", order.Employee_Id));
        command.Parameters.Add(new OracleParameter(":pTableId", order.Table_Id));
        command.Parameters.Add(new OracleParameter(":pReceiptId", order.Receipt_Id ?? (object)DBNull.Value));
        OracleParameter IdParam = new(":pId", OracleDbType.Int32, ParameterDirection.ReturnValue);
        command.Parameters.Add(IdParam);
        var result = command.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)command.Parameters[":pId"].Value;
        order.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Order? Get(int orderId)
    {
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order\" WHERE Id = :Order_Id";
        using OracleCommand command = new(query, connection);
        command.Parameters.Add(new OracleParameter(":Order_Id", orderId));
        using OracleDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapOrderFromReader(reader, _order_Item) : null;
    }

    public IEnumerable<Order> GetAll()
    {
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order\"";
        using OracleCommand command = new(query, connection);
        using OracleDataReader reader = command.ExecuteReader();
        while (reader.Read()) yield return MapOrderFromReader(reader, _order_Item);
    }
    public IEnumerable<Order> GetAllByEmployee(int employeeId)
    {
        List<Order> orders = [];
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order\" WHERE Employee_Id = :Employee_Id";
        using OracleCommand command = new(query, connection);
        command.Parameters.Add(new OracleParameter(":Employee_Id", employeeId));
        using OracleDataReader reader = command.ExecuteReader();
        while (reader.Read()) orders.Add(MapOrderFromReader(reader, _order_Item));
        return orders;
    }

    public IEnumerable<Order> GetAllByEmployeeAndYear(int employeeId, int year)
    {
        List<Order> orders = [];
        using OracleConnection connection = new OracleConnection(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order\" WHERE Employee_Id = :Employee_Id AND EXTRACT(YEAR FROM \"Date\") = :Year";
        using OracleCommand command = new(query, connection);
        command.Parameters.Add(new OracleParameter(":Employee_Id", employeeId));
        command.Parameters.Add(new OracleParameter(":Year", year));
        using OracleDataReader reader = command.ExecuteReader();
        while (reader.Read()) orders.Add(MapOrderFromReader(reader, _order_Item));
        return orders;
    }
    
    public IEnumerable<Order> GetAllByReceipt(int Receipt_Id)
    {
        List<Order> orders = [];
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order\" WHERE Receipt_Id = :Receipt_Id";
        using OracleCommand command = new(query, connection);
        command.Parameters.Add(new OracleParameter(":Receipt_Id", Receipt_Id));
        using OracleDataReader reader = command.ExecuteReader();
        while (reader.Read()) orders.Add(MapOrderFromReader(reader, _order_Item));
        return orders;
    }

    public IEnumerable<Order> GetAllByTable(int Table_Id)
    {
        List<Order> orders = [];
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order\" WHERE Table_Id = :Table_Id";
        using OracleCommand command = new(query, connection);
        command.Parameters.Add(new OracleParameter(":Table_Id", Table_Id));
        using OracleDataReader reader = command.ExecuteReader();
        while (reader.Read()) orders.Add(MapOrderFromReader(reader, _order_Item));
        return orders;
    }

    public bool Remove(int orderId)
    {
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        using OracleCommand command = new("DELETE FROM \"Order\" WHERE Id = :Order_Id", connection);
        command.Parameters.Add(new OracleParameter(":Order_Id", orderId));
        return command.ExecuteNonQuery() > 0;
    }

    public bool Update(Order order)
    {
        using OracleConnection connection = new(_connectionString);
        connection.Open();
        using OracleCommand command = new(
            "UPDATE \"Order\" SET \"Date\" = :pDate, Price = :pPrice, Employee_Id = :pEmployeeId, Table_Id = :pTableId, Receipt_Id = :pReceiptId " +
            "WHERE Id = :pOrderId", connection);
        OracleParameter dateParam = new(":pDate", OracleDbType.TimeStamp)
        {
            Value = order.Date
        };
        OracleParameter priceParam = new(":pPrice", OracleDbType.Decimal)
        {
            Value = order.Price
        };
        command.Parameters.Add(dateParam);
        command.Parameters.Add(priceParam);
        command.Parameters.Add(new OracleParameter(":pEmployeeId", order.Employee_Id));
        command.Parameters.Add(new OracleParameter(":pTableId", order.Table_Id));
        command.Parameters.Add(new OracleParameter(":pReceiptId", order.Receipt_Id ?? (object)DBNull.Value));
        command.Parameters.Add(new OracleParameter(":pOrderId", order.Id));
        foreach (var Order_Item in order.Order_Items)
        {
            _order_Item.Update(Order_Item);
        }
        return command.ExecuteNonQuery() > 0;
    }

    private static Order MapOrderFromReader(OracleDataReader reader, IOrder_Item _order_Item)
    {
        return new Order {
            Id = Convert.ToInt32(reader["Id"]),
            Date = Convert.ToDateTime(reader["\"Date\""]),
            Price = Convert.ToDecimal(reader["Price"]),
            Employee_Id = Convert.ToInt32(reader["Employee_Id"]),
            Table_Id = Convert.ToInt32(reader["Table_Id"]),
            Receipt_Id = reader["Receipt_Id"] != DBNull.Value ? Convert.ToInt32(reader["Receipt_Id"]) : null,
            Order_Items = _order_Item.GetOrder_ItemsForOrder(Convert.ToInt32(reader["Id"])).ToList(),
        };
    }
}