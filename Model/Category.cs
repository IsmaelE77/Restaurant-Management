namespace Restaurant_Management.Model;

public class Category
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public override string ToString() => $"Category(Id: {Id}, Name: {Name})";
}
