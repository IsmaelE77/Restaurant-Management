namespace Restaurant_Management.Model;

public class Section
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public override string ToString() => 
        $"Section(Id: {Id}, Name: {Name})";
}
