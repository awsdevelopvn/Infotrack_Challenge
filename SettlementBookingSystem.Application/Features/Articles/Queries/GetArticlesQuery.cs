using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Features.Articles.Models;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Articles.Queries;

public class GetArticlesQuery: IQuery<ResponseBase<IEnumerable<Article>>>
{
    public User RequestUser { get; set; }
    public ArticleFilterCriteria Criteria { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class GetArticlesQueryHandler : IQueryHandler<GetArticlesQuery, ResponseBase<IEnumerable<Article>>>
{
    private readonly IRepository<Article> _repository;
    public GetArticlesQueryHandler(IRepository<Article> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<IEnumerable<Article>>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        //var filter = GetFilters(request.Criteria);
        //var result = await _repository.GetByFilterAsync(filter,null);
        return new ResponseBase<IEnumerable<Article>>(new List<Article>
        {
            new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Question = "Question 1",
                DidAnswer = true,
                Date = DateTime.UtcNow
            },
            new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Question = "Question 1",
                DidAnswer = true,
                Date = DateTime.UtcNow
            }
        });
    }

    private static FilterDefinition<Article> GetFilters(ArticleFilterCriteria criteria)
    {
        var builder = Builders<Article>.Filter;
        var filter = builder.Where(o => o.IsActive == true);
        
        if (criteria.StartDate.HasValue && criteria.StartDate != default(DateTime))
        {
            filter &= builder.And(builder.Gte(o=>o.CreatedOn, criteria.StartDate));
        }
        
        if (criteria.EndDate.HasValue && criteria.EndDate != default(DateTime))
        {
            filter &= builder.And(builder.Lte(o=>o.CreatedOn, criteria.EndDate));
        }
        
        if (criteria.Keywords.Any())
        {
            filter &= criteria.Keywords.Aggregate(builder.Where(p => false), (current, keyword)
                => current | builder.Where(p => p.Title.ToLower().Contains(keyword) || p.Description.ToLower().Contains(keyword)));
        }

        return filter;
    }
}