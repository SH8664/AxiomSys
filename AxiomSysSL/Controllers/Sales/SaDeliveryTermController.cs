using AxiomSysBL.IServices.ISalesServices;
using AxiomSysBL.Models.Sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AxiomSysSL.Controllers.Sales
{

    [Authorize]
    [Route("api/Sales/[controller]")]
    [ApiController]
    public class SaDeliveryTermController : Controller
    {
        ISaDeliveryTermService _saDeliveryTermService;
        UserManager<IdentityUser> _userManager;

        public SaDeliveryTermController(ISaDeliveryTermService saDeliveryTermService, UserManager<IdentityUser> userManager)
        {
            _saDeliveryTermService = saDeliveryTermService;
            _userManager = userManager;

        }

        [HttpGet("GetAllSaDeliveryTerms")]
        public IActionResult GetAll()
        {
            var result = _saDeliveryTermService.GetSaDeliveryTerms();
            return Ok(result);
        }

        [HttpPost("AddSaDeliveryTerm")]
        public IActionResult AddSaDeliveryTerm(SaDeliveryTermModel saDeliveryTermDto)
        {
            if (!TryGetCurrentUserId(out var userId))
            {
                return Unauthorized();
            }
            var result = _saDeliveryTermService.AddSaDeliveryTerm(saDeliveryTermDto, userId!);
            return Ok(result);
        }

        [HttpPost("UpdateSaDeliveryTerm")]
        public IActionResult UpdateSaDeliveryTerm(SaDeliveryTermModel saDeliveryTermDto)
        {
            if (!TryGetCurrentUserId(out var userId))
            {
                return Unauthorized();
            }
            var result = _saDeliveryTermService.UpdateSaDeliveryTerm(saDeliveryTermDto, userId!);
            return Ok(result);
        }

        [HttpGet("GetSaDeliveryTermByCode/{code}")]
        public IActionResult GetSaDeliveryTermByCode(short code)
        {
            var result = _saDeliveryTermService.GetSaDeliveryTermByCode(code);
            return Ok(result);
        }

        [HttpDelete("DeleteSaDeliveryTerm/{code}")]
        public IActionResult DeleteSaDeliveryTerm(short code)
        {
            var result = _saDeliveryTermService.DeleteSaDeliveryTerm(code);
            return Ok(result);
        }
        private bool TryGetCurrentUserId(out string? userId)
        {
            userId = _userManager.GetUserId(User);
            return userId != null;
        }

    }
}
