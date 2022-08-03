using System.Threading.Tasks;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;

namespace SettlementBookingSystem.Application.Logging;

public interface ILogger
{
    Task Log(LogLevel level, string domain, object message);
    Task Log(LogLevel level, LogTraceEntity message);
}