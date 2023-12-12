namespace Restaurant_Management.Model
{
    public class Employee
    {
        public int? Id { get; set; }
        public int? ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal SalaryPerHour { get; set; }
        public int SectionId { get; set; }
    }
}
