using Loan_Management_System.Models;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _service;

        public LoanController(ILoanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _service.GetAllLoansAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _service.GetLoanByIdAsync(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Loan loan)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateLoanAsync(loan);
            return CreatedAtAction(nameof(GetById), new { id = created.LoanId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Loan updatedLoan)
        {
            if (id != updatedLoan.LoanId) return BadRequest("ID mismatch");

            var existing = await _service.GetLoanByIdAsync(id);
            if (existing == null) return NotFound();

            updatedLoan.UpdatedAt = DateTime.UtcNow;
            var updated = await _service.UpdateLoanAsync(updatedLoan);
            return Ok(updated);
        }

        [HttpPut("{id}/track-emi")]
        public async Task<IActionResult> TrackEmi(int id, [FromQuery] int emiPaid)
        {
            var updated = await _service.TrackEmiAsync(id, emiPaid);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteLoanAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }

    }

}
