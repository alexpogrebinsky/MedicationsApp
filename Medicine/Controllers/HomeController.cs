using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Models;
using Business.Methods;
using System.Data;
using System.Net;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;
using Medicine.ViewModels;




namespace Medicine.Controllers
{
    public class HomeController : Controller
    {

        private MedicineDB db = new MedicineDB();


        //public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.DescriptionOrder = String.IsNullOrEmpty(sortOrder) ? "desc_desc" : "";
        //    ViewBag.AlertThresholdOrder = sortOrder == "AlertThreshold" ? "alert_desc" : "AlertThreshold";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var medicine = from s in db.Medications
        //                   select s;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        medicine = medicine.Where(s => s.Description.Contains(searchString)
        //                             /*  || s.AlertThreshold.Contains(searchString))*/);
        //    }
        //    switch (sortOrder)
        //    {
        //        case "desc_desc":
        //            medicine = medicine.OrderByDescending(s => s.Description);
        //            break;
        //        //case "D":
        //        //    students = students.OrderBy(s => s.EnrollmentDate);
        //        //    break;
        //        case "alert_desc":
        //            medicine = medicine.OrderByDescending(s => s.AlertThreshold);
        //            break;
        //        default:  // Name ascending 
        //            medicine = medicine.OrderBy(s => s.Description);
        //            break;
        //    }

        //    //int pageSize = 3;
        //    int pageNumber = (page ?? 1);
        //    return View(medicine);
        //}


       
        public ActionResult CreateMedicine()
        {

            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            ///* List<Medication> MedicineList = new List<Medication>();
            // Medication med1 = new Medication { ID = 1, Description = "description one" };
            // Medication med2 = new Medication { ID = 2, Description = "description two" };
            // MedicineList.Add(med1);
            // MedicineList.Add(med2);
            // */

            //var meth = new Methods();
            //IEnumerable<UserTransaction> trans = meth.GetTransactions();

            return View(/*trans*/);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";

            return View();
        }



        // GET: Student/Create
        public ActionResult AddMeds()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMeds([Bind(Include = "Description, AlertThreshold, Vendor, Notes")]Medication meds)
        {
            ModelState["AppUser"].Errors.Clear();  //Required since ApplicationUser is NOT populated by the user in the view.

            try
            {
                if (ModelState.IsValid)
                {
                    //var userRec = db.Users.Find(User.Identity.GetUserId());
                    //var EmailAddress = userRec.Email;
                    //var x = userRec.Name;

                    meds.AppUser = db.Users.Find(User.Identity.GetUserId());  //Requires "using Microsoft.AspNet.Identity;"
                    meds.CreatedDate = DateTime.Now;
                    meds.ModifiedDate = DateTime.Now;   //remove later
                    db.Medications.Add(meds);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(meds);
        }


        // GET: Student/Edit/5
        public ActionResult EditMeds(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medication medicinefind = db.Medications.Find(id);
            if (medicinefind == null)
            {
                return HttpNotFound();
            }
            return View(medicinefind);
        }


        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("EditMeds")]
        [ValidateAntiForgeryToken]
        public ActionResult EditMedsPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var medsToUpdate = db.Medications.Find(id);
            if (TryUpdateModel(medsToUpdate, "",
               new string[] { "Description", "AlertThreshold", "Delete", "ModifiedUser", "Notes", "ModiefiedDate" }))
            {
                try
                {
                    medsToUpdate.ModifiedUser = db.Users.Find(User.Identity.GetUserId());
                    medsToUpdate.ModifiedDate = DateTime.Now;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(medsToUpdate);
        }


        public ActionResult Print()
        {
            var fullList = new List<PrintView>();

            //Retrieve list records from DB
            var medsList = db.Medications.ToList();

            var meth = new Methods();
            int[] qtys = meth.GetQuantityAvailable();

            foreach (Medication med in medsList)
            {
                //populate/hydrate one rec
                PrintView pvm = new PrintView();

                pvm.Description = med.Description;
                pvm.AlertThreshold = med.AlertThreshold;
                pvm.Quantity = qtys[med.ID];
                pvm.ModifiedByDate = med.ModifiedDate;
                pvm.ModifiedbyUser = med.AppUser.Email;
                pvm.CreatedByDate = med.CreatedDate;
                pvm.CreatedByEmail = med.AppUser.Email;

                //Add one rec to the fullList
                fullList.Add(pvm);
            }

            return View(fullList);
        }

        public ActionResult MedInventory()
        {
            var meth = new Methods();
            IEnumerable<Medication> meds = meth.GetMedications();

            ViewBag.QtyAvail = meth.GetQuantityAvailable();

            return View(meds);
        }


        public void AddTrans(Params prms) 
        {
            MedicineDB db = new MedicineDB();

            UserTransaction UserTrans = new UserTransaction();


            IEnumerable<Medication> md = from m in db.Medications
                                         where m.ID == prms.ID
                                         select m;

            Medication med = md.ElementAt(0);


            UserTrans.Quantity = prms.Quantity;
            UserTrans.CreatedByDate = DateTime.Now;
            UserTrans.CreatedById = db.Users.Find(User.Identity.GetUserId());
            UserTrans.Med = med;


            db.UserTransactions.Add(UserTrans);
            db.SaveChanges();

        }

        public ActionResult Index()
        {
            var FrontFullList = new List<MedicationsVM>();

            //Retrieve list records from DB
            var frontList = db.Medications.ToList();

            var meth = new Methods();
            int[] qtys = meth.GetQuantityAvailable();

            foreach (Medication meds in frontList)
            {
                //populate/hydrate one rec
                MedicationsVM mvm = new MedicationsVM();

                mvm.Description = meds.Description;
                mvm.AlertThreshold = meds.AlertThreshold;
                mvm.Quantity = qtys[meds.ID];
                mvm.Notes = meds.Notes;
                mvm.ID = meds.ID;
                //mvm.CreatedByDate = med.CreatedDate;
                //mvm.CreatedByEmail = med.AppUser.Email;

                //Add one rec to the fullList
                FrontFullList.Add(mvm);
            }

            return View(FrontFullList);
        }


    }
}
