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

// This function only user for import users where we don't know exact the user information
public class GetUserByNameQuery: IQuery<ResponseBase<User>>
{
    public string Fullname { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class GetUserByNameQueryHandler : IQueryHandler<GetUserByNameQuery, ResponseBase<User>>
{
    private readonly IRepository<User> _repository;
    public GetUserByNameQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<User>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        var builder = Builders<User>.Filter;
        var filter = builder.Where(x =>
            (x.Fullname == request.Fullname || (x.FirstName == request.FirstName && x.LastName == request.LastName)) &&
            x.IsActive);
        var users = await _repository.GetByFilterAsync(filter, null!);
        if (!users.Any())
        {
            return new ResponseBase<User>(new List<Error>
                {new() {ErrorCode = 404, Message = $"Not found user with fullname {request.Fullname}"}});
        }
        
        var user = users.FirstOrDefault();
        return new ResponseBase<User>(user);

    }
}