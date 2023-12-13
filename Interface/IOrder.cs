namespace Restaurant_Management.Interface
{
    public interface IOrder : ICrud<Order>
    {
        IEnumerable<Order> GetAllByEmploye(int EmployeeId);
        IEnumerable<Order> GetAllByEmployeeAndYear(int employeeId, int year);

    }
}
