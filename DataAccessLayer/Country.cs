using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Country
    {
        [Key]
        public int cid { get; set; }
        public string cname { get; set; }
    }
}
