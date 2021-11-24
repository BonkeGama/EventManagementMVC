using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OlwandleHotel.Models
{    //                            Tbl_CartStatus
    public class AddEvents
    {
        [Key]
        public int CartStatusId { get; set; }
        public string CartStatus { get; set; }

    }
}