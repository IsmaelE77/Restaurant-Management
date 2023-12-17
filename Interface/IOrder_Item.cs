namespace Restaurant_Management.Interface;

public interface IOrder_Item : ICrud<Order_Item>
{
    public IEnumerable<Order_Item> GetOrder_ItemsForOrder(int Order_Id);
    public IEnumerable<Order_Item> GetOrder_ItemForItem(int Item_Id);
    public bool RemoveByOrderId(int OrderId);
}
