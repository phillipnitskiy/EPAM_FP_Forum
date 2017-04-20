using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Input
{
    public class UserProfileInputModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public byte[] ImageData { get; set; }

        [ScaffoldColumn(false)]
        public string ImageMimeType { get; set; }

    }
}