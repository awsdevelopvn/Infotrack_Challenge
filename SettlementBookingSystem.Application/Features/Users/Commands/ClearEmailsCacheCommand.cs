using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Logging;
using SettlementBookingSystem.Application.Repositories;

namespace SettlementBookingSystem.Application.Features.Users.Commands;

public class ClearEmailsCacheCommand : ICommand<bool>
{
    public string CompanyDomain { get; set; }
    public User RequestUser { get; set; }
    public ILogTrace LogTrace { get; set; }
}

public class ClearEmailsCacheCommandHandler : ICommandHandler<ClearEmailsCacheCommand, bool>
{
    private readonly IRepository<User> _repository;
    //private readonly IDistributedCache _distributedCache;
    public ClearEmailsCacheCommandHandler(IRepository<User> repository)
    {
        _repository = repository;
        //_distributedCache = distributedCache;
    }
    
    public async Task<bool> Handle(ClearEmailsCacheCommand request, CancellationToken cancellationToken)
    {
        var cacheKey = $"USERS:{request.CompanyDomain}:emails";
        //await _distributedCache.RemoveAsync(cacheKey);
        return true;
    }
}

public sealed class ClearEmailsCacheCommandValidator : AbstractValidator<ClearEmailsCacheCommand>
{
    public ClearEmailsCacheCommandValidator()
    {
        RuleFor(x => x.CompanyDomain).NotEmpty()
            .WithMessage("Please ensure that you have entered company domain");
    }
}