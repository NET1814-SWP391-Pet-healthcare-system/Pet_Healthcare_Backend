using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.KennelDTO;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KennelController : Controller
    {
        private readonly IKennelService _kennelService;
        public KennelController(IKennelService kennelService)
        {
            _kennelService = kennelService;
        }

        [HttpGet]
        public IActionResult GetKennels()
        {
            var Kennels = _kennelService.GetKennels();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Kennels);
        }

        [HttpGet("{KennelId}")]
        public IActionResult GetKennelById(int KennelId)
        {
            var Kennel = _kennelService.GetKennelById(KennelId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Kennel);
        }

        [HttpPost]
        public IActionResult AddKennel([FromBody] KennelAddRequest KennelAddRequest)
        {
            if (KennelAddRequest == null) { return BadRequest(ModelState); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _kennelService.AddKennel(KennelAddRequest);
            return Ok("successfully created");
        }

        [HttpPut("{KennelId}")]
        public IActionResult UpdateKennel(int KennelId, [FromBody] KennelUpdateRequest KennelUpdateRequest)
        {
            if (KennelUpdateRequest == null) { return BadRequest(ModelState); }
            if (_kennelService.GetKennelById(KennelId) == null) { return NotFound(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_kennelService.UpdateKennel(KennelId, KennelUpdateRequest))
            {
                ModelState.AddModelError("", "Something went wrong updating Kennel");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully updated");
        }

        [HttpDelete("{KennelId}")]
        public IActionResult DeleteKennel(int KennelId)
        {
            if (_kennelService.GetKennelById(KennelId) == null) { return NotFound(); }
            if (!_kennelService.RemoveKennel(KennelId))
            {
                ModelState.AddModelError("", "Something went wrong deleting Kennel");
            }
            return Ok("successfully deleted");
        }
    }
}
