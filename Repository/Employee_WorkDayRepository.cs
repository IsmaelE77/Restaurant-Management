namespace Restaurant_Management.Repository;

public class Employee_WorkDayRepository(string _connectionString) : IEmployee_WorkDay
{
    public bool Add(Employee_WorkDay item)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "INSERT INTO \"Employee_WorkDay\" (\"Date\", Starts, Ends, Working_Hours, Note, Employee_Id) " +
            "VALUES (:pDate, :Starts, :Ends, :Working_Hours, :Note, :Employee_Id) RETURNING Id INTO :Id", connection);
        command.Parameters.Add(new OracleParameter(":pDate", item.Date));
        command.Parameters.Add(new OracleParameter(":Starts", item.Starts));
        command.Parameters.Add(new OracleParameter(":Ends", item.Ends));
        command.Parameters.Add(new OracleParameter(":Working_Hours", item.WorkingHours));
        command.Parameters.Add(new OracleParameter(":Note", item.Note ?? (object)DBNull.Value));
        command.Parameters.Add(new OracleParameter(":Employee_Id", item.Employee_Id));
        OracleParameter IdParam = new(":Id", OracleDbType.Int32, ParameterDirection.ReturnValue);
        command.Parameters.Add(IdParam);
        var result = command.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)IdParam.Value;
        item.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Employee_WorkDay? Get(int Id)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Employee_WorkDay\" WHERE Id = :Id", connection);
        command.Parameters.Add(":Id", OracleDbType.Int32).Value = Id;
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapEmployeeWorkDayFromReader(reader) : null;
    }

    public IEnumerable<Employee_WorkDay> GetAll()
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Employee_WorkDay\"", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()) yield return MapEmployeeWorkDayFromReader(reader);
    }

    public IEnumerable<Employee_WorkDay> GetEmployee_WorkDays(int Employee_Id)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Employee_WorkDay\" WHERE Employee_Id = :Employee_Id", connection);
        command.Parameters.Add(new OracleParameter(":Employee_Id", Employee_Id));
        using var reader = command.ExecuteReader();
        while (reader.Read()) yield return MapEmployeeWorkDayFromReader(reader);
    }

    public bool Remove(int Id)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("DELETE FROM \"Employee_WorkDay\" WHERE Id = :Id", connection);
        command.Parameters.Add(new OracleParameter(":Id", Id));
        return command.ExecuteNonQuery() > 0;
    }

    public bool Update(Employee_WorkDay item)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "UPDATE \"Employee_WorkDay\" SET \"Date\" = :Date, Starts = :Starts, Ends = :Ends, " +
            "Working_Hours = :Working_Hours, Note = :Note, Employee_Id = :Employee_Id WHERE Id = :Id", connection);
        command.Parameters.Add(new OracleParameter(":Date", item.Date));
        command.Parameters.Add(new OracleParameter(":Starts", item.Starts));
        command.Parameters.Add(new OracleParameter(":Ends", item.Ends));
        command.Parameters.Add(new OracleParameter(":Working_Hours", item.WorkingHours));
        command.Parameters.Add(new OracleParameter(":Note", item.Note ?? (object)DBNull.Value));
        command.Parameters.Add(new OracleParameter(":Employee_Id", item.Employee_Id));
        command.Parameters.Add(new OracleParameter(":Id", item.Id));
        return command.ExecuteNonQuery() > 0;
    }

    private static Employee_WorkDay MapEmployeeWorkDayFromReader(OracleDataReader reader)
    {
        return new Employee_WorkDay
        {
            Id = Convert.ToInt32(reader["Id"]),
            Date = Convert.ToDateTime(reader["Date"]),
            Starts = Convert.ToDateTime(reader["Starts"]),
            Ends = Convert.ToDateTime(reader["Ends"]),
            WorkingHours = Convert.ToInt32(reader["Working_Hours"]),
            Note = Convert.ToString(reader["Note"]) ?? "",
            Employee_Id = Convert.ToInt32(reader["Employee_Id"]),
        };
    }
}
