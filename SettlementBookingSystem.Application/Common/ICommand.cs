using MediatR;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;

namespace SettlementBookingSystem.Application.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    User RequestUser { get; set; }
    ILogTrace LogTrace { get; set; }
}