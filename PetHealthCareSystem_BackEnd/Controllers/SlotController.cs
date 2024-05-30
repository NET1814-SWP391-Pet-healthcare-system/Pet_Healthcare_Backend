using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts;
using ServiceContracts.DTO.SlotDTO;
using ServiceContracts.Mappers;

namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : Controller
    {
        private readonly ISlotService _slotService;
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSlots() 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slots = await _slotService.GetSlotsAsync();
            var slotDtos = slots.Select(s => s.ToSlotDto()).ToList();
            return Ok(slotDtos);
        }

        [HttpGet("{slotId:int}")]
        public async Task<IActionResult> GetSlotById([FromRoute] int slotId) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slot = await _slotService.GetSlotByIdAsync(slotId);
            if (slot == null)
            {
                return NotFound("Slot does not exist");
            }
            var slotDto = slot.ToSlotDto();
            return Ok(slotDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSlot([FromBody] SlotAddRequest slotAddRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slots = await _slotService.GetSlotsAsync();
            var slotModel = slotAddRequest.ToSlotFromAdd();
            if (SlotValidation.IsOverlapping(slotModel, slots))
            { 
                return BadRequest("Slot time cannot overlap with other slots"); 
            }
            await _slotService.AddSlotAsync(slotModel);
            return CreatedAtAction(nameof(GetSlotById), new { slotId = slotModel.SlotId }, slotModel.ToSlotDto());
        }

        [HttpPut("{slotId:int}")]
        public async Task<IActionResult> UpdateSlot([FromRoute] int slotId, [FromBody] SlotUpdateRequest slotUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slots = await _slotService.GetSlotsAsync();
            if (SlotValidation.IsOverlapping(slotUpdateRequest.ToSlotFromUpdate(), slots))
            {
                return BadRequest("Slot time cannot overlap with other slots");
            }
            var slotModel = await _slotService.UpdateSlotAsync(slotId, slotUpdateRequest.ToSlotFromUpdate());
            if (slotModel == null)
            {
                return NotFound("Slot does not exist");
            }
            var slotDto = slotModel.ToSlotDto();
            return Ok(slotDto);
        }

        [HttpDelete("{slotId:int}")]
        public async Task<IActionResult> DeleteSlot(int slotId) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var slotModel = await _slotService.RemoveSlotAsync(slotId);
            if (slotModel == null)
            {
                return NotFound("Slot does not exist");
            }
            return Ok(slotModel);
        }
    }
}
