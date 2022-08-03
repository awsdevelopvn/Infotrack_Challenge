using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Logging;

public class ConsoleLogger: ILogger
{
    public Task Log(LogLevel level, string domain, object message)
    {
        Console.WriteLine($"[{level}] - {domain}: {JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.Indented)}");
        return Task.CompletedTask;
    }

    public Task Log(LogLevel level, LogTraceEntity message)
    {
        foreach (var logEntry in message.Message)
        {
            Console.WriteLine($"[{logEntry.Level}] - {logEntry.Domain}: {JsonConvert.SerializeObject(logEntry.Message, Newtonsoft.Json.Formatting.Indented)}");
        }
        
        return Task.CompletedTask;
    }
}