namespace Restaurant_Management;

public class CreateTables
{
    private readonly OracleConnection con;
    public CreateTables(OracleConnection connection)
    {
        con = connection;
        connection.Open();
    }
    ~CreateTables()
    {
        con.Close();
    }
}
