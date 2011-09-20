using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CTraderEmployees.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CTraderEmployeesTests
{
    [TestClass]
    public class DataStoreTests
    {
        private DataStore _validDataStore;
        internal string DataStorePath = Directory.GetCurrentDirectory();
        internal string DataStoreFileName = "employee.data";

        internal string ExistingFile = @"test.data";
        [TestInitialize]
        public void CreateConfig()
        {
            FileStream stream = File.Create(ExistingFile);
            stream.Close();
            _validDataStore = new DataStore { Path = ExistingFile };
            Directory.CreateDirectory(("Content"));
        }

        [TestMethod]
        public void ConfigExists()
        {
            Assert.IsNotNull(_validDataStore);
        }
        [TestMethod]
        public void ConfigHasPath()
        {
            Assert.AreEqual(new FileInfo(ExistingFile).FullName, _validDataStore.Path);
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void InvalidPath()
        {
            var invalidDataStore = new DataStore { Path = "failure to communicate" };
        }
        [TestMethod]
        public void CreateFile()
        {
            var config = new DataStore();
            config.CreateDataStore(DataStorePath + DataStoreFileName);
            Assert.IsTrue(File.Exists(DataStorePath + DataStoreFileName));
        }

        [TestMethod]
        public void RemoveFile()
        {
            var dataStore = new DataStore();
            dataStore.CreateDataStore(DataStoreFileName);
            Assert.IsTrue(File.Exists(DataStoreFileName));
            dataStore.RemoveDataStore();
            Assert.IsFalse(File.Exists(DataStoreFileName));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void InvalidRecordCalled()
        {
            var dataStore = new DataStore();
            dataStore.CreateDataStore(DataStoreFileName);
            dataStore.GetRecordById(Guid.NewGuid());
        }

        [TestCleanup]
        public void RemoveExistingFile()
        {
            if (File.Exists(DataStorePath + DataStoreFileName))
            { File.Delete(DataStorePath + DataStoreFileName); }

        }


    }
}
