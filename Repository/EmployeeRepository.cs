namespace Restaurant_Management.Repository;

public class EmployeeRepository(string _connectionString, IEmployee_WorkDay _WorkDay) : IEmployee
{
    public bool Add(Employee employee)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "INSERT INTO \"Employee\" (Manager_Id, First_Name, Last_Name, Phone_Number, Address, Salary_per_Hour, Section_Id)"
            + "VALUES (:Manager_Id, :First_Name, :Last_Name, :Phone_Number, :Address, :Salary_per_Hour, :Section_Id) RETURNING Id INTO :pId", connection);
        command.Parameters.Add(new OracleParameter(":Manager_Id", employee.ManagerId ?? (object)DBNull.Value));
        command.Parameters.Add(new OracleParameter(":First_Name", employee.FirstName));
        command.Parameters.Add(new OracleParameter(":Last_Name", employee.LastName));
        command.Parameters.Add(new OracleParameter(":Phone_Number", employee.PhoneNumber));
        command.Parameters.Add(new OracleParameter(":Address", employee.Address));
        command.Parameters.Add(new OracleParameter(":Salary_per_Hour", employee.SalaryPerHour));
        command.Parameters.Add(new OracleParameter(":Section_Id", employee.SectionId));
        OracleParameter IdParam = new(":pId", OracleDbType.Int32, ParameterDirection.ReturnValue);
        command.Parameters.Add(IdParam);
        var result = command.ExecuteNonQuery();
        OracleDecimal oracleDecimalId = (OracleDecimal)IdParam.Value;
        employee.Id = oracleDecimalId.ToInt32();
        return result > 0;
    }

    public Employee? Get(int employeeId)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Employee\" WHERE Id = :Id", connection);
        command.Parameters.Add(":Id", OracleDbType.Int32).Value = employeeId;
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapEmployeeFromReader(reader, _WorkDay) : null;
    }

    public IEnumerable<Employee> GetAll()
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("SELECT * FROM \"Employee\"", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()) yield return MapEmployeeFromReader(reader, _WorkDay);
    }

    public bool Update(Employee employee)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand(
            "UPDATE \"Employee\" SET Manager_Id = :Manager_Id, First_Name = :First_Name, Last_Name = :Last_Name, " +
            "Phone_Number = :Phone_Number, Address = :Address, Salary_per_Hour = :Salary_per_Hour, Section_Id = :Section_Id " +
            "WHERE Id = :Employee_Id", connection);
        command.Parameters.Add(new OracleParameter(":Manager_Id", employee.ManagerId ?? (object)DBNull.Value));
        command.Parameters.Add(new OracleParameter(":First_Name", employee.FirstName));
        command.Parameters.Add(new OracleParameter(":Last_Name", employee.LastName));
        command.Parameters.Add(new OracleParameter(":Phone_Number", employee.PhoneNumber));
        command.Parameters.Add(new OracleParameter(":Address", employee.Address));
        command.Parameters.Add(new OracleParameter(":Salary_per_Hour", employee.SalaryPerHour));
        command.Parameters.Add(new OracleParameter(":Section_Id", employee.SectionId));
        command.Parameters.Add(new OracleParameter(":Employee_Id", employee.Id));
        return command.ExecuteNonQuery() > 0;
    }

    public bool Remove(int employeeId)
    {
        using var connection = new OracleConnection(_connectionString);
        connection.Open();
        using var command = new OracleCommand("DELETE FROM \"Employee\" WHERE Id = :Employee_Id", connection);
        command.Parameters.Add(new OracleParameter(":Employee_Id", employeeId));
        return command.ExecuteNonQuery() > 0;
    }

    public int GetTotalWorkingHours(int Id, DateTime? from = null, DateTime? to = null)
    {
        var employee = Get(Id);
        var total = employee?.WorkDay.Aggregate(0, (x, y) =>
        {
            if (from is null && to is null) return y.WorkingHours;
            else if (from is null) return (y.Date <= to) ? y.WorkingHours : 0;
            else if (to is null) return (y.Date >= from) ? y.WorkingHours : 0;
            else return (y.Date >= from && y.Date <= to) ? y.WorkingHours : 0;
        });
        return total ?? -1;
    }

    public decimal GetTheSalary(int Id)
    {
        var Employee = Get(Id);
        if (Employee is null) return 0;
        DateTime firstDayOfCurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        int workingHours = GetTotalWorkingHours(Id, firstDayOfCurrentMonth);
        return workingHours * Employee.SalaryPerHour;
    }

    private static Employee MapEmployeeFromReader(OracleDataReader reader, IEmployee_WorkDay _WorkDay)
    {
        return new Employee
        {
            Id = Convert.ToInt32(reader["Id"]),
            ManagerId = reader["Manager_Id"] != DBNull.Value ? Convert.ToInt32(reader["Manager_Id"]) : null,
            FirstName = Convert.ToString(reader["First_Name"]) ?? "",
            LastName = Convert.ToString(reader["Last_Name"]) ?? "",
            PhoneNumber = Convert.ToString(reader["Phone_Number"]) ?? "",
            Address = Convert.ToString(reader["Address"]) ?? "",
            SalaryPerHour = Convert.ToDecimal(reader["Salary_per_Hour"]),
            SectionId = Convert.ToInt32(reader["Section_Id"]),
            WorkDay = _WorkDay.GetEmployee_WorkDays(Convert.ToInt32(reader["Id"])).ToList(),
        };
    }
}
