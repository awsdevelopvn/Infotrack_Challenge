using System;
using SettlementBookingSystem.Application.Common;

namespace SettlementBookingSystem.Application.Logging;

public interface ILogTrace
{
    /// <summary>
    /// Add log entry to queue
    /// </summary>
    /// <param name="level"></param>
    /// <param name="domain"></param>
    /// <param name="message"></param>
    /// <param name="prevTime"></param>
    void Add(LogLevel level, string domain, object message, DateTime? prevTime = null);
    void AddError(System.Exception ex, DateTime? prevTime = null);
    void Flush();
}