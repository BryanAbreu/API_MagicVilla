using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Datos;
using Microsoft.AspNetCore.JsonPatch;
using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MagicVilla_API.Repository.IRepository;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumVillaController : ControllerBase
    {
        private readonly ILogger<NumVillaController> _logger;
        private readonly IVillaRepository _villaRepository;
        private readonly INumVillaRepository _numvillaRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumVillaController(ILogger<NumVillaController> logger, IVillaRepository villaRepository, INumVillaRepository numvillaRepository, IMapper mapper)
        {
            _logger = logger;
            _villaRepository = villaRepository;
            _numvillaRepository= numvillaRepository;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult< APIResponse>> GetNumvillas()
        {
            try
            {
                _logger.LogInformation("get num villa");

                IEnumerable<NumVilla> numvillaList = await _numvillaRepository.GetAll();

                _response.Result = _mapper.Map<IEnumerable<NumVillaDTO>>(numvillaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.ToString() };
               
            }
            return _response;
           
        }


        [HttpGet("id:int",Name = "GetNumVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> GetNumVilla(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error get num villa with id " + id);
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var numvilla = await _numvillaRepository.Get(v => v.VillaNo  == id);
                if (numvilla == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<NumVilla>(numvilla);
                _response.StatusCode=HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.ToString() };

            }
            return _response;


        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> CrearNumVilla([FromBody]NumVillaCreateDTO createvillaDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numvillaRepository.Get(v => v.VillaNo == createvillaDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("NameExist", "That Number of village already exist!");
                    return BadRequest(ModelState);
                }

                if (await _villaRepository.Get(v => v.Id == createvillaDTO.Villaid) == null)
                {
                    ModelState.AddModelError("ForeingKey", "That Id of village no exist!");
                    return BadRequest(ModelState);
                }

                if (createvillaDTO == null)
                {
                    return BadRequest(createvillaDTO);
                }

                NumVilla model = _mapper.Map<NumVilla>(createvillaDTO);

                model.DateCreate= DateTime.Now;
                model.DateUpdate= DateTime.Now;
                await _numvillaRepository.Create(model);
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumVilla", new { id = model.VillaNo }, model);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumVilla(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var vill = await _numvillaRepository.Get(V => V.VillaNo == id);
                if (vill == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _numvillaRepository.Remove(vill);

                _response.StatusCode=HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Errors = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumVilla(int id, [FromBody] NumVillaUpdateDTO UpdatevillaDTO)
        {
            if (UpdatevillaDTO == null || id!= UpdatevillaDTO.VillaNo)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepository.Get(v => v.Id == UpdatevillaDTO.Villaid) == null)
            {
                ModelState.AddModelError("ForeingKey", "That Id of village no exist!");
                return BadRequest(ModelState);

            }

            NumVilla model = _mapper.Map<NumVilla>(UpdatevillaDTO);

            
            await _numvillaRepository.Update(model);
            _response.StatusCode = HttpStatusCode.NoContent;
         

            return Ok(_response);    
        }

       

    }
}
