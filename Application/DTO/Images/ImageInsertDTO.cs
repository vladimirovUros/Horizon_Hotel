using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Images
{
    public class ImageInsertDTO
    {
        public IFormFile File { get; set; }
    }
}
