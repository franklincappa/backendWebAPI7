using MagicVilla_WebApi7.Data;
using MagicVilla_WebApi7.Model;

namespace MagicVilla_WebApi7.Repositorio.IRepositorio
{
    public interface INumeroVillaRepositorio : IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
    }
}
