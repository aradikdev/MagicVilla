using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
