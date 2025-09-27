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
        public async Task<IActionResult> GetAllOfficers() =>
            Ok(await _service.GetAllOfficersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfficerById(int id)
        {
            var officer = await _service.GetOfficerByIdAsync(id);
            return officer == null ? NotFound() : Ok(officer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOfficer([FromBody] LoanOfficer officer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateOfficerAsync(officer);
            return CreatedAtAction(nameof(GetOfficerById), new { id = created.OfficerId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOfficer(int id, [FromBody] LoanOfficer officer)
        {
            if (id != officer.OfficerId) return BadRequest("ID mismatch");
            var updated = await _service.UpdateOfficerAsync(officer);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficer(int id)
        {
            var deleted = await _service.DeleteOfficerAsync(id);
            return deleted == null ? NotFound() : Ok(new { message = "Officer deleted successfully" });
        }

        [HttpPatch("{id}/workload")]
        public async Task<IActionResult> UpdateWorkload(int id, [FromBody] WorkloadDto dto)
        {
            var updated = await _service.UpdateWorkloadAsync(id, dto.Count);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpPost("{id}/assign-application")]
        public async Task<IActionResult> AssignLoanApplication(int id, [FromBody] AssignAppDto dto)
        {
            var application = await _service.AssignLoanApplicationAsync(id, dto.ApplicationId);
            return application == null ? NotFound() : Ok(application);
        }

        [HttpPatch("{id}/verify-document")]
        public async Task<IActionResult> VerifyDocument(int id, [FromBody] VerifyDocDto dto)
        {
            var doc = await _service.VerifyDocumentAsync(id, dto.DocumentId, dto.Status, dto.Remarks);
            return doc == null ? NotFound("Officer or Document not found") : Ok(doc);
        }


        [HttpPost("{id}/respond-query")]
        public async Task<IActionResult> RespondToQuery(int id, [FromBody] OfficerRespondQueryDto dto)
        {
            var query = await _service.RespondToQueryAsync(id, dto.QueryId, dto.Response);
            return query == null ? NotFound() : Ok(query);
        }

        [HttpGet("{id}/applications")]
        public async Task<IActionResult> GetAssignedApplications(int id) =>
            Ok(await _service.GetAssignedApplicationsAsync(id));

        [HttpGet("{id}/documents")]
        public async Task<IActionResult> GetVerifiedDocuments(int id) =>
            Ok(await _service.GetVerifiedDocumentsAsync(id));

        [HttpGet("{id}/queries")]
        public async Task<IActionResult> GetHandledQueries(int id) =>
            Ok(await _service.GetHandledQueriesAsync(id));

        [HttpPatch("{id}/performance")]
        public async Task<IActionResult> UpdatePerformance(int id, [FromBody] PerformanceDto dto)
        {
            var updated = await _service.UpdatePerformanceAsync(id, dto.Rating);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateOfficer(int id)
        {
            var updated = await _service.DeactivateOfficerAsync(id);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpPatch("{id}/reactivate")]
        public async Task<IActionResult> ReactivateOfficer(int id)
        {
            var updated = await _service.ReactivateOfficerAsync(id);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpGet("{id}/audit-log")]
        public async Task<IActionResult> GetAuditLog(int id) =>
            Ok(await _service.GetAuditLogAsync(id));

        [HttpGet("{id}/dashboard")]
        public async Task<IActionResult> GetDashboard(int id)
        {
            var apps = await _service.GetAssignedApplicationsAsync(id);
            var docs = await _service.GetVerifiedDocumentsAsync(id);
            var queries = await _service.GetHandledQueriesAsync(id);

            return Ok(new { applications = apps, documents = docs, queries = queries });
        }
    }

    public class WorkloadDto { public int Count { get; set; } }
    public class AssignAppDto { public int ApplicationId { get; set; } }
    public class VerifyDocDto
    {
        public int DocumentId { get; set; }
        public Status Status { get; set; }   // Pending, Approved, Rejected
        public string Remarks { get; set; } = string.Empty;
    }
    public class OfficerRespondQueryDto { public int QueryId { get; set; } public string Response { get; set; } = ""; }
    public class PerformanceDto { public decimal Rating { get; set; } }

}
