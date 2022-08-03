using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SettlementBookingSystem.Application.Common;
using SettlementBookingSystem.Application.Entities;
using SettlementBookingSystem.Application.Features.Articles.Commands;
using SettlementBookingSystem.Application.Features.Articles.Models;
using SettlementBookingSystem.Application.Features.Articles.Queries;

namespace SettlementBookingSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseBase<Article>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseBase<Article>>> Create([FromBody] CreateArticleCommand command)
        {
            var result =  await _mediator.Send(command);
            return Ok(result);
        }
        
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Article>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Article>>> Get([FromQuery] ArticleFilterCriteria query)
        {
            var result =  await _mediator.Send(new GetArticlesQuery
            {
                Criteria = query
            });
            return Ok(result.Data);
        }
    }
}
