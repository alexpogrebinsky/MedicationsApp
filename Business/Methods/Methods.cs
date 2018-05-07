using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Business.Methods
{
    public class Methods
    {
       public IEnumerable<UserTransaction> GetTransactions()
        {
                MedicineDB db = new MedicineDB();
           
                IEnumerable<UserTransaction> Trans = from tr in db.UserTransactions
                                                     select tr;
                return Trans;

        }

        public IEnumerable<Medication> GetMedications()
        {
            MedicineDB db = new MedicineDB();

            IEnumerable<Medication> meds = from m in db.Medications
                                           select m;
            return meds;

        }

        public int[] GetQuantityAvailable()
        {
            MedicineDB db = new MedicineDB();

            IEnumerable<Medication> meds = GetMedications();

            int[] qtys = new int[1000];

            foreach (Medication md in meds)
            {
                IEnumerable<int> qty = from tr in db.UserTransactions
                                       where tr.Med.ID == md.ID
                                       select tr.Quantity;

                if (qty.Count() == 0)
                    qtys[md.ID] = 0;
                else
                    qtys[md.ID] = qty.Sum();
            }

            return qtys;
        }
    }
}
