using System;
using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Loan_Management_System.Services;
using Loan_Management_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class QueriesController : ControllerBase
{
    private readonly ICustomerQueryService _service;

    public QueriesController(ICustomerQueryService service)
    {
        _service = service;
    }

    // GET: api/queries/customer/{customerId}
    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetQueriesByCustomer(int customerId) =>
        Ok(await _service.GetQueriesByCustomerAsync(customerId));

    // GET: api/queries/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = await _service.GetByIdAsync(id);
        return query == null ? NotFound() : Ok(query);
    }

    // POST: api/queries
    [HttpPost]
    public async Task<IActionResult> CreateQuery([FromBody] CustomerQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateAsync(query);
        return CreatedAtAction(nameof(GetById), new { id = created.QueryId }, created);
    }

    // PATCH: api/queries/{id}/respond
    [HttpPatch("{id}/respond")]
    [Authorize]
    public async Task<IActionResult> RespondToQuery(int id, [FromBody] RespondQueryDto dto, LoanDbContext context)
    {
        // Extract UserId from JWT
        var userIdClaim = User.FindFirst("UserId");
        if (userIdClaim == null) return Unauthorized("UserId claim missing");

        int userId = int.Parse(userIdClaim.Value);

        // ✅ Map UserId → OfficerId
        var officer = await context.LoanOfficers.FirstOrDefaultAsync(o => o.UserId == userId);
        if (officer == null) return Forbid("User is not registered as a Loan Officer");

        // ✅ Now pass OfficerId to service
        var updated = await _service.RespondToQueryAsync(id, dto.Response, officer.OfficerId);
        return updated == null ? NotFound("Query not found") : Ok(updated);
    }

    //public async Task<IActionResult> RespondToQuery(int id, [FromBody] RespondQueryDto dto)
    //{
    //    int officerId = 1; // TODO: later extract from JWT
    //    var updated = await _service.RespondToQueryAsync(id, dto.Response, officerId);
    //    return updated == null ? NotFound("Query not found") : Ok(updated);
    //}

    // DELETE: api/queries/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuery(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return !deleted ? NotFound() : Ok(new { message = "Query deleted successfully" });
    }
}

public class RespondQueryDto
{
    public string Response { get; set; } = string.Empty;
}
