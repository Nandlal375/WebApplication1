using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Registration
    {
        [Key]
        public int Pid { get; set; }
        [Required(ErrorMessage = "Name Field is mandetory")]
        [DisplayName("Name")]
         
        public string Pname { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Pemail { get; set; }
        [Required]
        [DisplayName("Mobile")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Pmobile { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Paddress { get; set; }
        [Required]
        [DisplayName("Username")]
        public string Pusername { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Ppassword { get; set; }
    }
}
