﻿using MediatR;

namespace SettlementBookingSystem.Application.Common;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    
}