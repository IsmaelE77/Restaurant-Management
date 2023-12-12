namespace Restaurant_Management.Interface;

public interface ICategory : ICRUD<Category>
{
    public int? GetId(string name);
}
