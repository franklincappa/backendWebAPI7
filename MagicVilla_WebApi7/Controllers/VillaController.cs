using AutoMapper;
using MagicVilla_WebApi7.Data;
using MagicVilla_WebApi7.Model;
using MagicVilla_WebApi7.Model.DTO;
using MagicVilla_WebApi7.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_WebApi7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly IVillaRepositorio _villaRepositorio;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepositorio, IMapper mapper)
        {
            _logger = logger;
            //_db = db;
            _villaRepositorio = villaRepositorio;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas ()
        {
            _logger.LogInformation("Obtener las villas");
            IEnumerable<Villa> villaLista = await _villaRepositorio.ObtenerTodos();
            var lista= _mapper.Map<IEnumerable<VillaDto>>(villaLista);
            //return VillaStore.villaList; 
            return  Ok(lista);
        }

        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0) {
                _logger.LogError("Error al traer Villa con Id 0");
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa= await _villaRepositorio.Obtener(v => v.Id == id);
            if (villa == null) {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa)); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaCreateDto>> CrearVilla([FromBody] VillaCreateDto villaCreateDto) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            //if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null) 
            if (await _villaRepositorio.Obtener(v => v.Nombre.ToLower() == villaCreateDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (villaCreateDto is null) 
            {
                return BadRequest(villaCreateDto);
            }

            Villa modelo = _mapper.Map<Villa>(villaCreateDto);
            /*
            Villa modelo = new()
            {
                Nombre = villaCreateDto.Nombre,
                Detalle = villaCreateDto.Detalle,
                ImagenUrl = villaCreateDto.ImagenUrl,
                Ocupantes = villaCreateDto.Ocupantes,
                Tarifa = villaCreateDto.Tarifa,
                MetrosCuadrados = villaCreateDto.MetrosCuadrados,
                Amenidad = villaCreateDto.Amenidad
            };
            */

            await _villaRepositorio.Crear(modelo);
            await _villaRepositorio.Grabar();

            //villaDto.Id=VillaStore.villaList.OrderByDescending(v=>v.Id).FirstOrDefault().Id+1;
            //VillaStore.villaList.Add(villaDto);
            //return Ok(villaDto);
            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, villaCreateDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id) 
        {
            if (id == 0) 
            {
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = await _villaRepositorio.Obtener(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();            
            }
            //VillaStore.villaList.Remove(villa);
            await _villaRepositorio.Remover(villa);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto) 
        {
            if (villaUpdateDto == null || id != villaUpdateDto.Id)
            {
                return BadRequest();
            }
            //var villa=VillaStore.villaList.FirstOrDefault(v=>v.Id == id);
            //villa.Nombre = villaDto.Nombre;
            //villa.Ocupantes=villaDto.Ocupantes;
            //villa.MetrosCuadrados = villaDto.MetrosCuadrados;

            Villa modelo = _mapper.Map<Villa>(villaUpdateDto);
            /*
            Villa modelo = new () 
            {
                Id= villaUpdateDto.Id,
                Nombre = villaUpdateDto.Nombre,
                Detalle= villaUpdateDto.Detalle,
                ImagenUrl= villaUpdateDto.ImagenUrl,
                Ocupantes= villaUpdateDto.Ocupantes,
                Tarifa= villaUpdateDto.Tarifa,
                MetrosCuadrados= villaUpdateDto.MetrosCuadrados,
                Amenidad= villaUpdateDto.Amenidad
            };
            */

            await _villaRepositorio.Actualizar(modelo);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa =await _villaRepositorio.Obtener(v => v.Id == id, tracked: false);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            /*
            VillaUpdateDto villaDto = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };
            */

            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);
            /*
            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };
            */
            //_db.Villas.Update(modelo);
            //await _db.SaveChangesAsync();

            await _villaRepositorio.Actualizar(modelo);
            return NoContent();
        }

    }
}
