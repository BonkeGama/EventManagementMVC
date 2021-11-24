using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OlwandleHotel.Models;

namespace OlwandleHotel.Models
{   //                             Tbl_ShippingDetails
    public class BookEvent
    {
        [Key]
        public int ShippingDetailId { get; set; }

        public Nullable<int> MemberId { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string PaymentType { get; set; }

        public virtual Event Event { get; set; }

    }

    }


