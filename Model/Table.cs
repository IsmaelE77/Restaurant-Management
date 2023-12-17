namespace Restaurant_Management.Model;

public class Table
{
    public int? Id { get; set; }
    public int Number { get; set; }
    public string? Status { get; set; }
    public override string ToString() => $"Table(Id: {Id}, Number: {Number}, Status: {Status})";
}
