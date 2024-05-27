using Domain.Entidades.Log;
using Domain.Interfaces.Log;
using Infra.Configuracao;
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
        private readonly ContextBase _contextBase;

        public LogRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public void SaveLog(LogEntry logEntry)
        {
            var log = new LogEntry()
            {
                Level = logEntry.Level,
                Message = logEntry.Message,
                Timestamp = logEntry.Timestamp,
            };
            _contextBase.Logs.Add(log);
            _contextBase.SaveChanges();

        }
    }
}
