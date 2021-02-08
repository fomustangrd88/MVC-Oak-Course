using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MetaDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Meta Content is required.")]
        public string MetaContent { get; set; }
    }
}
