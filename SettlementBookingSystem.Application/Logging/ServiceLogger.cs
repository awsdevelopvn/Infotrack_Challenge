using System;
using System.Threading.Tasks;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Logging;

public class ServiceLogger: ILogger
{
    public Task Log(LogLevel level, string domain, object message)
    {
        throw new NotImplementedException();
    }

    public Task Log(LogLevel level, LogTraceEntity message)
    {
        throw new NotImplementedException();
    }
}