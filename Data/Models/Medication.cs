using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Medication
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Medication")]
        public string Description { get; set; }
        [Display(Name = "Alert Threshold")]
        public int AlertThreshold { get; set; }
        [Display(Name ="Deactivated")]
        public bool Delete { get; set; } = false;
        //public string Email { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Vendor { get; set; }
        public string Notes { get; set; }
         


        //[ForeignKey("Id")]
        [Required]
        public virtual AppUser AppUser { get; set; }
        public virtual AppUser ModifiedUser { get; set; }
        
        //[ForeignKey("Email")]
        //[Required]
        //public virtual AppUser Email { get; set; }
        ////public virtual AppUser User { get; set; }
    }
}
