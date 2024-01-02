using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebApi7.Model.DTO
{
    public class VillaDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }
        [Required]
        public string ImagenUrl { get; set; }
        public string Amenidad { get; set; }
    }
}
