using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Description
    {    
        [Key]
        public int desc { get; set; }   
        public string ReturnUrl { get; set; }
        
    }
}