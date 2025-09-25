using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class NpaController : ControllerBase
{
    private readonly INpaService _service;

    public NpaController(INpaService service)
    {
        _service = service;
    }

    // GET: api/npa
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    // GET: api/npa/loan/{loanId}
    [HttpGet("loan/{loanId}")]
    public async Task<IActionResult> GetByLoan(int loanId) =>
        Ok(await _service.GetByLoanIdAsync(loanId));

    // POST: api/npa
    [HttpPost]
    public async Task<IActionResult> FlagAsNpa([FromBody] Npa npa)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.FlagAsNpaAsync(npa);
        return CreatedAtAction(nameof(GetByLoan), new { loanId = created.LoanId }, created);
    }
    // DELETE: api/npa/{id}
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? Ok(new { message = $"NPA {id} deleted successfully" }) : NotFound();
    }

}
