using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalaryInsights.Application.Dtos.Setup;
using SalaryInsights.Domain.Dtos;

namespace SalaryInsights.API.Controllers
{
    /// <summary>
    /// Setup Wizard Endpoint
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SetupController(IMediator mediator) : ControllerBase
    {
    }
}