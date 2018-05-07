using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UserTransaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime CreatedByDate { get; set; }
       // [Required]
        public virtual AppUser CreatedById { get; set; }
        [Required]
        public virtual Medication Med { get; set; }
    }

    public class Params
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
    }
}
