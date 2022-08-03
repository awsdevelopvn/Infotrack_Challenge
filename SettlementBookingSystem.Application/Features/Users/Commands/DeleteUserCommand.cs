using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Users.Commands;

public class DeleteUserCommand: ICommand<ResponseBase<bool>>
{
    public string UserId { get; set; }
    public User RequestUser { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class DeleteUserCommandHandler: ICommandHandler<DeleteUserCommand, ResponseBase<bool>>
{
    private readonly IRepository<User> _repository;
    public DeleteUserCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseBase<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteAsync(request.UserId);
        return new ResponseBase<bool>(result);
    }
}

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
            .OverridePropertyName("id")
            .WithMessage("Please ensure that you have entered User Id");
    }
}