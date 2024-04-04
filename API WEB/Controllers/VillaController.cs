using API_WEB.Dato;
using API_WEB.Modelos;
using API_WEB.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult< IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(Store.villaList);
        }

        [HttpGet("id:int", Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }

            var vill = Store.villaList.FirstOrDefault(v => v.Id == id);

            if (vill ==null)
            {
                return NotFound();              
            }
            return Ok(vill);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> insertVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if (villaDto.Id>0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = Store.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1; 
            Store.villaList.Add(villaDto);

            return CreatedAtRoute("GetVilla", new {id=villaDto.Id}, villaDto);
        
        }


    }
}
