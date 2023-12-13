﻿namespace Restaurant_Management.Repository;

public class SupplierRepository(string connectionString, ISupplier_Ingredient _supplier_Ingredient) : ISupplier
{
    public bool Add(Supplier supplier)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"insert into Supplier(Full_Name, Phone_Number, Total_Pay) Values('{supplier.Full_Name}', '{supplier.Id}', '{supplier.Total_Pay}') RETURNING Id into :Id", con);
        OracleParameter IdParam = new(":Id", OracleDbType.Int32)
        {
            Value = ParameterDirection.ReturnValue
        };
        foreach (var item in supplier.Suppliers_Ingredients) _supplier_Ingredient.Add(item);
        cmd.Parameters.Add(IdParam);
        int result = cmd.ExecuteNonQuery();
        supplier.Id = Convert.ToInt32(IdParam.Value.ToString());
        return result > 0;
    }

    public Supplier? Get(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from Supplier where Id='{Id}'", con);
        using var reader = cmd.ExecuteReader();
        reader.Read();
        var Full_Name = reader.GetString(reader.GetOrdinal("Full_Name"));
        var Phone_Number = reader.GetString(reader.GetOrdinal("Phone_Number"));
        var Total_Pay = _supplier_Ingredient.GetTotal_Pay(Id);
        return new(Total_Pay, Id, Full_Name, Phone_Number);
    }

    public IEnumerable<Supplier> GetAll()
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Select * from Supplier", con);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var Id = reader.GetInt32(reader.GetOrdinal("Id"));
            var Full_Name = reader.GetString(reader.GetOrdinal("Full_Name"));
            var Phone_Number = reader.GetString(reader.GetOrdinal("Phone_Number"));
            var Total_Pay = _supplier_Ingredient.GetTotal_Pay(Id);
            yield return new(Total_Pay, Id, Full_Name, Phone_Number);
        }
    }

    public bool Remove(int Id)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Delete from Supplier where Id='{Id}'", con);
        var top = _supplier_Ingredient.GetSupplier_IngredientsForSuppliers(Id);
        foreach (var supplier_Ingredient in top)
        {
            _supplier_Ingredient.Remove(supplier_Ingredient.Id ?? 0);
        }
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(Supplier supplier)
    {
        using var con = new OracleConnection(connectionString);
        con.Open();
        using var cmd = new OracleCommand($"Update \"Table\" Set Full_Name='{supplier.Full_Name}', Phone_Number='{supplier.Phone_Number}', Total_Pay='{supplier.Total_Pay}' where Id='{supplier.Id}'", con);
        foreach (var supplier_Ingredient in supplier.Suppliers_Ingredients.Where(x => x.Id is not null))
        {
            _supplier_Ingredient.Update(supplier_Ingredient);
        }
        return cmd.ExecuteNonQuery() > 0;
    }
}