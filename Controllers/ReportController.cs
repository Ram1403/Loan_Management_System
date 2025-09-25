using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Loan_Management_System.Services.Report;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportService _service;
    private readonly ReportGeneratorService _reportGenerator;

    public ReportController(IReportService service, ReportGeneratorService reportGenerator) // ✅ inject here
    {
        _service = service;
        _reportGenerator = reportGenerator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var report = await _service.GetByIdAsync(id);
        return report == null ? NotFound() : Ok(report);
    }

    [HttpGet("admin/{adminId}")]
    public async Task<IActionResult> GetByAdmin(int adminId) =>
        Ok(await _service.GetByAdminAsync(adminId));

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] Report report)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var created = await _service.CreateAsync(report);
        return CreatedAtAction(nameof(GetById), new { id = created.ReportId }, created);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? Ok(new { message = "Report deleted successfully" }) : NotFound();
    }



    [HttpPost("generate")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> Generate([FromQuery] string reportType, [FromQuery] int adminId,[FromQuery] string format = "pdf" )
    {
        //int adminId = 1; // TODO: take from JWT (User.Identity.Name or Claim)

        var filePath = await _reportGenerator.GenerateReportAsync(reportType, adminId, format);

        return Ok(new { message = "Report generated successfully", filePath });
    }

    [HttpGet("download/{id}")]
    //[Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Download(int id)
    {
        var report = await _service.GetByIdAsync(id);
        if (report == null) return NotFound();

        if (!System.IO.File.Exists(report.FilePath))
            return NotFound(new { message = "Report file not found" });

        var fileBytes = await System.IO.File.ReadAllBytesAsync(report.FilePath);
        var fileName = Path.GetFileName(report.FilePath);

        // detect content type
        string contentType = fileName.EndsWith(".pdf") ? "application/pdf"
                             : fileName.EndsWith(".xlsx") ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                             : "application/octet-stream";

        return File(fileBytes, contentType, fileName);
    }

}
