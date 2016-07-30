using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;
using Dapper;

namespace BicycleBackend.Db
{
    public class CrashContext : IDisposable
    {
        private readonly IDbConnection _conn;
        private string DbPath => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\bicyclebicycle\\crash.db";

        public CrashContext()
        {
            _conn = new SQLiteConnection($"Data Source={DbPath};");
            _conn.Open();
        }

        public IEnumerable<Crash> GetCrashes()
        {
            return _conn.Query<Crash>("select * from incidents");
        }

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}