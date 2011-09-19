using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using CTraderEmployees.Models;

namespace CTraderEmployees.Controllers
{
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
        internal DataStore _dataStore;
        public EmployeeController()
        {
            _dataStore = new DataStore();
            _dataStore.CreateDataStore(HostingEnvironment.ApplicationPhysicalPath + Properties.Settings.Default["store"].ToString());
        }
        public EmployeeController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public ActionResult Details(Guid id)
        {
            var employeeRecord = _dataStore.GetRecordById(id);
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

        public ActionResult Index(ListSearchFilters? filterList)
        {
            List<EmployeeModel> employeeModels = _dataStore.GetAllRecords();
            if (filterList.HasValue && filterList != ListSearchFilters.All)
            {
                var removeStatus = (filterList.Value == ListSearchFilters.No);
                employeeModels.RemoveAll(employee => employee.IsCurrentEmployee == removeStatus);
            }
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
                    _dataStore.SaveRecord(collection);
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
            var employeeRecord = _dataStore.GetRecordById(id);

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
                    _dataStore.SaveRecord(collection);
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
            var employeeRecord = _dataStore.GetRecordById(id);

            return View(employeeRecord);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public ActionResult Delete(Guid id, EmployeeModel collection)
        {
            try
            {
                _dataStore.RemoveRecordById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Terminate(Guid id)
        {
            var employeeModel = _dataStore.GetRecordById(id);
            employeeModel.IsCurrentEmployee = false;
            _dataStore.SaveRecord(employeeModel);

            return RedirectToAction("Index");
        }
    }
}
