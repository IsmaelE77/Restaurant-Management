using Microsoft.VisualBasic;
using System.Data;


namespace Restaurant_Management.Repository
{
    public class EmployeeRepository(string _connectionString) : IEmployee
    {
        public Employee? Get(int employeeId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Employee\" WHERE Id = :Id";
            using OracleCommand command = new(query, connection);

            command.Parameters.Add(":Id", employeeId);

            using OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapEmployeeFromReader(reader);
            }

            return null;
        }

        public IEnumerable<Employee> GetAll()
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            string query = "SELECT * FROM \"Employee\"";
            using OracleCommand command = new(query, connection);
            using OracleDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return MapEmployeeFromReader(reader);
            }
        }

        public bool Add(Employee employee)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new("INSERT INTO \"Employee\" (Manager_Id, First_Name, Last_Name, Phone_Number, Address, Salary_per_Hour, Section_Id)"
                + "VALUES (:Manager_Id, :First_Name, :Last_Name, :Phone_Number, :Address, :Salary_per_Hour, :Section_Id) RETURNING Id into :pId", connection);
            // Set parameters based on your Employee model properties
            command.Parameters.Add(new OracleParameter(":Manager_Id", employee.ManagerId ?? (object)DBNull.Value));
            command.Parameters.Add(new OracleParameter(":First_Name", employee.FirstName));
            command.Parameters.Add(new OracleParameter(":Last_Name", employee.LastName));
            command.Parameters.Add(new OracleParameter(":Phone_Number", employee.PhoneNumber));
            command.Parameters.Add(new OracleParameter(":Address", employee.Address));
            command.Parameters.Add(new OracleParameter(":Salary_per_Hour", employee.SalaryPerHour));
            command.Parameters.Add(new OracleParameter(":Section_Id", employee.SectionId));
            OracleParameter IdParam = new(":pId", OracleDbType.Int32)
            {
                Value = ParameterDirection.ReturnValue
            };
            command.Parameters.Add(IdParam);
            var result = command.ExecuteNonQuery();
            employee.Id = Convert.ToInt32(IdParam.Value.ToString());
            return result > 0;
        }

        public bool Update(Employee employee)
        {
            if(employee.Id == null)
                return false;
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new("UPDATE \"Employee\" SET Manager_Id = :Manager_Id, First_Name = :First_Name, Last_Name = :Last_Name, " +
                                                            "Phone_Number = :Phone_Number, Address = :Address, Salary_per_Hour = :Salary_per_Hour, Section_Id = :Section_Id " +
                                                            "WHERE Id = :Employee_Id", connection);
            command.Parameters.Add(new OracleParameter(":Manager_Id", employee.ManagerId ?? (object)DBNull.Value));
            command.Parameters.Add(new OracleParameter(":First_Name", employee.FirstName));
            command.Parameters.Add(new OracleParameter(":Last_Name", employee.LastName));
            command.Parameters.Add(new OracleParameter(":Phone_Number", employee.PhoneNumber));
            command.Parameters.Add(new OracleParameter(":Address", employee.Address));
            command.Parameters.Add(new OracleParameter(":Salary_per_Hour", employee.SalaryPerHour));
            command.Parameters.Add(new OracleParameter(":Section_Id", employee.SectionId));
            command.Parameters.Add(new OracleParameter(":Id", employee.Id)); // Assuming EmployeeId is the primary key

            return command.ExecuteNonQuery() > 0;
        }

        public bool Remove(int employeeId)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();
            using OracleCommand command = new("DELETE FROM \"Employee\" WHERE Id = :Employee_Id", connection);
            command.Parameters.Add(new OracleParameter(":Employee_Id", employeeId));

            return command.ExecuteNonQuery() > 0;
        }



        private Employee MapEmployeeFromReader(OracleDataReader reader)
        {
            // Map data from the reader to an Employee object
            return new Employee
            {
                Id = Convert.ToInt32(reader["Id"]),
                ManagerId = reader["Manager_Id"] != DBNull.Value ? Convert.ToInt32(reader["Manager_Id"]) : null,
                FirstName = Convert.ToString(reader["First_Name"])?? "",
                LastName = Convert.ToString(reader["Last_Name"])?? "",
                PhoneNumber = Convert.ToString(reader["Phone_Number"])?? "",
                Address = Convert.ToString(reader["Address"])?? "",
                SalaryPerHour = Convert.ToDecimal(reader["Salary_per_Hour"]),
                SectionId = Convert.ToInt32(reader["Section_Id"]),
            };
        }
        public decimal GetTotalSalesForEmployeeInYear(int employeeId, int year)
        {
            using OracleConnection connection = new(_connectionString);
            connection.Open();

            using OracleCommand command = new(
                $"SELECT SUM(Price) AS TotalSales " +
                $"FROM \"Order\" " +
                $"WHERE EXTRACT(YEAR FROM \"Date\") = :year AND Employee_Id = :employeeId", connection);
            command.Parameters.Add("year", OracleDbType.Int32).Value = year;
            command.Parameters.Add("employeeId", OracleDbType.Int32).Value = employeeId;

            object result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return Convert.ToDecimal(result);
            }

            return 0m; // If no sales found, return zero.
        }


        public int GetTotalWorkingHours(int Id, DateTime? from = null, DateTime? to = null)
        {
            throw new NotImplementedException();
        }

        public decimal GetTheSalary(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
