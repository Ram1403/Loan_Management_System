using Loan_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RepaymentController : ControllerBase
{
    private readonly IRepaymentService _service;

    public RepaymentController(IRepaymentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var repayment = await _service.GetByIdAsync(id);
        return repayment == null ? NotFound() : Ok(repayment);
    }

    [HttpGet("loan/{loanId}")]
    public async Task<IActionResult> GetByLoan(int loanId) =>
        Ok(await _service.GetByLoanIdAsync(loanId));

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(int customerId) =>
        Ok(await _service.GetByCustomerIdAsync(customerId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Repayment repayment)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(repayment);
        return CreatedAtAction(nameof(GetById), new { id = created.RepaymentId }, created);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted == null ? NotFound() : Ok(deleted);
    }

}
