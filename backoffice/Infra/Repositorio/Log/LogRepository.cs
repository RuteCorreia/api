using Domain.Entidades.Log;
using Domain.Interfaces.Log;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.Log
{
    public class LogRepository : ILogRepository
    {
        private readonly string _connectionString;

        public LogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveLog(LogEntry logEntry)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Logs (Level, Message, Timestamp) VALUES (@Level, @Message, @Timestamp)";
            }
        }
    }
}
