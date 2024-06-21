using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO.KennelDTO;
using ServiceContracts.Mappers;

namespace PetHealthCareSystem_BackEnd.Controllers
{
  //  [Authorize(Policy = "AdminEmployeePolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class KennelController : Controller
    {
        private readonly IKennelService _kennelService;
        private readonly IHospitalizationService _hospitalizationService;
        public KennelController(IKennelService kennelService, IHospitalizationService hospitalizationService)
        {
            _kennelService = kennelService;
            _hospitalizationService = hospitalizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetKennels()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _kennelService.GetKennelsAsync());
        }

        [HttpGet("{kennelId:int}")]
        public async Task<IActionResult> GetKennelById([FromRoute] int kennelId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _kennelService.GetKennelByIdAsync(kennelId);
            if (result == null)
            {
                return NotFound("Kennel does not exist");
            }
            return Ok(result);
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
            var existingkennel = await _kennelService.GetKennelByIdAsync(kennelId); 
            if (existingkennel == null) 
            { 
                return NotFound("Kennel does not exist"); 
            }

            var kennelModel = kennelUpdateRequest.ToKennelFromUpdate();
            kennelModel.KennelId = kennelId;
            var isUpdated = await _kennelService.UpdateKennelAsync(kennelModel);
            if(isUpdated == null)
            {
                return BadRequest(ModelState);
            }
            return Ok(isUpdated);
        }

        [HttpDelete("{kennelId:int}")]
        public async Task<IActionResult> DeleteKennel(int kennelId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var kennel = await _kennelService.GetKennelByIdAsync(kennelId);
            //kennel is occupied
            if(kennel.IsAvailable == false)
            {
                return BadRequest("Kennel is currently occupied");
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
