using Loan_Management_System.Models;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanOfficerController : ControllerBase
    {
        private readonly ILoanOfficerService _service;

        public LoanOfficerController(ILoanOfficerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var officers = await _service.GetAllOfficersAsync();
            return Ok(officers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var officer = await _service.GetOfficerByIdAsync(id);
            if (officer == null) return NotFound();
            return Ok(officer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoanOfficer officer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateOfficerAsync(officer);
            return CreatedAtAction(nameof(GetById), new { id = created.OfficerId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoanOfficer updatedOfficer)
        {
            if (id != updatedOfficer.OfficerId) return BadRequest("ID mismatch");

            var existing = await _service.GetOfficerByIdAsync(id);
            if (existing == null) return NotFound();

            // Manual field updates
            existing.UserId = updatedOfficer.UserId;
            existing.City = updatedOfficer.City;
            existing.Designation = updatedOfficer.Designation;
            existing.MaxLoansAssigned = updatedOfficer.MaxLoansAssigned;
            existing.CurrentWorkload = updatedOfficer.CurrentWorkload;
            existing.Specialization = updatedOfficer.Specialization;
            existing.PerformanceRating = updatedOfficer.PerformanceRating;
            existing.IsActive = updatedOfficer.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _service.UpdateOfficerAsync(existing);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteOfficerAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }

}
