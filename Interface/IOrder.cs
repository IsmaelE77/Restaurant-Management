namespace Restaurant_Management.Interface
{
    public interface IOrder : ICrud<Order>
    {
        IEnumerable<Order> GetAllByEmployee(int EmployeeId);
        IEnumerable<Order> GetAllByEmployeeAndYear(int employeeId, int year);
        IEnumerable<Order> GetAllByReceipt(int Receipt_Id);
        IEnumerable<Order> GetAllByTable(int Table_Id);
    }
}
