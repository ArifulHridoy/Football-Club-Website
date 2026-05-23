using System.Configuration;
using System.Data.SqlClient;

public class DBHelper
{
    public static SqlConnection GetConnection()
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        return con;
    }
}
