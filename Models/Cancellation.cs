using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OlwandleHotel.Models
{
    public class Cancellation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CancelID { get; set; }         
        [Display(Name = "Start Date")]        
        public DateTime checkInDate { get; set; }        
        [Display(Name = "End Date")]        
        public DateTime checkOutDate { get; set; }       
        [DisplayName("No of Days")]
        [Range(1, 6, ErrorMessage = "Please Hire another tent because this tent cannot be hired for more than 6 Days")]
        public int numGuests { get; set; }
        public string Email { get; set; }

        [DisplayName("Tent No.")]
        public string RoomNo { get; set; }
        [DisplayName("Date Hired")]
        public DateTime DateBooked { get; set; }

        [Display(Name = "Start In?")]
        public bool checkedIn { get; set; }

        [Display(Name = "End?")]
        public bool checkedOut { get; set; }
        public Nullable<DateTime> date_CheckedOut { get; set; }      
        [DisplayName("No Of Nights")]
        public int DayNo { get; set; }

        [DisplayName("Number of Hirings")]
        public int bookings { get; set; } = 0;
       
        [DisplayName("Total Price")]
        public double TotalPrice { get; set; }
        public double Discount { get; set; }
        public string Status { get; set; }
    }
}