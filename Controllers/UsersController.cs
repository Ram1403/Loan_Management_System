using System.Text.Json;
using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    //// GET: api/users/me
    //[HttpGet("me")] //use this for hard code user profile
    //public async Task<IActionResult> GetProfile()
    //{
    //    // ⚡ For now, just simulate a logged-in user (id=1)
    //    // Later, extract from JWT claims
    //    int userId = 1;
    //    var user = await _service.GetProfileAsync(userId);
    //    return user == null ? NotFound() : Ok(user);
    //}


    [HttpGet("me")]
    [Authorize] // using this for extracting user profile from token
    public async Task<IActionResult> GetProfile()
    {
        // Extract userId from claims
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("UserId not found in token");

        int userId = int.Parse(userIdClaim);

        var user = await _service.GetProfileAsync(userId);
        return user == null ? NotFound() : Ok(user);
    }


    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAllUsers() =>
        Ok(await _service.GetAllUsersAsync());

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = created.UserId }, created);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.UserId) return BadRequest("ID mismatch");
        var updated = await _service.UpdateUserAsync(user);
        return updated == null ? NotFound() : Ok(updated);
    }

    // PATCH: api/users/{id}/role
    [HttpPatch("{id}/role")]
    public async Task<IActionResult> UpdateUserRole(int id, [FromBody] JsonElement body)
    {
        string? roleString = null;

        if (body.ValueKind == JsonValueKind.String)
        {
            // Case 1: raw string
            roleString = body.GetString();
        }
        else if (body.ValueKind == JsonValueKind.Object && body.TryGetProperty("role", out var roleProp))
        {
            // Case 2: object { "role": "Admin" }
            roleString = roleProp.GetString();
        }

        if (string.IsNullOrEmpty(roleString))
            return BadRequest("Invalid payload. Send either \"Admin\" or { \"role\": \"Admin\" }");

        if (!Enum.TryParse<Role>(roleString, true, out var role))
            return BadRequest($"Invalid role. Allowed values: {string.Join(", ", Enum.GetNames(typeof(Role)))}");

        var updated = await _service.UpdateUserRoleAsync(id, role);
        return updated == null ? NotFound() : Ok(updated);
    }


    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _service.DeleteUserAsync(id);
        return deleted == null ? NotFound() : Ok(new { message = "User soft deleted successfully" });
    }
}




//public class UpdateRoleDto
//{
//    public string Role { get; set; } = string.Empty;
//}



//using Microsoft.AspNetCore.Http;
//using Loan_Management_System.Services;
//using Loan_Management_System.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;

//namespace Loan_Management_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userService;
//        public UserController(IUserService userService)
//        {
//            _userService = userService;
//        }
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetUserById(int id)
//        {
//            var user = await _userService.GetById(id);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            return Ok(user);
//        }
//        [HttpPost]
//        public async Task<IActionResult> CreateUser(User user)
//        {
//            var createdUser = await _userService.Create(user);
//            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
//        {
//            if (id != user.UserId)
//            {
//                return BadRequest();
//            }
//            var updatedUser = await _userService.Update(user);
//            if (updatedUser == null)
//            {
//                return NotFound();
//            }
//            return Ok(updatedUser);
//        }
//        [HttpDelete("{id}")]
//        //[Authorize(Roles = nameof(Role.Customer))]
//        public async Task<IActionResult> DeleteUser(int id)
//        {
//            var deletedUser = await _userService.Delete(id);
//            if (deletedUser == null)
//            {
//                return NotFound();
//            }
//            return Ok(deletedUser);
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllUsers()
//        {
//            var users = await _userService.GetAll();
//            return Ok(users);
//        }


//    }
//}
