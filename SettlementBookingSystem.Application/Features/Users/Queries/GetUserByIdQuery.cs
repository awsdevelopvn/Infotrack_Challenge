using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Users.Queries;

public class GetUserByIdQuery: IQuery<ResponseBase<User>>
{
    public string Id { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, ResponseBase<User>>
{
    private readonly IRepository<User> _repository;
    public GetUserByIdQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindAsync(request.Id);
        if (user == null)
        {
            return new ResponseBase<User>(new List<Error>
                {new() {ErrorCode = 404, Message = $"User with id {request.Id} is not found"}});
        }
        return new ResponseBase<User>(user);
    }
}