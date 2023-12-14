
using System.Data;

namespace Restaurant_Management.Repository
{
    public class ItemRepository : IItem
    {
        private readonly string _connectionString;

        public ItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(Item item)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new(
                "INSERT INTO \"Item\" (Title, Description, Price, Added, Rating, Category_Id) " +
                "VALUES (:title, :description, :price, :added, :rating, :categoryId) RETURNING Id into :pId", connection);
            command.Parameters.Add(new OracleParameter(":title", item.Title));
            command.Parameters.Add(new OracleParameter(":description", item.Description));
            command.Parameters.Add(new OracleParameter(":price", item.Price));
            OracleParameter dateParam = new(":added", OracleDbType.TimeStamp);
            dateParam.Value = item.Added;
            command.Parameters.Add(dateParam);
            command.Parameters.Add(new OracleParameter(":rating", item.Rating));
            command.Parameters.Add(new OracleParameter(":categoryId",item.CategoryId));
            OracleParameter IdParam = new(":pId", OracleDbType.Int32)
            {
                Value = ParameterDirection.ReturnValue
            };
            command.Parameters.Add(IdParam);
            var result = command.ExecuteNonQuery();
            item.Id = Convert.ToInt32(IdParam.Value.ToString());
            return result > 0;
        }

        public Item Get(int itemId)
        {
            using OracleConnection connection = new OracleConnection(_connectionString);
            connection.Open();

            using OracleCommand command = new OracleCommand("SELECT * FROM \"Item\" WHERE Id = :itemId", connection);
            command.Parameters.Add("itemId", OracleDbType.Int32).Value = itemId;

            using OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapItemFromReader(reader);
            }

            return null;
        }

        public IEnumerable<Item> GetAllByCategoryId(int categoryId)
        {
            List<Item> items = [];

            using OracleConnection connection = new OracleConnection(_connectionString);
            connection.Open();

            using OracleCommand command = new OracleCommand("SELECT * FROM \"Item\" WHERE Category_Id = :categoryId", connection);
            command.Parameters.Add("categoryId", OracleDbType.Int32).Value = categoryId;

            using OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Item item = MapItemFromReader(reader);
                items.Add(item);

            }
            return items;
        }

        public IEnumerable<Item>  GetAll()
        {
            List<Item> items = [];

            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new("SELECT * FROM \"Item\"", connection);
            using OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Item item = MapItemFromReader(reader);
                items.Add(item);
            }

            return items;
        }

        public bool Remove(int Id)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new("DELETE FROM \"Item\" WHERE Id = :itemId", connection);
            command.Parameters.Add("itemId", OracleDbType.Int32).Value = Id;

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool Update(Item item)
        {
            if(item.Id == null)
                return false;
            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new(
                "UPDATE \"Item\" SET Title = :title, Description = :description, Price = :price, " +
                "Added = :added, Rating = :rating, Category_Id = :categoryId WHERE Id = :itemId", connection);
            command.Parameters.Add(new OracleParameter(":title", item.Title));
            command.Parameters.Add(new OracleParameter(":description",  item.Description));
            command.Parameters.Add(new OracleParameter(":price", item.Price));
            OracleParameter dateParam = new(":added", OracleDbType.TimeStamp);
            dateParam.Value = item.Added;
            command.Parameters.Add(dateParam);
            command.Parameters.Add(new OracleParameter(":rating", item.Rating));
            command.Parameters.Add(new OracleParameter(":categoryId",item.CategoryId));
            command.Parameters.Add(new OracleParameter(":itemId",item.Id));
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        private Item MapItemFromReader(OracleDataReader reader)
        {
            return new Item
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title = Convert.ToString(reader["Title"]),
                Description = Convert.ToString(reader["Description"]),
                Price = Convert.ToDecimal(reader["Price"]),
                Added = Convert.ToDateTime(reader["Added"]),
                Rating = Convert.ToInt16(reader["Rating"]),
                CategoryId = Convert.ToInt32(reader["Category_Id"])
            };
        }
    }
}
