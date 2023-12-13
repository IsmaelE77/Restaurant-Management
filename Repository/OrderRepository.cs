namespace Restaurant_Management.Repository
{
    public class OrderRepository : IOrder
    {
        private readonly string _connectionString;
        private readonly IOrderItem _orderItemRepository;

        public OrderRepository(string connectionString, IOrderItem orderItemRepository)
        {
            _connectionString = connectionString;
            _orderItemRepository = orderItemRepository;
        }

        public bool Add(Order order)
        {
            if(order.OrderItems == null || order.OrderItems.Count < 1)
                return false;
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new(
            "INSERT INTO \"Order\"(\"Date\", Price, Employee_Id, Table_Id, Receipt_Id) " +
            "VALUES (:pDate, :pPrice, :pEmployeeId, :pTableId, :pReceiptId) RETURNING Id into :Id", connection);

            OracleParameter dateParam = new(":pDate", OracleDbType.TimeStamp);
            dateParam.Value = order.Date;
            command.Parameters.Add(dateParam);
            command.Parameters.Add(new OracleParameter(":Price", order.Price));
            command.Parameters.Add(new OracleParameter(":EmployeeId", order.EmployeeId));
            command.Parameters.Add(new OracleParameter(":TableId", order.TableId));
            command.Parameters.Add(new OracleParameter(":ReceiptId", order.ReceiptId ?? (object)DBNull.Value));
            command.Parameters.Add(new OracleParameter(":ReceiptId", order.ReceiptId ?? (object)DBNull.Value));
            OracleParameter IdParam = new(":Id", OracleDbType.Int32)
            {
                Value = ParameterDirection.ReturnValue
            };
            command.Parameters.Add(IdParam);
            var result = command.ExecuteNonQuery();
            order.Id = Convert.ToInt32(IdParam.Value.ToString());
            foreach(var orderItem in order.OrderItems){
                orderItem.OrderId = order.Id.Value;
                _orderItemRepository.Add(orderItem);
            }
            return result > 0;
        }

        public bool Remove(int orderId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new("DELETE FROM \"Order\" WHERE Id = :OrderId", connection);
            command.Parameters.Add(new OracleParameter(":OrderId", orderId));
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public IEnumerable<Order> GetAll()
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Order\"";
            using OracleCommand command = new(query, connection);
            using OracleDataReader reader = command.ExecuteReader();
            List<Order> orders = [];

            while (reader.Read())
            {
                orders.Add(MapOrderFromReader(reader));
            }

            return orders;
        }
        public IEnumerable<Order> GetAllByEmploye(int employeeId)
        {
            List<Order> orders = [];

            using OracleConnection connection = new(_connectionString);

            connection.Open();
            string query = "SELECT * FROM \"Order\" WHERE Employee_Id = :EmployeeId";

            using OracleCommand command = new(query, connection);
            command.Parameters.Add(new OracleParameter(":EmployeeId", employeeId));

            using OracleDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                orders.Add(MapOrderFromReader(reader));
            }

            return orders;
        }

        public IEnumerable<Order> GetAllByEmployeeAndYear(int employeeId, int year)
        {
            List<Order> orders = [];

            using OracleConnection connection = new OracleConnection(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Order\" WHERE Employee_Id = :EmployeeId AND EXTRACT(YEAR FROM \"Date\") = :Year";

            using OracleCommand command = new(query, connection);
            command.Parameters.Add(new OracleParameter(":EmployeeId", employeeId));
            command.Parameters.Add(new OracleParameter(":Year", year));

            using OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(MapOrderFromReader(reader));

            }

            return orders;
        }

        public Order? Get(int orderId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Order\" WHERE Id = :OrderId";
            using OracleCommand command = new(query, connection);
            command.Parameters.Add(new OracleParameter(":OrderId", orderId));

            using OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapOrderFromReader(reader);
            }

            return null;
        }

        public bool Update(Order order)
        {
            if(order.Id == null)
                return false;
            if(order.OrderItems == null|| order.OrderItems.Count < 1)
                return false;
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
            command.Parameters.Add(new OracleParameter(":pEmployeeId", order.EmployeeId));
            command.Parameters.Add(new OracleParameter(":pTableId", order.TableId));
            command.Parameters.Add(new OracleParameter(":pReceiptId", order.ReceiptId ?? (object)DBNull.Value));
            command.Parameters.Add(new OracleParameter(":pOrderId", order.Id));

            int rowsAffected = command.ExecuteNonQuery();
            foreach(var orderItem in order.OrderItems){
                orderItem.OrderId = order.Id.Value;
                if(_orderItemRepository.Get(orderItem.Id) == null)
                    _orderItemRepository.Add(orderItem);
                else
                    _orderItemRepository.Update(orderItem);
            }
            // Check if any rows were affected
            return rowsAffected > 0;
        }

        private Order MapOrderFromReader(OracleDataReader reader)
        {
            Order order = new()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Date = Convert.ToDateTime(reader["\"Date\""]),
                Price = Convert.ToDecimal(reader["Price"]),
                EmployeeId = Convert.ToInt32(reader["Employee_Id"]),
                TableId = Convert.ToInt32(reader["Table_Id"]),
                ReceiptId = reader["Receipt_Id"] != DBNull.Value ? Convert.ToInt32(reader["Receipt_Id"]) : (int?)null,
            };
            order.OrderItems = _orderItemRepository.GetAllByOrder(order.Id.Value).ToList();
            return order;
        }
    }
}