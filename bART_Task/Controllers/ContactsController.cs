using bART_Task.API.Models.Input;
using bART_Task.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bART_Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IAllInOneService _allInOneService;

        public ContactsController(IAllInOneService allInOneService)
        {
            _allInOneService = allInOneService;
        }

        [HttpPost(nameof(CreateContact))]
        public async Task<IActionResult> CreateContact(ContactArgs contactArgs)
        {
            try
            {
                await _allInOneService.CreateContact(contactArgs.FirstName, contactArgs.LastName, contactArgs.Email);

                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
