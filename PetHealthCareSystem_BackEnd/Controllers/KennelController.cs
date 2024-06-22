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

            var kennel = await _kennelService.GetKennelsAsync();
            return Ok(kennel);
        }

        [HttpGet("{kennelId:int}")]
        public async Task<IActionResult> GetKennelById([FromRoute] int kennelId)
        {
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
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }
            var kennelModel = KennelAddRequest.ToKennelFromAdd();
            var result = await _kennelService.AddKennelAsync(kennelModel);
            return Ok(result.ToKennelDto());
        }

        [HttpPut("{kennelId:int}")]
        public async Task<IActionResult> UpdateKennel([FromRoute] int kennelId, [FromBody] KennelUpdateRequest kennelUpdateRequest)
        {
            if (!ModelState.IsValid) 
            {
                string errorMessage = string.Join(",", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
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
            return Ok(isUpdated.ToKennelDto());
        }

        [HttpDelete("{kennelId:int}")]
        public async Task<IActionResult> DeleteKennel(int kennelId)
        {
            var kennel = await _kennelService.GetKennelByIdAsync(kennelId);
            //kennel is occupied
            if(kennel.IsAvailable == false || kennel == null)
            {
                return BadRequest("Kennel is currently occupied or missing");
            }
            var kennelModel = await _kennelService.RemoveKennelAsync(kennelId);
            if (kennelModel == null)
            {
                return NotFound("Delete Failed");
            }
            return Ok(kennel);
        }
    }
}
