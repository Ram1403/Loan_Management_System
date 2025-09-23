using Loan_Management_System.Models;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _service;

        public LoanApplicationController(ILoanApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _service.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _service.GetApplicationByIdAsync(id);
            if (application == null) return NotFound();
            return Ok(application);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoanApplication application)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateApplicationAsync(application);
            return CreatedAtAction(nameof(GetById), new { id = created.ApplicationId }, created);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] Status newStatus, [FromBody] string? remarks)
        {
            var updated = await _service.UpdateApplicationStatusAsync(id, newStatus, remarks);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPut("{id}/assign-officer")]
        public async Task<IActionResult> AssignOfficer(int id, [FromQuery] int officerId)
        {
            var updated = await _service.AssignLoanOfficerAsync(id, officerId);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }

}
