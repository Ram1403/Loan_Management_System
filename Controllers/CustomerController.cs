using Loan_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers() =>
        Ok(await _service.GetAllCustomersAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _service.GetCustomerByIdAsync(id);
        return customer == null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.AddCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = created.CustomerId }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
    {
        if (id != customer.CustomerId) return BadRequest("ID mismatch");
        var updated = await _service.UpdateCustomerAsync(customer);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpPatch("{id}/verify")]
    public async Task<IActionResult> VerifyCustomer(int id)
    {
        var updated = await _service.VerifyCustomerAsync(id);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpPatch("{id}/reject")]
    public async Task<IActionResult> RejectCustomer(int id, [FromBody] RejectCustomerDto dto)
    {
        var updated = await _service.RejectCustomerAsync(id, dto.Reason);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var deleted = await _service.DeleteCustomerAsync(id);
        return deleted == null ? NotFound() : Ok(new { message = "Customer deleted successfully" });
    }
}

public class RejectCustomerDto
{
    public string Reason { get; set; } = string.Empty;
}




//using Loan_Management_System.Models;
//using Loan_Management_System.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Loan_Management_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {
//        private readonly ICustomerService _service;

//        public CustomerController(ICustomerService service)
//        {
//            _service = service;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var customers = await _service.GetAll();
//            return Ok(customers);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(int id)
//        {
//            var customer = await _service.GetById(id);
//            if (customer == null) return NotFound();
//            return Ok(customer);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] Customer customer)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);
//            await _service.Add(customer);
//            return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, customer);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, [FromBody] Customer updatedCustomer)
//        {
//            if (!ModelState.IsValid || id != updatedCustomer.CustomerId)
//                return BadRequest("Invalid data or ID mismatch.");

//            var existingCustomer = await _service.GetById(id);
//            if (existingCustomer == null)
//                return NotFound($"Customer with ID {id} not found.");

//            // repo me bhi ye kar sakte hai
//            existingCustomer.FirstName = updatedCustomer.FirstName;
//            existingCustomer.LastName = updatedCustomer.LastName;
//            existingCustomer.DateOfBirth = updatedCustomer.DateOfBirth;
//            existingCustomer.Address = updatedCustomer.Address;
//            existingCustomer.CreditScore = updatedCustomer.CreditScore;
//            existingCustomer.VerifiedAt = updatedCustomer.VerifiedAt;
//            existingCustomer.UpdatedAt = DateTime.UtcNow;
//            existingCustomer.Gender = updatedCustomer.Gender;
//            existingCustomer.City = updatedCustomer.City;
//            existingCustomer.Occupation = updatedCustomer.Occupation;
//            existingCustomer.AnnualIncome = updatedCustomer.AnnualIncome;
//            existingCustomer.PanNumber = updatedCustomer.PanNumber;
//            existingCustomer.AadhaarNumber = updatedCustomer.AadhaarNumber;
//            existingCustomer.DocumentType = updatedCustomer.DocumentType;
//            existingCustomer.DocumentPath = updatedCustomer.DocumentPath;
//            existingCustomer.VerificationStatus = updatedCustomer.VerificationStatus;

//            await _service.Update(existingCustomer);
//            return Ok(existingCustomer);
//        }

//        [HttpDelete("{id}")]
//        //[Authorize]

//        public async Task<IActionResult> Delete(int id)
//        {
//            var existing = await _service.GetById(id);
//            if (existing == null) return NotFound();

//            await _service.Delete(id);
//            return Ok(existing);
//            //return NoContent();
//        }
//    }
//}
