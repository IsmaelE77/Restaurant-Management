namespace Restaurant_Management.Interface
{
    public interface IOrderItemRepository
    {
        OrderItem? Get(int OrderItemId);
        List<OrderItem> GetAll();
        List<OrderItem> GetAllByOrder(int OrderId);
        bool Add(OrderItem OrderItem);
        bool Update(OrderItem OrderItem);
        bool Delete(int OrderItemId);
    }
}
