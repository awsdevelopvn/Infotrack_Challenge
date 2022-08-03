using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Users.Queries;

public class GetUsersQuery: IQuery<ResponseBase<IEnumerable<User>>>
{
    public PagingCriteria<User> Criteria { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, ResponseBase<IEnumerable<User>>>
{
    private readonly IRepository<User> _repository;
    public GetUsersQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<IEnumerable<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var filter = GetFilters(request.Criteria);
        
        var result = await _repository.GetByFilterAsync(filter, null);
        
        return new ResponseBase<IEnumerable<User>>(result);
    }

    private static FilterDefinition<User> GetFilters(PagingCriteria<User> criteria)
    {
        var builder = Builders<User>.Filter;
        var filter = builder.Where(o => o.IsActive == true);
        
        if (criteria.Keywords.Any())
        {
            filter &= criteria.Keywords.Aggregate(builder.Where(p => false), (current, keyword)
                => current | builder.Where(p => p.FirstName.ToLower().Contains(keyword) 
                                                || p.LastName.ToLower().Contains(keyword)
                                                || p.Email.ToLower().Contains(keyword)));
        }

        return filter;
    }
}

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(x => x.Criteria.PageSize).NotEqual(0);
    }
}