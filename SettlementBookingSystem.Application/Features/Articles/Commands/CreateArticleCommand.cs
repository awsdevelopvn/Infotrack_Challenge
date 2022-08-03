using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Mapster;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Features.Articles.Models;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Articles.Commands;

public class CreateArticleCommand: ICommand<ResponseBase<Article>>
{
    public User RequestUser { get; set; }
    public AddArticleRequest Data {get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class CreateArticleCommandHandler : CommandBase<Article>, ICommandHandler<CreateArticleCommand, ResponseBase<Article>>
{
    private readonly IRepository<Article> _repository;
    public CreateArticleCommandHandler(IRepository<Article> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<Article>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var newEntity = request.Data.Adapt<Article>();

        newEntity = PrepareNewEntity(newEntity, request.RequestUser.Adapt<User>());
        
        var result = await _repository.InsertAsync(newEntity);
        return new ResponseBase<Article>(result);
    }
}

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(o => o.Data.Title).NotEmpty()
            .WithMessage("Please ensure that you have entered Title.")
            .OverridePropertyName("title");
    }
}