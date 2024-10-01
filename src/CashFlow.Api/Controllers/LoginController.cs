using CashFlow.Application.UseCases.Login;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.User;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUser), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromServices] ILoginUseCase useCase,
            RequestLogin request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

    }
}
