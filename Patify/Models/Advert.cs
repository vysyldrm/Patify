using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Patify.Models
{
    public class Advert
    {
        public int AdvertId { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Description { get; set; }

        public string AnimalName { get; set; }

        public int AnimalAge { get; set; }

        [Required]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string AnimalRace { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public SizeOfAnimal? SizeOfAnimal { get; set; }
            
        public List<Photos> AnimalPhotos { get; set; }

    }

    public enum Gender
    {
        Disi,
        Erkek,
        Bilinmiyor
    }
    public enum SizeOfAnimal
    {
        Kucuk,
        Orta,
        Buyuk
    }
}
