using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Features.Users.Models;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Users.Commands;

public class UserLoginCommand: ICommand<ResponseBase<UserLoginResponse>>
{
    public User RequestUser { get; set; }
    public UserLoginRequest Data { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class UserLoginCommandHandler: CommandBase<User>, ICommandHandler<UserLoginCommand, ResponseBase<UserLoginResponse>>
{
    private readonly IRepository<User> _repository;
    private readonly IMediator _mediator;
    public UserLoginCommandHandler(IRepository<User> repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<ResponseBase<UserLoginResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        if (!(await ValidUserLogin(request.Data)))
        {
            return new ResponseBase<UserLoginResponse>(new List<Error>{new() {ErrorCode = 409, Message = "Username or password is not correct"}});
        }
        
        return new ResponseBase<UserLoginResponse>(new UserLoginResponse
        {
            Name = "Test User",
            AccessToken = Guid.NewGuid().ToString()
        });
    }

    private static async Task<bool> ValidUserLogin(UserLoginRequest userLoginRequest)
    {
        // TODO: validate user login
        return await Task.FromResult(true);
    }
}

public sealed class UserLoginCommandValidator : AbstractValidator<CreateUserCommand>
{
    public UserLoginCommandValidator()
    {
        RuleFor(x => x.Data.Email).NotEmpty()
            .WithMessage("Please ensure that you have entered your Email address")
            .OverridePropertyName("email");
    }
}