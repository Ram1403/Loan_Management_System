using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllApplicationsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var app = await _service.GetApplicationByIdAsync(id);
        return app == null ? NotFound() : Ok(app);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LoanApplication application)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateApplicationAsync(application);
        return CreatedAtAction(nameof(GetById), new { id = created.ApplicationId }, created);
    }

    [HttpPatch("{id}/status")]
    //[Authorize(Roles = "LoanOfficer")]
    public async Task<IActionResult> UpdateStatus(int id, [FromQuery] Status newStatus, [FromBody] string? remarks)
    {
        var updated = await _service.UpdateApplicationStatusAsync(id, newStatus, remarks);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpPatch("{id}/assign")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignOfficer(int id, [FromQuery] int officerId)
    {
        var updated = await _service.AssignLoanOfficerAsync(id, officerId);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteApplicationAsync(id);
        return deleted ? Ok() : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] LoanApplication application)
    {
        if (id != application.ApplicationId) return BadRequest("ID mismatch");
        var updated = await _service.UpdateApplicationAsync(application);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(Status status) =>
        Ok(await _service.GetByStatusAsync(status));

    [HttpGet("daterange")]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end) =>
        Ok(await _service.GetByDateRangeAsync(start, end));

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q) =>
        Ok(await _service.SearchAsync(q));

    [HttpGet("customer/{customerId}/recent")]
    public async Task<IActionResult> GetRecentByCustomer(int customerId) =>
        Ok(await _service.GetRecentByCustomerAsync(customerId));

    [HttpGet("customer/{customerId}/status/{status}")]
    public async Task<IActionResult> GetByCustomerAndStatus(int customerId, Status status) =>
        Ok(await _service.GetByCustomerAndStatusAsync(customerId, status));

    [HttpGet("customer/{customerId}/summary")]
    public async Task<IActionResult> GetSummaryByCustomer(int customerId) =>
        Ok(await _service.GetSummaryByCustomerAsync(customerId));


}



//old version:
//using Loan_Management_System.Models;
//using Loan_Management_System.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Loan_Management_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoanApplicationController : ControllerBase
//    {
//        private readonly ILoanApplicationService _service;

//        public LoanApplicationController(ILoanApplicationService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var applications = await _service.GetAllApplicationsAsync();
//            return Ok(applications);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var application = await _service.GetApplicationByIdAsync(id);
//            if (application == null) return NotFound();
//            return Ok(application);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] LoanApplication application)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            var created = await _service.CreateApplicationAsync(application);
//            return CreatedAtAction(nameof(GetById), new { id = created.ApplicationId }, created);
//        }

//        [HttpPut("{id}/status")]
//        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] Status newStatus, [FromBody] string? remarks)
//        {
//            var updated = await _service.UpdateApplicationStatusAsync(id, newStatus, remarks);
//            if (updated == null) return NotFound();
//            return Ok(updated);
//        }

//        [HttpPut("{id}/assign-officer")]
//        public async Task<IActionResult> AssignOfficer(int id, [FromQuery] int officerId)
//        {
//            var updated = await _service.AssignLoanOfficerAsync(id, officerId);
//            if (updated == null) return NotFound();
//            return Ok(updated);
//        }
//    }

//}
