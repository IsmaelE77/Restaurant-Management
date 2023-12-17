namespace Restaurant_Management.Repository;

public class ItemRepository(string _connectionString, IItem_Ingredient _item_Ingredient, 
    IOrder_Item _order_Item) : IItem
{
    public bool Add(Item item)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "INSERT INTO \"Item\" (Title, Description, Price, Added, Rating, Category_Id) " +
            "VALUES (:title, :description, :price, :added, :rating, :categoryId) RETURNING Id into :pId", connection);
        command.Parameters.Add(new OracleParameter(":title", item.Title));
        command.Parameters.Add(new OracleParameter(":description", item.Description));
        command.Parameters.Add(new OracleParameter(":price", item.Price));
        OracleParameter dateParam = new(":added", OracleDbType.TimeStamp) { Value = item.Added };
        command.Parameters.Add(dateParam);
        command.Parameters.Add(new OracleParameter(":rating", item.Rating));
        command.Parameters.Add(new OracleParameter(":categoryId", item.Category_Id));
        OracleParameter IdParam = new(":pId", OracleDbType.Int32) { Value = ParameterDirection.ReturnValue };
        command.Parameters.Add(IdParam);
        var result = command.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)IdParam.Value;
        item.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Item? Get(int Item_Id)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Item\" WHERE Id = :Item_Id", connection);
        command.Parameters.Add("Item_Id", OracleDbType.Int32).Value = Item_Id;
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapItemFromReader(reader, _item_Ingredient, _order_Item) : null;
    }

    public IEnumerable<Item> GetAllByCategoryId(int categoryId)
    {
        var items = new List<Item>();
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Item\" WHERE Category_Id = :categoryId", connection);
        command.Parameters.Add("categoryId", OracleDbType.Int32).Value = categoryId;
        using var reader = command.ExecuteReader();
        while (reader.Read()) items.Add(MapItemFromReader(reader, _item_Ingredient, _order_Item));
        return items;
    }

    public IEnumerable<Item> GetAll()
    {
        var items = new List<Item>();
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Item\"", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()) items.Add(MapItemFromReader(reader, _item_Ingredient, _order_Item));
        return items;
    }

    public bool Remove(int Id)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("DELETE FROM \"Item\" WHERE Id = :Item_Id", connection);
        command.Parameters.Add("Item_Id", OracleDbType.Int32).Value = Id;
        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    public bool Update(Item item)
    {
        if (item.Id == null)
            return false;
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "UPDATE \"Item\" SET Title = :title, Description = :description, Price = :price, " +
            "Added = :added, Rating = :rating, Category_Id = :categoryId WHERE Id = :Item_Id", connection);
        command.Parameters.Add(new OracleParameter(":title", item.Title));
        command.Parameters.Add(new OracleParameter(":description", item.Description));
        command.Parameters.Add(new OracleParameter(":price", item.Price));
        OracleParameter dateParam = new(":added", OracleDbType.TimeStamp) { Value = item.Added };
        command.Parameters.Add(dateParam);
        command.Parameters.Add(new OracleParameter(":rating", item.Rating));
        command.Parameters.Add(new OracleParameter(":categoryId", item.Category_Id));
        command.Parameters.Add(new OracleParameter(":Item_Id", item.Id));
        foreach (var Item_Ing in item.Item_Ingredients)
        {
            _item_Ingredient.Update(Item_Ing);
        }
        foreach (var Order_Item in item.Order_Items)
        {
            _order_Item.Update(Order_Item);
        }
        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0;
    }

    private static Item MapItemFromReader(OracleDataReader reader, IItem_Ingredient _item_Ingredient, 
        IOrder_Item _order_Item)
    {
        int Id = Convert.ToInt32(reader["Id"]);
        return new Item
        {
            Id = Id,
            Title = Convert.ToString(reader["Title"]) ?? "",
            Description = Convert.ToString(reader["Description"]) ?? "",
            Price = Convert.ToDecimal(reader["Price"]),
            Added = Convert.ToDateTime(reader["Added"]),
            Rating = Convert.ToInt16(reader["Rating"]),
            Category_Id = Convert.ToInt32(reader["Category_Id"]),
            Item_Ingredients = _item_Ingredient.GetItem_IngredientForItems(Id).ToList(),
            Order_Items = _order_Item.GetOrder_ItemForItem(Id).ToList(),
        };
    }
}