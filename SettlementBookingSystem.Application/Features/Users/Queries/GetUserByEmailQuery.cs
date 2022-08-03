using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Users.Queries;

public class GetUserByEmailQuery: IQuery<ResponseBase<User>>
{
    public string Email { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, ResponseBase<User>>
{
    private readonly IRepository<User> _repository;
    public GetUserByEmailQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var builder = Builders<User>.Filter;
        var filter = builder.Where(x => x.Email == request.Email && x.IsActive);
        var users = await _repository.GetByFilterAsync(filter, null!);
        if (!users.Any())
        {
            return new ResponseBase<User>(new List<Error>
                {new() {ErrorCode = 404, Message = $"Not found user with email {request.Email}"}});
        }
        
        var user = users.FirstOrDefault();
        return new ResponseBase<User>(user);

    }
}