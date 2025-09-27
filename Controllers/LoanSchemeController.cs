using Loan_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SchemesController : ControllerBase
{
    private readonly ILoanSchemeService _service;

    public SchemesController(ILoanSchemeService service)
    {
        _service = service;
    }

    // GET: api/schemes
    [HttpGet]
    public async Task<IActionResult> GetAllSchemes() =>
        Ok(await _service.GetAllSchemesAsync());

    // GET: api/schemes/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSchemeById(int id)
    {
        var scheme = await _service.GetSchemeByIdAsync(id);
        return scheme == null ? NotFound() : Ok(scheme);
    }

    // POST: api/schemes
    [HttpPost]
    public async Task<IActionResult> CreateScheme([FromBody] LoanScheme scheme)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateSchemeAsync(scheme);
        return CreatedAtAction(nameof(GetSchemeById), new { id = created.SchemeId }, created);
    }

    // PUT: api/schemes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateScheme(int id, [FromBody] LoanScheme scheme)
    {
        if (id != scheme.SchemeId) return BadRequest("ID mismatch");
        var updated = await _service.UpdateSchemeAsync(scheme);
        return updated == null ? NotFound() : Ok(updated);
    }

    // DELETE: api/schemes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteScheme(int id)
    {
        var deleted = await _service.DeleteSchemeAsync(id);
        return !deleted ? NotFound() : Ok(new { message = "Scheme deleted successfully" });
    }
}
