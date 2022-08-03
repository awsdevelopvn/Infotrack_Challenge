using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Mapster;
using MongoDB.Driver;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;
using Varoom.Admin.Service.Features.User.Models;

namespace SettlementBookingSystem.Application.Features.Users.Commands;

public class UpdateUserCommand: ICommand<ResponseBase<User>>
{
    public UpdateUserRequest Data { get; set; }
    public User RequestUser { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class UpdateUserCommandHandler: CommandBase<User>, ICommandHandler<UpdateUserCommand, ResponseBase<User>>
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;
    public UpdateUserCommandHandler(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResponseBase<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        
        var filter = Builders<User>.Filter.Where(u => u.IsActive && u.Id == request.Data.Id);
        var result = await _repository.UpdatePartialByFilterAsync(filter, GetUpdateDefinition(request.Data, request.RequestUser));
        return new ResponseBase<User>(result);
    }

    private static UpdateDefinition<User> GetUpdateDefinition(UpdateUserRequest request, User requestUser)
    {
        var update = Builders<User>.Update.Combine();

        // if (!string.IsNullOrWhiteSpace(request.FirstName))
        // {
        //     update = update.Set(p => p.FirstName, request.FirstName);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.LastName))
        // {
        //     update = update.Set(p => p.LastName, request.LastName);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.Fullname))
        // {
        //     update = update.Set(p => p.Fullname, request.Fullname);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.Position))
        // {
        //     update = update.Set(p => p.Position, request.Position);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.Department))
        // {
        //     update = update.Set(p => p.Department, request.Department);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.DepartmentId))
        // {
        //     update = update.Set(p => p.DepartmentId, request.DepartmentId);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.CompanyId))
        // {
        //     update = update.Set(p => p.CompanyId, request.CompanyId);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.CompanyDomain))
        // {
        //     update = update.Set(p => p.CompanyDomain, request.CompanyDomain);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.Supervisor))
        // {
        //     update = update.Set(p => p.Supervisor, request.Supervisor);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.SupervisorId))
        // {
        //     update = update.Set(p => p.SupervisorId, request.SupervisorId);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.SupervisorEmail))
        // {
        //     update = update.Set(p => p.SupervisorEmail, request.SupervisorEmail);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.MobilePhone))
        // {
        //     update = update.Set(p => p.MobilePhone, request.MobilePhone);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.ImageURL))
        // {
        //     update = update.Set(p => p.ImageURL, request.ImageURL);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.Division))
        // {
        //     update = update.Set(p => p.Division, request.Division);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.EmployeeNumber))
        // {
        //     update = update.Set(p => p.EmployeeNumber, request.EmployeeNumber);
        // }
        //
        // if (!string.IsNullOrWhiteSpace(request.EmployeeId))
        // {
        //     update = update.Set(p => p.EmployeeId, request.EmployeeId);
        // }
        
        update = update.Set(p => p.UpdatedBy, requestUser.Adapt<User>());

        return update;
    }
}

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Data.Id).NotEmpty()
            .OverridePropertyName("id")
            .WithMessage("Please ensure that you have entered User Id");
    }
}