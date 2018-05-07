using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Medicine.ViewModels
{
    public class MedicationsVM
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int AlertThreshold { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }


        //string alertThreshold;
        //public string AlertThreshold
        //{
        //    get
        //    {
        //        return alertThreshold;
        //    }
        //    set
        //    {
        //        alertThreshold = value;
        //    }
        //}





    }
}