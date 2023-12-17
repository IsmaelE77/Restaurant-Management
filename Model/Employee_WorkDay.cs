namespace Restaurant_Management.Model;

public class Employee_WorkDay
{
    public int? Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public DateTime Starts { get; set; } = DateTime.Now;
    public DateTime Ends { get; set; } = DateTime.Now;
    public int WorkingHours { get; set; }
    public string Note { get; set; } = "Nothing special";
    public int Employee_Id { get; set; }
    public override string ToString() => 
        $"Employee_WorkDay(Id: {Id}, Date: {Date:yyyy/MM/dd}, Starts: {Starts:HH:mm:ss}, Ends: {Ends:HH:mm:ss}, " +
        $"WorkingHours: {WorkingHours}, Note: {Note}, Employee_Id: {Employee_Id})";
}
