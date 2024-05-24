using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts;
using ServiceContracts.DTO.SlotDTO;

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
        public IActionResult GetSlots() 
        { 
            var slots = _slotService.GetSlots();
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return Ok(slots);
        }

        [HttpGet("{slotId}")]
        public IActionResult GetSlotById(int slotId) 
        { 
            var slot = _slotService.GetSlotById(slotId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(slot);
        }

        [HttpPost]
        public IActionResult AddSlot([FromBody] SlotAddRequest slotAddRequest)
        { 
            if (slotAddRequest == null) { return BadRequest(ModelState); }
            if (SlotValidation.IsOverlapping(slotAddRequest.ToSlot(), _slotService.GetSlots()))
            { return BadRequest("Slot time cannot overlap with other slots"); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            _slotService.AddSlot(slotAddRequest);
            return Ok("successfully created");
        }

        [HttpPut("{slotId}")]
        public IActionResult UpdateSlot(int slotId, [FromBody] SlotUpdateRequest slotUpdateRequest)
        { 
            if (slotUpdateRequest == null) { return BadRequest(ModelState); }
            if (SlotValidation.IsOverlapping(slotUpdateRequest.ToSlot(), _slotService.GetSlots()))
            { return BadRequest("Slot time cannot overlap with other slots"); }
            if (_slotService.GetSlotById(slotId) == null) { return NotFound(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (!_slotService.UpdateSlot(slotId, slotUpdateRequest))
            {
                ModelState.AddModelError("", "Something went wrong updating slot");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully updated");
        }

        [HttpDelete("{slotId}")]
        public IActionResult DeleteSlot(int slotId) 
        {
            if (_slotService.GetSlotById(slotId) == null) { return NotFound(); }
            if (!_slotService.RemoveSlot(slotId))
            {
                ModelState.AddModelError("", "Something went wrong deleting slot");
            }
            return Ok("successfully deleted");
        }
    }
}
