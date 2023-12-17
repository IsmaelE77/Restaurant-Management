namespace Restaurant_Management.Model;

public class Employee
{
    public int? Id { get; set; }
    public int? ManagerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal SalaryPerHour { get; set; }
    public int SectionId { get; set; }
    public List<Employee_WorkDay> WorkDay { get; set; } = [];

    public override string ToString() =>
        $"Employee(Id: {Id}, ManagerId: {ManagerId}, FirstName: {FirstName}, LastName: {LastName}, PhoneNumber: {PhoneNumber}, " +
        $"Address: {Address}, SalaryPerHour: {SalaryPerHour}, SectionId: {SectionId})";
}
