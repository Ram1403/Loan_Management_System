using Loan_Management_System.Models;
using Loan_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ILoanDocumentService _service;

        public DocumentsController(ILoanDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("application/{appId}")]
        public async Task<IActionResult> GetByApplication(int appId) =>
            Ok(await _service.GetByApplicationAsync(appId));

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId) =>
            Ok(await _service.GetByCustomerAsync(customerId));

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending() =>
            Ok(await _service.GetPendingAsync());

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody] LoanDocument doc)
        {
            var created = await _service.AddAsync(doc);
            return CreatedAtAction(nameof(GetAll), new { id = created.DocumentId }, created);
        }

        [HttpPatch("{id}/verify")]
        public async Task<IActionResult> Verify(int id, [FromBody] VerifyRequest request)
        {
            var updated = await _service.VerifyAsync(id, request.Status, request.Remarks, request.OfficerId);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPatch("{id}/reject")]
        public async Task<IActionResult> Reject(int id, [FromBody] RejectRequest request)
        {
            var updated = await _service.RejectAsync(id, request.Remarks, request.OfficerId);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }

    public class VerifyRequest
    {
        public Status Status { get; set; }
        public string Remarks { get; set; }
        public int OfficerId { get; set; }
    }

    public class RejectRequest
    {
        public string Remarks { get; set; }
        public int OfficerId { get; set; }
    }
}
