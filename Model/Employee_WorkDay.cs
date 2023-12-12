namespace Restaurant_Management.Model;

public class Employee_WorkDay()
{
    [Key]
    public int? Id { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [Timestamp]
    public DateTime Date { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "{0} is required")]
    [Timestamp]
    public DateTime Starts { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "{0} is required")]
    [Timestamp]
    public DateTime Ends { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "{0} is required")]
    [Range(0, double.MaxValue, ErrorMessage = "{0} must be a non-negative value")]
    public int WorkingHours { get; set; }
    public string Note { get; set; } = "Nothing special";
    [Required(ErrorMessage = "{0} is required")]
    public int EmployeeId { get; set; }
}
