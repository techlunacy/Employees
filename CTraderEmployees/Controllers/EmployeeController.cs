using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using CTraderEmployees.Models;
using CTraderEmployees.Properties;

namespace CTraderEmployees.Controllers
{
    [HandleError]
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        //public ActionResult Index()
        //{
        //    var dataStore = new DataStore();
        //    dataStore.CreateDataStore(Server.MapPath(Properties.Settings.Default["store"].ToString()));
        //    return View(dataStore.GetAllRecords());
        //}

        //
        // GET: /Employee/Details/5
        internal string Path = Settings.Default["store"].ToString();
        public EmployeeController()
        {

        }
        public EmployeeController(string path)
        {
            Path = path;
        }

        public ActionResult Details(Guid id)
        {
            var employeeRecord = EmployeeModel.LoadById(id, Path);
            return View(employeeRecord);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Employee/Create

        public ActionResult Index(EmploymentStatusFilter? filterList)
        {
            var employeeModels = EmployeeModel.FindByEmploymentStatusFilter(filterList, Path);
            return View(employeeModels);
        }


        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(EmployeeModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collection.Save(Path);
                    return RedirectToAction("Details", new { id = collection.Id });
                }
                else
                {
                    return View();

                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(Guid id)
        {
            var employeeRecord = EmployeeModel.LoadById(id, Path);

            return View(employeeRecord);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Guid id, EmployeeModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collection.Id = id;
                    collection.Save(Path);
                    return RedirectToAction("Details", new { id = collection.Id });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(Guid id)
        {
            var employeeRecord = EmployeeModel.LoadById(id, Path);

            return View(employeeRecord);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, EmployeeModel collection)
        {
            try
            {
                EmployeeModel.DeleteById(id, Path);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Terminate(Guid id)
        {
            var employeeModel = EmployeeModel.LoadById(id, Path);
            employeeModel.IsCurrentEmployee = false;
            employeeModel.Save(Path);

            return RedirectToAction("Index");
        }
    }
}
