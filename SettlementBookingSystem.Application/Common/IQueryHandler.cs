using MediatR;

namespace SettlementBookingSystem.Application.Common;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    
}