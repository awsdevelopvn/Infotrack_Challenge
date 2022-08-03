using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Logging;

public class LogTrace: LogTraceBase, ILogTrace
{
    public LogTrace(ILogger logger)
    {
        _logger = logger;
    }
        
    static LogLevel _globalLogLevel = LogLevel.Verbose;
    /// <summary>
    /// Global LOG_LEVEL (should set by environment variable or config file)
    /// </summary>
    public static LogLevel GlobalLogLevel
    {
        get { return _globalLogLevel; }
        set { _globalLogLevel = value; }
    }

        
    public void Flush()
    {
        lock (_lock)
        {
            var entries = FilterLogEntries(_entries, GlobalLogLevel);

            if (entries != null && entries.Any())
            {
                _logger.Log(LogLevel.Info, new LogTraceEntity()
                {
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    Message = entries,
                    ProcessingMilliseconds = DateTime.Now.Subtract(_startTime).TotalMilliseconds
                });
            }
        }
    }
      
    private static IEnumerable<LogEntry> FilterLogEntries(IEnumerable<LogEntry> entries, LogLevel logLevel)
    {
        // if Entries contains a entry with ERROR or FATAL level, keep all Entries. this help us to investigate why the error occured
        if (entries.Any(e => e.Level == LogLevel.Error) || entries.Any(e => e.Level == LogLevel.Fatal))
        {
            return entries;
        }

        // filter log entries by global LOG_LEVEL
        return entries.Where(e => e.Level >= logLevel);
    }
    
    /// <summary>
    /// Stringify
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private string Stringify(object message)
    {
        if (message == null)
        {
            return string.Empty;
        }
        if (message.GetType().Equals(typeof(System.String)))
        {
            return (string)message;
        }
        return JsonConvert.SerializeObject(message, Formatting.Indented);
    }
}