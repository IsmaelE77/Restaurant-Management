namespace Restaurant_Management.Interface;

public interface ICategory : ICrud<Category>
{
    public int? GetId(string name);
}
