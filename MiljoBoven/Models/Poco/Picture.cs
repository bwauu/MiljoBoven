using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiljoBoven.Models
{
    public class Picture
    {
        public int PictureId { get; set; } // <= Är det numret som filen får (automatiskt av EF). 
        public String PictureName { get; set; } // <= PictureName är namnet på filen 
        public int ErrandId { get; set; } // <= ErrandId är ärende denna fil är kopplad till
    }
}
