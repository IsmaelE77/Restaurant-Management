namespace Restaurant_Management.Repository;

public class Order_ItemRepository(string _connectionString) : IOrder_Item
{
    public bool Add(Order_Item orderItem)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "INSERT INTO \"Order_Item\" (Quantity, Unit_Price, Order_Id, Item_Id) " +
            "VALUES (:Quantity, :UnitPrice, :Order_Id, :Item_Id) RETURNING Id INTO :Id", connection);
        command.Parameters.Add(":Quantity", OracleDbType.Int32).Value = orderItem.Quantity;
        command.Parameters.Add(":UnitPrice", OracleDbType.Decimal).Value = orderItem.UnitPrice;
        command.Parameters.Add(":Order_Id", OracleDbType.Int32).Value = orderItem.Order_Id;
        command.Parameters.Add(":Item_Id", OracleDbType.Int32).Value = orderItem.Item_Id;
        command.Parameters.Add(":Id", OracleDbType.Int32, ParameterDirection.ReturnValue);
        var result = command.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)command.Parameters[":Id"].Value;
        orderItem.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public bool Remove(int orderItemId)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("DELETE FROM \"Order_Item\" WHERE Id = :OrderItemId", connection);
        command.Parameters.Add(":OrderItemId", OracleDbType.Int32).Value = orderItemId;
        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public Order_Item? Get(int orderItemId)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order_Item\" WHERE Id = :OrderItemId";
        using var command = new OracleCommand(query, connection);
        command.Parameters.Add(":OrderItemId", OracleDbType.Int32).Value = orderItemId;
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapOrderItemFromReader(reader) : null;
    }

    public IEnumerable<Order_Item> GetAll()
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order_Item\"";
        using var command = new OracleCommand(query, connection);
        using var reader = command.ExecuteReader();
        List<Order_Item> orderItems = new List<Order_Item>();
        while (reader.Read()) orderItems.Add(MapOrderItemFromReader(reader));
        return orderItems;
    }

    public bool Update(Order_Item orderItem)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "UPDATE \"Order_Item\" SET Quantity = :Quantity, Unit_Price = :UnitPrice, Order_Id = :Order_Id, Item_Id = :Item_Id " +
            "WHERE Id = :OrderItemId", connection);
        command.Parameters.Add(":Quantity", OracleDbType.Int32).Value = orderItem.Quantity;
        command.Parameters.Add(":UnitPrice", OracleDbType.Decimal).Value = orderItem.UnitPrice;
        command.Parameters.Add(":Order_Id", OracleDbType.Int32).Value = orderItem.Order_Id;
        command.Parameters.Add(":Item_Id", OracleDbType.Int32).Value = orderItem.Item_Id;
        command.Parameters.Add(":OrderItemId", OracleDbType.Int32).Value = orderItem.Id;
        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public bool RemoveByOrderId(int orderId)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("DELETE FROM \"Order_Item\" WHERE Order_Id = :Order_Id", connection);
        command.Parameters.Add(":Order_Id", OracleDbType.Int32).Value = orderId;
        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    private static Order_Item MapOrderItemFromReader(OracleDataReader reader)
    {
        return new Order_Item
        {
            Id = Convert.ToInt32(reader["Id"]),
            Quantity = Convert.ToInt32(reader["Quantity"]),
            UnitPrice = Convert.ToDecimal(reader["Unit_Price"]),
            Order_Id = Convert.ToInt32(reader["Order_Id"]),
            Item_Id = Convert.ToInt32(reader["Item_Id"]),
        };
    }

    public IEnumerable<Order_Item> GetOrder_ItemsForOrder(int Order_Id)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order_Item\" WHERE Order_Id = :Order_Id";
        using var command = new OracleCommand(query, connection);
        command.Parameters.Add(":Order_Id", OracleDbType.Int32).Value = Order_Id;
        using var reader = command.ExecuteReader();
        while (reader.Read()) yield return MapOrderItemFromReader(reader);
    }

    public IEnumerable<Order_Item> GetOrder_ItemForItem(int Item_Id)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        string query = "SELECT * FROM \"Order_Item\" WHERE Item_Id = :Item_Id";
        using var command = new OracleCommand(query, connection);
        command.Parameters.Add(":Item_Id", OracleDbType.Int32).Value = Item_Id;
        using var reader = command.ExecuteReader();
        while (reader.Read()) yield return MapOrderItemFromReader(reader);
    }
}
