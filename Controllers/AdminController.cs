using Loan_Management_System.Models;
using Loan_Management_System.Services.Report;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly IAdminService _service;
    private readonly ReportGeneratorService _reportGenerator;

    public AdminsController(IAdminService service, ReportGeneratorService reportGenerator)
    {
        _service = service;
        _reportGenerator = reportGenerator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdminById(int id)
    {
        var admin = await _service.GetAdminByIdAsync(id);
        return admin == null ? NotFound() : Ok(admin);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdmins() =>
        Ok(await _service.GetAllAdminsAsync());


    [HttpPost]
    public async Task<IActionResult> CreateAdmin(LoanAdmin loanAdmin)
    {
        var createdAdmin = await _service.CreateAdminAsync(loanAdmin);
        return CreatedAtAction(nameof(GetAdminById), new { id = createdAdmin.AdminId }, createdAdmin);
    }




    [HttpPost("{adminId}/loan-schemes")]
    public async Task<IActionResult> CreateLoanScheme(int adminId, [FromBody] LoanScheme scheme) =>
        Ok(await _service.CreateLoanSchemeAsync(adminId, scheme));

    [HttpPut("{adminId}/loan-schemes/{schemeId}")]
    public async Task<IActionResult> UpdateLoanScheme(int adminId, int schemeId, [FromBody] LoanScheme scheme)
    {
        var updated = await _service.UpdateLoanSchemeAsync(schemeId, scheme);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{adminId}/loan-schemes/{schemeId}")]
    public async Task<IActionResult> DeleteLoanScheme(int adminId, int schemeId)
    {
        var deleted = await _service.DeleteLoanSchemeAsync(schemeId);
        return !deleted ? NotFound() : Ok(new { message = "Loan scheme deleted" });
    }

    [HttpPost("{adminId}/assign-officer")]
    public async Task<IActionResult> AssignOfficer(int adminId, [FromBody] AssignOfficerDto dto)
    {
        var application = await _service.AssignOfficerToApplicationAsync(dto.ApplicationId, dto.OfficerId);
        return application == null ? NotFound("Application or Officer not found") : Ok(application);
    }

    [HttpPost("{adminId}/reports")]
    public async Task<IActionResult> GenerateReport(int adminId, [FromBody] ReportRequestDto dto)
    {
        var filePath = await _reportGenerator.GenerateReportAsync(dto.Type, adminId, "pdf");
        return Ok(new { message = "Report generated successfully", filePath });
    }

    [HttpGet("{id}/audit-log")]
    public async Task<IActionResult> GetAuditLog(int id) =>
        Ok(await _service.GetAuditLogAsync(id));

    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> DeactivateAdmin(int id)
    {
        var updated = await _service.DeactivateAdminAsync(id);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpPatch("{id}/reactivate")]
    public async Task<IActionResult> ReactivateAdmin(int id)
    {
        var updated = await _service.ReactivateAdminAsync(id);
        return updated == null ? NotFound() : Ok(updated);
    }
}

public class AssignOfficerDto
{
    public int ApplicationId { get; set; }
    public int OfficerId { get; set; }
}
public class ReportRequestDto { public string Type { get; set; } = string.Empty; }










//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Loan_Management_System.Services;
//using Loan_Management_System.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Authorization;

//namespace Loan_Management_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AdminController : ControllerBase
//    {
//        private readonly IAdminService _adminService;


//    public AdminController(IAdminService adminService)
//        {
//            _adminService = adminService;
//        }

//    [HttpPost]
//    public async Task<IActionResult> CreateAdmin(LoanAdmin loanAdmin)
//        {
//            var createdAdmin = await _adminService.Create(loanAdmin);
//            return CreatedAtAction(nameof(GetAdminById), new { id = createdAdmin.AdminId }, createdAdmin);
//        }
//        [HttpGet("{id}")]

//        public async Task<IActionResult> GetAdminById(int id)
//        {
//            var admin = await _adminService.GetById(id);
//            if (admin == null)
//            {
//                return NotFound();
//            }
//            return Ok(admin);
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] LoanAdmin loanAdmin)
//        {
//            if (id != loanAdmin.AdminId)
//            {
//                return BadRequest();
//            }
//            var updatedAdmin = await _adminService.Update(loanAdmin);
//            if (updatedAdmin == null)
//            {
//                return NotFound();
//            }
//            return Ok(updatedAdmin);
//        }
//        [HttpDelete("{id}")]
//        //[Authorize]
//        public async Task<IActionResult> DeleteAdmin(int id)
//        {
//            var deletedAdmin = await _adminService.Delete(id);
//            if (deletedAdmin == null)
//            {
//                return NotFound();
//            }
//            return Ok(deletedAdmin);
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllAdmins()
//        {
//            var admins = await _adminService.GetAll();
//            return Ok(admins);
//        }
//    }
//}

