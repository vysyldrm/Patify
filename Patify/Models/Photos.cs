using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Patify.Models
{
    public class Photos
    {
        public int PhotosId { get; set; }
        public int AdvertId { get; set; }
        [ForeignKey("AdvertId")]
        public Advert Advert { get; set; }
    }
}
