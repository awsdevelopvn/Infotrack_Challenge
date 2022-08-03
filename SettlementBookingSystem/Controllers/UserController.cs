using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Features.Users.Commands;
using SettlementBookingSystem.Application.Features.Users.Models;
using SettlementBookingSystem.Application.Features.Users.Queries;

namespace SettlementBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("login")]
        [HttpPost]
        [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserLoginResponse>> Create([FromBody] UserLoginRequest command)
        {
            var result =  await _mediator.Send(new UserLoginCommand
            {
                Data = command
            });
            return Ok(result.Data);
        }
        
        
        [HttpGet]
        [ProducesResponseType(typeof(ResponseBase<IEnumerable<User>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseBase<IEnumerable<User>>>> Get([FromQuery] GetUsersQuery query)
        {
            var result =  await _mediator.Send(query);
            return Ok(result);
        }
    }
}
