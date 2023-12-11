using Oracle.ManagedDataAccess.Client;

namespace Restaurant
{
    public class Program
    {
        private static readonly string  CONNECTION_STRING = "DATA SOURCE=localhost:1521/XEPDB1;PERSIST SECURITY INFO=True;USER ID=admin;Password=admin";

        public static void Main()
        {
        }
        public static OracleConnection getConnection()
        {
            return new OracleConnection(CONNECTION_STRING);
        }
    }
}