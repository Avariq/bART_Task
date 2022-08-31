using bART_Task.API.Models.Input;
using bART_Task.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bART_Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IAllInOneService _allInOneService;

        public IncidentsController(IAllInOneService allInOneService)
        {
            _allInOneService = allInOneService;
        }

        [HttpPost(nameof(CreateIncident))]
        public async Task<IActionResult> CreateIncident(IncidentArgs incidentArgs)
        {
            try
            {
                var accountArgs = incidentArgs.AccountArgs;
                var contactArgs = incidentArgs.ContactArgs;

                await _allInOneService.CreateIncident(accountArgs.AccountName, contactArgs.FirstName,
                                                                            contactArgs.LastName, contactArgs.Email, incidentArgs.Description);

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
