using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Loan_Management_System.Services;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Loan_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
    

    public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

    [HttpPost]
    public async Task<IActionResult> CreateAdmin(LoanAdmin loanAdmin)
        {
            var createdAdmin = await _adminService.Create(loanAdmin);
            return CreatedAtAction(nameof(GetAdminById), new { id = createdAdmin.AdminId }, createdAdmin);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _adminService.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] LoanAdmin loanAdmin)
        {
            if (id != loanAdmin.AdminId)
            {
                return BadRequest();
            }
            var updatedAdmin = await _adminService.Update(loanAdmin);
            if (updatedAdmin == null)
            {
                return NotFound();
            }
            return Ok(updatedAdmin);
        }
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var deletedAdmin = await _adminService.Delete(id);
            if (deletedAdmin == null)
            {
                return NotFound();
            }
            return Ok(deletedAdmin);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _adminService.GetAll();
            return Ok(admins);
        }
    }
}

