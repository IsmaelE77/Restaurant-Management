namespace Restaurant_Management.Interface
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        IEnumerable<OrderItem> GetAllByOrder(int OrderId);
    }
}
