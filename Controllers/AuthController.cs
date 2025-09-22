using Loan_Management_System.Models;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            LoginResponseViewModel response;
            if (ModelState.IsValid)
            {
                response = _authService.Login(model);
                if (response.User != null)
                {
                    //return Ok(response);
                    return Ok(response.Token);

                }
                return Unauthorized();
            }
            return BadRequest();

        }
    }
}

