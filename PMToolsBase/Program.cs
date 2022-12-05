using PMToolsBase.Core;

namespace PMToolsBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string connectionString = "APIKey=keys6KaOJAsbRXStR; BaseId=appSjOiqevcG5pX1B; TableNames=SP21_Test; ViewNames=SP21_Test.Grid view;";
            

            //// A I R T A B L E

            //AirtableConnectionStringBuilder builder = new AirtableConnectionStringBuilder();
            ////Pass the connection string builder an existing connection string, and you can get and set any of the elements as strongly typed properties.
            //builder.ConnectionString = "";
            ////Now that the connection string has been parsed,
            //// you can work with individual items:
            //builder.ApiKey = "keys6KaOJAsbRXStR";
            //builder.BaseId = "appSjOiqevcG5pX1B";
            //builder.TableNames = "SP21_Test";
            //builder.ViewNames = "SP21_Test.Grid view";

            //// You can refer to connection keys using strings,
            //// as well.
            //builder["Logfile"] = "test.log";
            //builder["Verbosity"] = 5;

            //using (AirtableConnection connection = new AirtableConnection(builder.ConnectionString))
            //{
            //    connection.Open();

            //    AirtableCommand cmd = new AirtableCommand("SELECT * FROM SP21_Test", connection);
            //    AirtableDataReader rdr = cmd.ExecuteReader();
            //}


            var engine = new Engine();
            engine.Run();
        }
    }
}
