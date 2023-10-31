using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //puxar nosso serviço
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //criar método que vai gerar nosso token
        [HttpPost]
        [Route("token")]
        public async Task<ActionResult> PostAsync([FromForm] UserDTO userDTO)
        {
            var result = await _userService.GenerateTokenAsync(userDTO);
            if(result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result);
        }
    }
}
