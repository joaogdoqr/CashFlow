using CashFlow.Application.UseCases.Users;
using CashFlow.Application.UseCases.Users.ChangePassoword;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Responses.User;
using CashFlow.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUser), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseError), statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUser request
            )
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ResponseUserProfile), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfile([FromServices] IGetUserProfileUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfile(
            [FromServices] IUpdateUserProfileUseCase useCase,
            [FromBody] RequestUpdateUser request)
        {
            await useCase.Execute(request);

            return NoContent();
        }

        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePassword(
           [FromServices] IChangePasswordUseCase useCase,
           [FromBody] RequestChangePassword request)
        {
            await useCase.Execute(request);

            return NoContent();
        }

        [HttpGet("admin")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
        public IActionResult IsAdmin()
        {
            return Ok("You're an admin!");
        }
    }
}
