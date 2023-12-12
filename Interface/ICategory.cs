namespace Restaurant_Management.Interface;

public interface ICategory : ISpecial<Category>
{
    public int? GetId(string name);
}
