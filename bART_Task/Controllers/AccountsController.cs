using bART_Task.API.Models.Input;
using bART_Task.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bART_Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAllInOneService _allInOneService;

        public AccountsController(IAllInOneService allInOneService)
        {
            _allInOneService = allInOneService;
        }

        [HttpPost(nameof(CreateAccount))]
        public async Task<IActionResult> CreateAccount(AccountCreationArgs accountArgs)
        {
            try
            {
                await _allInOneService.CreateAccount(accountArgs.AccountName, accountArgs.ContactEmail);

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
