using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebApi7.Model.DTO
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string Nombre { get; set; }
    }
}
