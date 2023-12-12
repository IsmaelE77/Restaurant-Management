

using System.Data;

namespace Restaurant_Management.Repository
{
    public class OrderItemRepository : IOrderItem
    {
        private readonly string _connectionString;

        public OrderItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(OrderItem orderItem)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new(
                "INSERT INTO \"Order_Item\" (Quantity, Unit_Price, Order_Id, Item_Id) " +
                "VALUES (:Quantity, :UnitPrice, :OrderId, :ItemId) RETURNING Id into :Id", connection);

            command.Parameters.Add(new OracleParameter(":Quantity", orderItem.Quantity));
            command.Parameters.Add(new OracleParameter(":UnitPrice", orderItem.UnitPrice));
            command.Parameters.Add(new OracleParameter(":OrderId", orderItem.OrderId));
            command.Parameters.Add(new OracleParameter(":ItemId", orderItem.ItemId));
            OracleParameter IdParam = new(":Id",OracleDbType.Int32);
            IdParam.Value =  ParameterDirection.ReturnValue;
            command.Parameters.Add(IdParam);
            command.ExecuteNonQuery();
            return Convert.ToInt32(IdParam.Value.ToString());
        }

        public bool Remove(int orderItemId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new("DELETE FROM \"Order_Item\" WHERE Id = :OrderItemId", connection);
            command.Parameters.Add(new OracleParameter(":OrderItemId", orderItemId));


            int rowsAffected = command.ExecuteNonQuery();

            // Check if any rows were affected
            return rowsAffected > 0;
        }

        public OrderItem? Get(int orderItemId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Order_Item\" WHERE Id = :OrderItemId";
            using OracleCommand command = new(query, connection);
            command.Parameters.Add(new OracleParameter(":OrderItemId", orderItemId));

            using OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapOrderItemFromReader(reader);
            }

            return null;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Order_Item\"";
            using OracleCommand command = new(query, connection);
            using OracleDataReader reader = command.ExecuteReader();
            List<OrderItem> orderItems = [];

            while (reader.Read())
            {
                orderItems.Add(MapOrderItemFromReader(reader));
            }

            return orderItems;
        }

        public IEnumerable<OrderItem> GetAllByOrder(int orderId)
        {
            List<OrderItem> orderItems = [];

            using OracleConnection connection = new(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Order_Item\" WHERE Order_Id = :OrderId";

            using OracleCommand command = new(query, connection);
            command.Parameters.Add(new OracleParameter(":OrderId", orderId));


            using OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderItems.Add(MapOrderItemFromReader(reader));

            }
            

            return orderItems;
        }

        public bool Update(OrderItem orderItem)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new(
                "UPDATE \"Order_Item\" SET Quantity = :Quantity, Unit_Price = :UnitPrice, Order_Id = :OrderId, Item_Id = :ItemId " +
                "WHERE Id = :OrderItemId", connection);

            command.Parameters.Add(new OracleParameter(":Quantity", orderItem.Quantity));
            command.Parameters.Add(new OracleParameter(":UnitPrice", orderItem.UnitPrice));
            command.Parameters.Add(new OracleParameter(":OrderId", orderItem.OrderId));
            command.Parameters.Add(new OracleParameter(":ItemId", orderItem.ItemId));
            command.Parameters.Add(new OracleParameter(":OrderItemId", orderItem.Id));

            int rowsAffected = command.ExecuteNonQuery();

            // Check if any rows were affected
            return rowsAffected > 0;
        }

        private OrderItem MapOrderItemFromReader(OracleDataReader reader)
        {
            return new OrderItem
            {
                Id = Convert.ToInt32(reader["Id"]),
                Quantity = Convert.ToInt32(reader["Quantity"]),
                UnitPrice = Convert.ToDecimal(reader["Unit_Price"]),
                OrderId = Convert.ToInt32(reader["Order_Id"]),
                ItemId = Convert.ToInt32(reader["Item_Id"]),
            };
        }

    }
}