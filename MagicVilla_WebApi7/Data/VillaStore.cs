using MagicVilla_WebApi7.Model.DTO;

namespace MagicVilla_WebApi7.Data
{
    public class VillaStore
    {
        public static List<VillaUpdateDto> villaList = new List<VillaUpdateDto>
        {
            new VillaUpdateDto {Id=1, Nombre="Vista a la Piscina", Ocupantes=3, MetrosCuadrados=50},
            new VillaUpdateDto {Id=2, Nombre="Vista a la Playa", Ocupantes=4, MetrosCuadrados=63}
        };
    }
}
