using MagicVilla_WebApi7.Data;
using MagicVilla_WebApi7.Model;

namespace MagicVilla_WebApi7.Repositorio.IRepositorio
{
    public interface IVillaRepositorio : IRepositorio<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
    }
}
