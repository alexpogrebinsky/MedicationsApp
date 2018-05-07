using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace Medicine.ViewModels
{
    public class PrintView
    {
        [Display(Name = "Medication")]
        public string Description { get; set; }
        [Display(Name = "Alert Threshold")]
        public int AlertThreshold { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Date Created")]
        public DateTime CreatedByDate { get; set; }
        [Display(Name = "Created by")]
        public string CreatedByEmail { get; set; }
        [Display(Name = "Modified date")]
        public DateTime ModifiedByDate { get; set; }
        [Display(Name = "Modified by")]
        public string ModifiedbyUser { get; set; }
    }

    
}