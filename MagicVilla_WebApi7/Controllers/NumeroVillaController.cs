using AutoMapper;
using MagicVilla_WebApi7.Data;
using MagicVilla_WebApi7.Model;
using MagicVilla_WebApi7.Model.DTO;
using MagicVilla_WebApi7.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace MagicVilla_WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase
    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepositorio _villaRepositorio;
        private readonly INumeroVillaRepositorio _numerovillaRepositorio;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepositorio, INumeroVillaRepositorio numerovillaRepositorio,  IMapper mapper)
        {
            _logger = logger;
            _villaRepositorio = villaRepositorio;
            _numerovillaRepositorio=numerovillaRepositorio;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas ()
        {
            try
            {
                _logger.LogInformation("Obtener Números villas");
                IEnumerable<NumeroVilla> numerovillaLista = await _numerovillaRepositorio.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numerovillaLista);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}",Name ="GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer número Villa con Id 0");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numerovilla = await _numerovillaRepositorio.Obtener(v => v.VillaNro == id);
                if (numerovilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Resultado = _mapper.Map<NumeroVillaDto>(numerovilla);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };                
            }
            return _response;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto numerovillaCreateDto) 
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    
                    return BadRequest(ModelState);
                }

                //if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null) 
                if (await _numerovillaRepositorio.Obtener(v => v.VillaNro == numerovillaCreateDto.VillaNro) != null)
                {
                    ModelState.AddModelError("NombreExiste", "La número de villa ya existe!");
                    
                    return BadRequest(ModelState);
                }

                if (await _villaRepositorio.Obtener(v => v.Id == numerovillaCreateDto.VillaId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "El id de la Villa no existe!");
                    return BadRequest(ModelState);
                }

                if (numerovillaCreateDto is null)
                {
                    return BadRequest(numerovillaCreateDto);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(numerovillaCreateDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numerovillaRepositorio.Crear(modelo);
                await _numerovillaRepositorio.Grabar();

                //villaDto.Id=VillaStore.villaList.OrderByDescending(v=>v.Id).FirstOrDefault().Id+1;
                //VillaStore.villaList.Add(villaDto);
                //return Ok(villaDto);

                _response.Resultado = modelo; 
                _response.statusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = modelo.VillaNro }, _response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                
            }
            return _response;
            
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumeroVilla(int id) 
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numeroVilla = await _numerovillaRepositorio.Obtener(v => v.VillaNro == id);

                if (numeroVilla == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _numerovillaRepositorio.Remover(numeroVilla);

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto numeroVillaUpdateDto) 
        {
            try
            {
                if (numeroVillaUpdateDto == null || id != numeroVillaUpdateDto.VillaNro)
                {
                    return BadRequest();
                }

                if (await _villaRepositorio.Obtener(v => v.Id == numeroVillaUpdateDto.VillaId)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de la villa no Eistes!");
                    return BadRequest(ModelState);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(numeroVillaUpdateDto);

                await _numerovillaRepositorio.Actualizar(modelo);
                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsExitoso = false;
            }
            return Ok(_response);
        }
              

    }
}
