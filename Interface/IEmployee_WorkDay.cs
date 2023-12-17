namespace Restaurant_Management.Interface;

public interface IEmployee_WorkDay : ICrud<Employee_WorkDay>
{
    public IEnumerable<Employee_WorkDay> GetEmployee_WorkDays(int Employee_Id);
}
