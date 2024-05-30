using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO.KennelDTO;
using ServiceContracts.Mappers;

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
        public async Task<IActionResult> GetKennels()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var kennels = await _kennelService.GetKennelsAsync();
            var kennelDtos = kennels.Select(k => k.ToKennelDto()).ToList();
            return Ok(kennelDtos);
        }

        [HttpGet("{kennelId:int}")]
        public async Task<IActionResult> GetKennelById([FromRoute] int kennelId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var kennelModel = await _kennelService.GetKennelByIdAsync(kennelId);
            if (kennelModel == null)
            {
                return NotFound("Kennel does not exist");
            }
            return Ok(kennelModel.ToKennelDto());
        }

        [HttpPost]
        public async Task<IActionResult> AddKennel([FromBody] KennelAddRequest KennelAddRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }
            var kennelModel = KennelAddRequest.ToKennelFromAdd();
            await _kennelService.AddKennelAsync(kennelModel);
            return CreatedAtAction(nameof(GetKennelById), new { kennelId = kennelModel.KennelId }, kennelModel.ToKennelDto());
        }

        [HttpPut("{kennelId:int}")]
        public async Task<IActionResult> UpdateKennel([FromRoute] int kennelId, [FromBody] KennelUpdateRequest kennelUpdateRequest)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }
            var kennelModel = await _kennelService.UpdateKennelAsync(kennelId, kennelUpdateRequest.ToKennelFromUpdate());
            if (kennelModel == null)
            {
                return NotFound("Kennel does not exist");
            }
            return Ok(kennelModel.ToKennelDto());
        }

        [HttpDelete("{kennelId:int}")]
        public async Task<IActionResult> DeleteKennel(int kennelId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var kennelModel = await _kennelService.RemoveKennelAsync(kennelId);
            if (kennelModel == null)
            {
                return NotFound("Kennel does not exist");
            }
            return Ok(kennelModel);
        }
    }
}
