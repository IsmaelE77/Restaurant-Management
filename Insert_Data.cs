namespace Restaurant_Management;

public class InsertData
{
    private readonly OracleConnection con;
    public InsertData(OracleConnection connection)
    {
        con = connection;
        connection.Open();
    }
    ~InsertData()
    {
        con.Close();
    }
}