using System;
using System.IO;

namespace CTraderEmployees.Models
{
    public class DataStore
    {
        internal string _path;
        private string delimiter ="|";
        public string Path
        {
            get { return _path; }
            set
            {
                if (value == null) return;
                if (!File.Exists(value))
                {
                    throw new FileNotFoundException();
                }
                _path = value;
            }
        }

        public void CreateDataStore(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            this.Path = path;
        }

        public void RemoveDataStore()
        {
            if (File.Exists(this.Path))
            { File.Delete(this.Path); }
        }

        public void SaveRecord(EmployeeModel employee)
        {

            string formattedRecord = employee.FormatRecord(this.delimiter);
            formattedRecord += Environment.NewLine;
            File.AppendAllText(this.Path, formattedRecord);
        }

    }
}