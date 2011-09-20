using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CTraderEmployees.Controllers;
using CTraderEmployees.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CTraderEmployeesTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class EmployeeTests
    {
        public EmployeeTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private EmployeeModel _currentEmployeeModel;
        private EmployeeModel _secondEmployeeModel;
        private Guid _identifier;
        private DataStore _dataStore;
        private const string InvalidRecord = "21d9fb81-bdf8-4c4e-b397-01018f30e90b|john|smith|Male|True";
        protected const string ExpectedFormattedRecordSetFirstRow = "21d9fb81-bdf8-4c4e-b397-01018f30e90b|john|smith|Male|5|True";
        protected const string ExpectedFormattedRecordSetSecondRow = "31d9fb81-bdf8-4c4e-b397-01018f30e90b|pete|John|Male|1|False";
        private const string Delimiter = "|";

        [TestInitialize]
        public void CreateEmployee()
        {
            _identifier = Guid.Parse("21d9fb81-bdf8-4c4e-b397-01018f30e90b");
            _currentEmployeeModel = new EmployeeModel { Id = _identifier, Age = 5, FirstName = "john", LastName = "smith", IsCurrentEmployee = true };
            var secondEmployeeId = Guid.Parse("31d9fb81-bdf8-4c4e-b397-01018f30e90b");
            _secondEmployeeModel = new EmployeeModel { Id = secondEmployeeId, Age = 1, FirstName = "pete", LastName = "John", Gender = EmployeeGender.Male, IsCurrentEmployee = false };
            _dataStore = new DataStore();
            _dataStore.CreateDataStore("test.data");
        }

        [TestMethod]
        public void EmployeeExists()
        {
            Assert.IsNotNull(_currentEmployeeModel);

        }
        [TestMethod]
        public void EmployeeHasFields()
        {
            Assert.AreEqual(_identifier, _currentEmployeeModel.Id);
            Assert.AreEqual("john", _currentEmployeeModel.FirstName);
            Assert.AreEqual("smith", _currentEmployeeModel.LastName);
            Assert.AreEqual(5, _currentEmployeeModel.Age);
            Assert.IsTrue(_currentEmployeeModel.IsCurrentEmployee);
            Assert.AreEqual(EmployeeGender.Male, _currentEmployeeModel.Gender);
        }
        [TestMethod]
        public void SaveEmployee()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            var records = File.ReadLines(_dataStore.Path);
            Assert.AreEqual(ExpectedFormattedRecordSetFirstRow, records.First());
        }

        [TestMethod]
        public void RecordExists()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            Assert.IsTrue(_dataStore.IdExists(_currentEmployeeModel.Id));
        }
        [TestMethod]
        public void RemoveRecord()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            Assert.IsTrue(_dataStore.IdExists(_currentEmployeeModel.Id));
            _dataStore.RemoveRecordById(_currentEmployeeModel.Id);
            Assert.IsFalse(_dataStore.IdExists(_currentEmployeeModel.Id));
        }

        [TestMethod]
        public void UpdateEmployee()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            _currentEmployeeModel.FirstName = "James";
            _dataStore.SaveRecord(_currentEmployeeModel);
            var records = _dataStore.ReadRecords();
            Assert.AreNotEqual(ExpectedFormattedRecordSetFirstRow, records.ElementAt(0));
        }
        [TestMethod]
        public void HaveMultipleRecords()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            _dataStore.SaveRecord(_secondEmployeeModel);
            Assert.IsTrue(_dataStore.IdExists(_currentEmployeeModel.Id));
            Assert.IsTrue(_dataStore.IdExists(_secondEmployeeModel.Id));
        }
        [TestMethod]
        public void ReturnEmployee()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            EmployeeModel employeeModel = _dataStore.GetRecordById(_currentEmployeeModel.Id);
            Assert.AreEqual(employeeModel.Id, _currentEmployeeModel.Id);
            Assert.AreEqual(employeeModel.FirstName, _currentEmployeeModel.FirstName);
            Assert.AreEqual(employeeModel.LastName, _currentEmployeeModel.LastName);
            Assert.AreEqual(employeeModel.Age, _currentEmployeeModel.Age);
            Assert.AreEqual(employeeModel.IsCurrentEmployee, _currentEmployeeModel.IsCurrentEmployee);
            Assert.AreEqual(employeeModel.Gender, _currentEmployeeModel.Gender);
        }
        [TestMethod]
        public void ParseEmployee()
        {
            EmployeeModel employeeModel = EmployeeModel.Parse(Delimiter, _currentEmployeeModel.FormatRecord(Delimiter));
            Assert.AreEqual(employeeModel.Id, _currentEmployeeModel.Id);
            Assert.AreEqual(employeeModel.FirstName, _currentEmployeeModel.FirstName);
            Assert.AreEqual(employeeModel.LastName, _currentEmployeeModel.LastName);
            Assert.AreEqual(employeeModel.Age, _currentEmployeeModel.Age);
            Assert.AreEqual(employeeModel.IsCurrentEmployee, _currentEmployeeModel.IsCurrentEmployee);
            Assert.AreEqual(employeeModel.Gender, _currentEmployeeModel.Gender);
        }
        [TestMethod]
        public void GetAllEmployees()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            _dataStore.SaveRecord(_secondEmployeeModel);
            var employeeModels = _dataStore.GetAllRecords();
            Assert.AreEqual(2, employeeModels.Count);
        }
        [TestMethod]
        public void TerminateEmployee()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            var employeeController = new EmployeeController(_dataStore);
            var action = employeeController.Terminate(_currentEmployeeModel.Id);

            Assert.IsNotNull(action);
            var localModel = _dataStore.GetRecordById(_currentEmployeeModel.Id);
            Assert.IsFalse(localModel.IsCurrentEmployee);
        }

        [TestMethod]
        public void EmployeeDetails()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            var employeeController = new EmployeeController(_dataStore);
            var action = employeeController.Details(_currentEmployeeModel.Id)as ViewResult;

            Assert.IsNotNull(action);
            var localModel = (EmployeeModel)action.ViewData.Model;
            Assert.AreEqual(localModel.Id, _currentEmployeeModel.Id);
            Assert.AreEqual(localModel.FirstName, _currentEmployeeModel.FirstName);
            Assert.AreEqual(localModel.LastName, _currentEmployeeModel.LastName);
            Assert.AreEqual(localModel.Age, _currentEmployeeModel.Age);
            Assert.AreEqual(localModel.IsCurrentEmployee, _currentEmployeeModel.IsCurrentEmployee);
            Assert.AreEqual(localModel.Gender, _currentEmployeeModel.Gender);
        }


        [TestMethod]
        public void EmployeeHasId()
        {
            var employee = new EmployeeModel();
            Assert.IsNotNull(employee.Id);

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ErrorParsing()
        {
            EmployeeModel.Parse(Delimiter, InvalidRecord);
        }

        [TestCleanup]
        public void RemoveDatastore()
        {
            _dataStore.RemoveDataStore();
        }
    }
}
