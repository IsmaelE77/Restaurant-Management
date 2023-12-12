namespace Restaurant_Management.Model;

public class Table(int _Number, int? _Id = null, int _Status = 0)
{
    [Key]
    public int? Id { get; set; } = _Id;
    [Required(ErrorMessage="{0} is required")]
    public int Number { get; set; } = _Number;
    [Range(0, 1, ErrorMessage="Invalid status")]
    public int? Status { get; set; } = _Status;
    public override string ToString() => $"Table(Id: {Id}, Number: {Number}, Status: {Status})";
}
