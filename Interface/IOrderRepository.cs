namespace Restaurant_Management.Interface
{
    public interface IOrderRepository
    {
        Order? Get(int OrderId);
        List<Order> GetAll();
        List<Order> GetAllByEmploye(int EmployeeId);
        List<Order> GetAllByEmployeeAndYear(int employeeId, int year);
        bool Add(Order Order);
        bool Update(Order Order);
        bool Delete(int OrderId);
    }
}
