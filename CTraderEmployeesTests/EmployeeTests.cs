using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
        private Guid _identifier;
        private DataStore _dataStore;
        protected const string ExpectedFormattedRecordSet = "21d9fb81-bdf8-4c4e-b397-01018f30e90b|john|smith|Male|5|True";
        [TestInitialize]
        public void CreateEmployee()
        {
            _identifier = Guid.Parse("21d9fb81-bdf8-4c4e-b397-01018f30e90b");
            _currentEmployeeModel = new EmployeeModel { Id = _identifier, Age = 5, FirstName = "john", LastName = "smith", IsCurrentEmployee = true };
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
            Assert.AreEqual(ExpectedFormattedRecordSet, records.ElementAt(0));
        }
        [TestMethod]
        public void UpdateEmployee()
        {
            _dataStore.SaveRecord(_currentEmployeeModel);
            _currentEmployeeModel.FirstName = "James";
            _dataStore.SaveRecord(_currentEmployeeModel);
            IEnumerable<string> records = File.ReadLines(_dataStore.Path);
            Assert.AreNotEqual(ExpectedFormattedRecordSet, records.ElementAt(0));
        }
        [TestCleanup]
        public void RemoveDatastore()
        {
            _dataStore.RemoveDataStore();
        }
    }
}
