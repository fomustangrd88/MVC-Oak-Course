using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DTO
{
    public class AddressDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter an address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter an email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a phone number.")]
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        [Required(ErrorMessage = "Please enter a fax number.")]
        public string Fax { get; set; }
        [Required(ErrorMessage = "Please enter a large map path.")]
        [AllowHtml]
        public string MapPathLarge { get; set; }
        [Required(ErrorMessage = "Please enter a small map path.")]
        [AllowHtml]
        public string MapPathSmall { get; set; }
    }
}
