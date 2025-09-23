using Loan_Management_System.Models;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanSchemeController : ControllerBase
    {
        private readonly ILoanSchemeService _service;

        public LoanSchemeController(ILoanSchemeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schemes = await _service.GetAllSchemesAsync();
            return Ok(schemes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var scheme = await _service.GetSchemeByIdAsync(id);
            if (scheme == null) return NotFound();
            return Ok(scheme);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoanScheme scheme)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateSchemeAsync(scheme);
            return CreatedAtAction(nameof(GetById), new { id = created.SchemeId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LoanScheme updatedScheme)
        {
            if (id != updatedScheme.SchemeId) return BadRequest("ID mismatch");

            var existing = await _service.GetSchemeByIdAsync(id);
            if (existing == null) return NotFound();

            // Manual field updates
            existing.SchemeName = updatedScheme.SchemeName;
            existing.InterestRate = updatedScheme.InterestRate;
            existing.MaxAmount = updatedScheme.MaxAmount;
            existing.MinAmount = updatedScheme.MinAmount;
            existing.TenureMonths = updatedScheme.TenureMonths;
            existing.EligibilityCriteria = updatedScheme.EligibilityCriteria;
            existing.IsActive = updatedScheme.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _service.UpdateSchemeAsync(existing);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteSchemeAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }

}
