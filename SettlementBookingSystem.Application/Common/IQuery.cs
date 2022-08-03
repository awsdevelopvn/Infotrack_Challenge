using MediatR;
using SettlementBookingSystem.Application.Logging;

namespace SettlementBookingSystem.Application.Common;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    public ILogTrace LogTrace { get; set; }
}