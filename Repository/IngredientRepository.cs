using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace Restaurant_Management.Repository
{
    public class IngredientRepository : IIngredient
    {
        private readonly string _connectionString;

        public IngredientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(Ingredient ingredient)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new(
                "INSERT INTO \"Ingredient\" (Name, Quantity) VALUES (:name, :quantity) RETURNING Id INTO :Id", connection);
            command.Parameters.Add("name", OracleDbType.Varchar2).Value = ingredient.Name;
            command.Parameters.Add("quantity", OracleDbType.Decimal).Value = ingredient.Quantity;

            OracleParameter IdParam = new(":Id", OracleDbType.Int32)
            {
                Direction = ParameterDirection.ReturnValue
            };
            command.Parameters.Add(IdParam);
            var result = command.ExecuteNonQuery();
            ingredient.Id = Convert.ToInt32(IdParam.Value.ToString());
        
            return result > 0;
        }

        public Ingredient? Get(int ingredientId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new("SELECT * FROM \"Ingredient\" WHERE Id = :ingredientId", connection);
            command.Parameters.Add("ingredientId", OracleDbType.Int32).Value = ingredientId;

            using OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapIngredientFromReader(reader);
            }

            return null;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            List<Ingredient> ingredients = new();

            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new("SELECT * FROM \"Ingredient\"", connection);
            using OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Ingredient ingredient = MapIngredientFromReader(reader);
                ingredients.Add(ingredient);
            }

            return ingredients;
        }

        public bool Remove(int ingredientId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new("DELETE FROM \"Ingredient\" WHERE Id = :ingredientId", connection);
            command.Parameters.Add("ingredientId", OracleDbType.Int32).Value = ingredientId;

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool Update(Ingredient ingredient)
        {
            if (ingredient.Id == null)
                return false;

            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new(
                "UPDATE \"Ingredient\" SET Name = :name, Quantity = :quantity WHERE Id = :ingredientId", connection);
            command.Parameters.Add("name", OracleDbType.Varchar2).Value = ingredient.Name;
            command.Parameters.Add("quantity", OracleDbType.Decimal).Value = ingredient.Quantity;
            command.Parameters.Add("ingredientId", OracleDbType.Int32).Value = ingredient.Id;

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        private Ingredient MapIngredientFromReader(OracleDataReader reader)
        {
            return new Ingredient
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                Quantity = Convert.ToDecimal(reader["Quantity"]),
            };
        }
    }
}
