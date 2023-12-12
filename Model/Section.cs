namespace Restaurant_Management.Model;

public class Section(int? _Id = null, string? _Name = "")
{
    [Key]
    public int? Id { get; set; } = _Id;
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Length of Section shouldn't exceed 100")]
    public string Name { get; set; } = _Name ?? string.Empty;
    public override string ToString() => $"Section(Id: {Id}, Name: {Name})";
}
