using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{

    //Najim Ansari...
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [DisplayName("Coutry Name")]
        public string countryName { get; set; }
        [NotMapped]
        public IFormFile image { get; set; } 
        
        public string imageFileName { get; set; }
    }
}
