using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Application.Commands.CreateSalary;

namespace SalaryInsights.API.Controllers
{
    /// <summary>
    /// Salaries Endpoint
    /// </summary>
    /// <param name="mediator"></param>
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SalariesController(IMediator mediator) : ControllerBase
    {
        #region APIs

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSalary([FromBody] CreateSalaryCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return Created("", result);
        }

        #endregion
    }
}