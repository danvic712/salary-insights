using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Application.Commands.CreateSalary;

namespace SalaryInsights.API.Controllers
{
    [Route("api/[controller]")]
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