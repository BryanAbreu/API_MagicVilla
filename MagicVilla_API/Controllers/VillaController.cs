using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Datos;
using Microsoft.AspNetCore.JsonPatch;
using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _context;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public ActionResult< IEnumerable<VillaDTO>> Getvillas()
        {
            _logger.LogInformation("get villa");
            return Ok(_context.Villa.ToList());
        }


        [HttpGet("id:int",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult< VillaDTO> GetVilla(int id) 
        {

            if (id == 0)
            {
                _logger.LogError("Error get villa with id " + id);
                return BadRequest();
            }
            // var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
            var villa = _context.Villa.FirstOrDefault(v => v.Id == id);
            if (villa == null) 
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CrearVilla([FromBody]VillaDTO villaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Villa.FirstOrDefault(v => v.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "That Name of village already exist!");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            
            }
            //villaDTO.Id = VillaStore.VillaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //VillaStore.VillaList.Add(villaDTO);
            Villa model = new()
            {
                Name=villaDTO.Name,
                Detail=villaDTO.Detail,
                ImageURL=villaDTO.ImageURL,
                Occupants=villaDTO.Occupants,
                Rate=villaDTO.Rate,
                squaremeter=villaDTO.squaremeter,
                amenidad=villaDTO.amenidad
            };
            _context.Villa.Add(model);
            _context.SaveChanges();

            return CreatedAtRoute("GetVilla", new {id= villaDTO.Id },villaDTO);
        
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id) 
        {
            if(id==0)
            {
                return BadRequest();
            }
            var vill = _context.Villa.FirstOrDefault(V => V.Id == id);
            if (vill == null)
            {
                return NotFound();
            }
            //VillaStore.VillaList.Remove(vill);
            _context.Villa.Remove(vill);
            _context.SaveChanges();

            return NoContent();
        
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id!= villaDTO.Id)
            { 
                return BadRequest();
            }
            //var vill = _context.Villa.FirstOrDefault(V => V.Id == id);
            //vill.Name = villaDTO.Name;
            //vill.Occupants = villaDTO.Occupants;
            //vill.squaremeter= villaDTO.squaremeter;
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Detail = villaDTO.Detail,
                ImageURL = villaDTO.ImageURL,
                Occupants = villaDTO.Occupants,
                Rate = villaDTO.Rate,
                squaremeter = villaDTO.squaremeter,
                amenidad = villaDTO.amenidad

            };
            _context.Villa.Update(model);
            _context.SaveChanges();

            return NoContent();    
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchdto)
        {
            if (patchdto == null || id ==0)
            {
                return BadRequest();
            }
            var vill = _context.Villa.AsNoTracking().FirstOrDefault(V => V.Id == id);

            VillaDTO villaDTO = new()
            {
                Id=vill.Id,
                Name = vill.Name,
                Detail = vill.Detail,
                ImageURL = vill.ImageURL,
                Occupants = vill.Occupants,
                Rate = vill.Rate,
                squaremeter = vill.squaremeter,
                amenidad = vill.amenidad
            };

            if (vill == null) return BadRequest(); 

            patchdto.ApplyTo(villaDTO, ModelState);

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            Villa model = new()
            {

                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Detail = villaDTO.Detail,
                ImageURL = villaDTO.ImageURL,
                Occupants = villaDTO.Occupants,
                Rate = villaDTO.Rate,
                squaremeter = villaDTO.squaremeter,
                amenidad = villaDTO.amenidad
            };
            _context.Villa.Update(model);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
