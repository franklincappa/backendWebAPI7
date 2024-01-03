using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebApi7.Model.DTO
{
    public class NumeroVillaCreateDto
    {
        [Required]
        public int VillaNro { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; }
    }
}
