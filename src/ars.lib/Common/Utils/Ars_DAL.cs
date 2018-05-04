using ars.lib.Common.Interfaces;
using Serilog;
using System;
using System.Data.SQLite;
using System.Diagnostics;

namespace ars.lib.Common.Utils
{
    public class Ars_DAL
    {
        private ISettings _settings;
        private string _db;
        public Ars_DAL(ISettings settings, string db)
        {
            _settings = settings;
            _db = db;
        }
        public void InitializeDB()
        {
            //if the database has already password
            try
            {
                string conn = $"Data Source={_db};Password={_settings.AppDbPwd};";
                SQLiteConnection connection = new SQLiteConnection(conn);
                connection.Open();
                //Some code               
                connection.ChangePassword(_settings.AppDbPwd);
                connection.Close();
            }
            //if it is the first time sets the password in the database
            catch (SQLiteException)
            {
                string conn = $"Data Source={_db};";
                SQLiteConnection connection = new SQLiteConnection(conn);
                connection.Open();
                //Some code
                var passGen = new PasswordGenerator();
                _settings.AppDbPwd = passGen.Generate();
                connection.ChangePassword(_settings.AppDbPwd);
                connection.Close();

                _settings.SaveSettings();
            }
            catch(Exception ex)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(0);
                string MethodName = sf.GetMethod().Name;
                Log.Error(ex, "Get {MethodName} failed: {ErrMsg}", MethodName, ex.Message);
            }
        }
    }
}
