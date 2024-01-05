using MagicVilla_WebApi7.Data;
using MagicVilla_WebApi7.Model;
using MagicVilla_WebApi7.Repositorio.IRepositorio;

namespace MagicVilla_WebApi7.Repositorio
{
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla> , INumeroVillaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public NumeroVillaRepositorio(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }
        
        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroVilla.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
