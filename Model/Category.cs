namespace Restaurant_Management.Model;

public class Category
{
    [Key]
    public int? Id { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Length of Section shouldn't exceed 100")]
    public string Name { get; set; } = string.Empty;
}
