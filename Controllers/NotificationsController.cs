using Microsoft.AspNetCore.Mvc;
using Loan_Management_System.Services;
using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;
        public NotificationsController(INotificationService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId) => Ok(await _service.GetByUserAsync(userId));

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] EmailNotification notification)
        {
            var created = await _service.SendNotificationAsync(notification);
            return CreatedAtAction(nameof(GetByUser), new { userId = created.UserId }, created);
        }

        [HttpPost("{id}/send")]
        public async Task<IActionResult> SendById(int id)
        {
            var ok = await _service.SendNotificationByIdAsync(id);
            return ok ? Ok(new { message = "Sent" }) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? Ok(new { message = "Deleted" }) : NotFound();
        }
    }
}
