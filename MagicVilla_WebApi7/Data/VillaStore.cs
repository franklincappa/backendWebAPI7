using MagicVilla_WebApi7.Model.DTO;

namespace MagicVilla_WebApi7.Data
{
    public class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto {Id=1, Nombre="Vista a la Piscina"},
            new VillaDto {Id=2, Nombre="Vista a la Playa"}
        };
    }
}
