namespace Restaurant_Management.Interface
{
    public interface IItem : ICrud<Item>
    {
         public IEnumerable<Item> GetAllByCategoryId(int Item_Id);
    }
}
