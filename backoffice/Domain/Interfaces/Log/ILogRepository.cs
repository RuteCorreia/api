using Domain.Entidades.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Log
{
    public interface ILogRepository
    {
        void SaveLog(LogEntry logEntry);
    }
}
