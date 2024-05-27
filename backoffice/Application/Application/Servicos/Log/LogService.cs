using Application.DTOs.Log.Interface;
using Domain.Entidades.Log;
using Domain.Interfaces.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Servicos.Log
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;
        private readonly ILogRepository _logRepository;

        public LogService(ILogger<LogService> logger, ILogRepository logRepository)
        {
            _logger = logger;
            _logRepository = logRepository;
        }
        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
            _logRepository.SaveLog(new LogEntry { Level = "Information", Message = message, Timestamp = DateTime.UtcNow });
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
            _logRepository.SaveLog(new LogEntry { Level = "Warning", Message = message, Timestamp = DateTime.UtcNow });
        }

        public void LogError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
            _logRepository.SaveLog(new LogEntry { Level = "Error", Message = $"{message}: {exception}", Timestamp = DateTime.UtcNow });
        }
    }
}
