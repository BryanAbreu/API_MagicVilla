using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Datos;
using Microsoft.AspNetCore.JsonPatch;
using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext context,IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task< ActionResult< IEnumerable<VillaDTO>>> Getvillas()
        {
            _logger.LogInformation("get villa");
            IEnumerable<Villa> villaList = await _context.Villa.ToListAsync();

            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villaList));
        }


        [HttpGet("id:int",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VillaDTO>> GetVilla(int id) 
        {

            if (id == 0)    
            {
                _logger.LogError("Error get villa with id " + id);
                return BadRequest();
            }
            var villa = await _context.Villa.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null) 
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<VillaDTO>> CrearVilla([FromBody]VillaCreateDTO createvillaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Villa.FirstOrDefaultAsync(v => v.Name.ToLower() == createvillaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "That Name of village already exist!");
                return BadRequest(ModelState);
            }
            if (createvillaDTO == null)
            {
                return BadRequest(createvillaDTO);
            }

            Villa model= _mapper.Map<Villa>(createvillaDTO);
           
            await _context.Villa.AddAsync(model);
           await _context.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new {id= model.Id },model);
        
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id) 
        {
            if(id==0)
            {
                return BadRequest();
            }
            var vill = await _context.Villa.FirstOrDefaultAsync(V => V.Id == id);
            if (vill == null)
            {
                return NotFound();
            }
            _context.Villa.Remove(vill);
            await _context.SaveChangesAsync();

            return NoContent();
        
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO UpdatevillaDTO)
        {
            if (UpdatevillaDTO == null || id!= UpdatevillaDTO.Id)
            { 
                return BadRequest();
            }
            
            Villa model = _mapper.Map<Villa>(UpdatevillaDTO);

            
            _context.Villa.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();    
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchdto)
        {
            if (patchdto == null || id ==0)
            {
                return BadRequest();
            }
            var vill = await _context.Villa.AsNoTracking().FirstOrDefaultAsync(V => V.Id == id);

            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(vill);


            if (vill == null) return BadRequest(); 

            patchdto.ApplyTo(villaDTO, ModelState);

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            Villa model = _mapper.Map<Villa>(villaDTO);
           
            _context.Villa.Update(model);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
