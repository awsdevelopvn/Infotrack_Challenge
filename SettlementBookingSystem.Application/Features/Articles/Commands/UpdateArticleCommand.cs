using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Mapster;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Features.Articles.Models;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Articles.Commands;

public class UpdateArticleCommand: ICommand<ResponseBase<Article>>
{
    public UpdateArticleRequest Data { get; set; }
    public User RequestUser { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class UpdateObjectiveCommandHandler: CommandBase<Article>, ICommandHandler<UpdateArticleCommand, ResponseBase<Article>>
{
    private readonly IRepository<Article> _repository;
    public UpdateObjectiveCommandHandler(IRepository<Article> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<Article>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Article>.Filter.Where(o => o.IsActive && o.Id == request.Data.Id);

        var result = await _repository.UpdatePartialByFilterAsync(filter, GetUpdateDefinition(request.Data, request.RequestUser));
        return new ResponseBase<Article>(result);
    }
    
    private static UpdateDefinition<Article> GetUpdateDefinition(UpdateArticleRequest request, User requestUser)
    {
        var update = Builders<Article>.Update.Combine();

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            update = update.Set(p => p.Title, request.Title);
        }
        
        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            update = update.Set(p => p.Description, request.Description);
        }
        
        update = update.Set(p => p.UpdatedBy, requestUser.Adapt<User>());

        return update;
    }
    
    private static T PrepareNewEntity<T>(User requestUser, T entity) where T: IEntity
    {
        // entity.CreatedBy = requestUser;
        // entity.IsActive = true;
        // entity.CreatedOn = DateTime.UtcNow;

        return entity;
    }
}

public sealed class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleCommandValidator()
    {
        RuleFor(x => x.RequestUser).NotNull()
            .WithMessage("Please ensure that you have valid login credentials")
            .OverridePropertyName("authorization bearer {token}");
        RuleFor(x => x.Data.Id).NotEmpty()
            .WithMessage("Please ensure that you have entered User Id")
            .OverridePropertyName("id");
    }
}