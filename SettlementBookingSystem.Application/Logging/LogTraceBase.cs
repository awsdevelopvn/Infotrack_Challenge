using System;
using System.Collections.Generic;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Logging
{
    public abstract class LogTraceBase
    {
        protected object _lock = new object();
        protected IList<LogEntry> _entries = new List<LogEntry>();

        protected ILogger _logger;
        
        protected DateTime _startTime;

        protected DateTime _prevTimeOfLog;
        
        protected LogTraceBase()
        {
            _startTime = DateTime.UtcNow;
            _prevTimeOfLog = DateTime.UtcNow;
        }
        
        /// <summary>
        /// Add log into _entries
        /// </summary>
        /// <param name="level"></param>
        /// <param name="domain"></param>
        /// <param name="message"></param>
        /// <param name="endTime">Utc DateTime</param>
        public void Add(LogLevel level, string domain, object message, DateTime? endTime = null)
        {
            lock (_lock)
            {
                // implement time elaspsed
                int? timeElapsed = null;
                if (endTime != null)
                {
                    timeElapsed = endTime?.Subtract(_prevTimeOfLog).Milliseconds;
                    _prevTimeOfLog = endTime ?? DateTime.UtcNow;
                }

                _entries.Add(new LogEntry { Domain = domain, Message = message, TimeElapsed = timeElapsed });
            }
        }
        
        public void AddError(System.Exception ex, DateTime? prevTime = null)
        {
            lock (_lock)
            {
                Add(LogLevel.Error, ex.Message, ex.StackTrace, prevTime);
            }
        }
    }
}