﻿using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Models.Dto
{
    public class VillaUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
