namespace Restaurant_Management;

public class Program
{
    private const string CONNECTION_STRING = "DATA SOURCE=localhost:1521/XEPDB1;PERSIST SECURITY INFO=True;USER ID=admin;Password=admin";

    public static void Main()
    {
       Database.CreateTables(GetConnection());
    }
    public static OracleConnection GetConnection()
    {
        return new OracleConnection(CONNECTION_STRING);
    }
}