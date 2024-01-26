using Npgsql;

namespace APISquad7.Infraestrutura
{
    public class DbConnection : IDisposable
    {
        public NpgsqlConnection Connection { get; set; }

        public DbConnection()
        {
            //local
            //Connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=api_orange_portfolio;User Id=postgres;Password=vencer;");
            //servidor teste
            Connection = new NpgsqlConnection("Server=kesavan.db.elephantsql.com;Database=iilkoaqc;User Id=iilkoaqc;Password=9uwdeEAg2Pt5cc7VrxuQOCetAh4pBDMn;");
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
