namespace Restaurant_Management.Interface;

public interface IEmployee : ICrud<Employee>
{
    public int GetTotalWorkingHours(int Id, DateTime? from = null, DateTime? to = null);
    public decimal GetTheSalary(int Id);
}
