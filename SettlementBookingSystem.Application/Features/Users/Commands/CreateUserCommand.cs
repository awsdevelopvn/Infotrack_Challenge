using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;
using Varoom.Admin.Service.Features.User.Models;

namespace SettlementBookingSystem.Application.Features.Users.Commands;

public class CreateUserCommand: ICommand<ResponseBase<User>>
{
    public User RequestUser { get; set; }
    public AddUserRequest Data { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class CreateUserCommandHandler: CommandBase<User>, ICommandHandler<CreateUserCommand, ResponseBase<User>>
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public CreateUserCommandHandler(IRepository<User> repository, IMapper mapper, IMediator mediator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public async Task<ResponseBase<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await UserExists(request.Data.Email))
        {
            return new ResponseBase<User>(new List<Error>{new() {ErrorCode = 409, Message = $"The use with email {request.Data.Email} is already existed"}});
        }
        
        var creatingUser = _mapper.Map<User>(request.Data);
        creatingUser = PrepareNewEntity(creatingUser, _mapper.Map<User>(request.RequestUser));
        
        request.LogTrace.Add(LogLevel.Info, $"{nameof(CreateUserCommandHandler)} - Handler ", new {requestUser = creatingUser});
        var user = await _repository.InsertAsync(creatingUser);
        return new ResponseBase<User>(user);
    }

    private async Task<bool> UserExists(string email)
    {
        var users = await _repository.GetByFilterAsync(
            Builders<User>.Filter.Where(u => u.Email == email && u.IsActive), null);
        return users.Any();
    }
}

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Data.Email).NotEmpty()
            .WithMessage("Please ensure that you have entered your Email address")
            .OverridePropertyName("email");
    }
}