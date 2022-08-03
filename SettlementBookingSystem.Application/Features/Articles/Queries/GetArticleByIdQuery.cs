using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Articles.Queries;

public class GetArticleByIdQuery: IQuery<ResponseBase<Article>>
{
    public string Id { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class GetArticleByIdQueryHandler : IQueryHandler<GetArticleByIdQuery, ResponseBase<Article>>
{
    private readonly IRepository<Article> _repository;
    public GetArticleByIdQueryHandler(IRepository<Article> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<Article>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var objective = await _repository.FindAsync(request.Id);
        if (objective == null)
        {
            return new ResponseBase<Article>(new List<Error>
                {new() {ErrorCode = 404, Message = $"Objective with id {request.Id} is not found"}});
        }

        return new ResponseBase<Article>(objective);
    }
}