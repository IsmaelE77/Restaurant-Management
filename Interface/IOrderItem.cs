namespace Restaurant_Management.Interface
{
    public interface IOrderItem : ICRUD<OrderItem>
    {
        IEnumerable<OrderItem> GetAllByOrder(int OrderId);
    }
}
