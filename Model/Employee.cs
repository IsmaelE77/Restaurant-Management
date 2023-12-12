namespace Restaurant_Management.Model;

public class Employee
{
    [Key]
    public int? Id { get; set; }
    public int? ManagerId { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Length of FirstName shouldn't exceed 100")]
    public string FirstName { get; set; } = string.Empty;
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Length of LastName shouldn't exceed 100")]
    public string LastName { get; set; } = string.Empty;
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Length of PhoneNumber shouldn't exceed 20")]
    [DataType(DataType.PhoneNumber)]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    [StringLength(255, MinimumLength = 1, ErrorMessage = "Length of Address shouldn't exceed 100")]
    public string Address { get; set; } = string.Empty;
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value")]
    public decimal SalaryPerHour { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public int SectionId { get; set; }

    public override string ToString()
    {
        return $"Employee(Id: {Id}, ManagerId: {ManagerId}, FirstName: {FirstName}, LastName: {LastName}, " +
                $"PhoneNumber: {PhoneNumber}, Address: {Address}, SalaryPerHour: {SalaryPerHour}, SectionId: {SectionId})";
    }
}
