using MagicVilla_WebApi7.Data;
using MagicVilla_WebApi7.Model;
using MagicVilla_WebApi7.Repositorio.IRepositorio;

namespace MagicVilla_WebApi7.Repositorio
{
    public class VillaRepositorio : Repositorio<Villa> , IVillaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public VillaRepositorio(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }
        
        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
