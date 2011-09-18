using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CTraderEmployees.Models
{
    public class DataStore
    {
        private string _path;
        private const string Delimiter = "|";
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
                var file = new FileInfo(value);
                _path = file.FullName;
            }
        }

        public void CreateDataStore(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            Path = path;
        }

        public void RemoveDataStore()
        {
            if (File.Exists(Path))
            { File.Delete(Path); }
        }

        public void SaveRecord(EmployeeModel employee)
        {
            var formattedRecord = employee.FormatRecord(Delimiter);
            if (IdExists(employee.Id))
            {
                RemoveRecordById(employee.Id);
            }
            CommitRecords(new[] { formattedRecord });
        }

        public void RemoveRecordById(Guid guid)
        {
            var allLines = ReadRecords();
            var survivingLines = allLines.Where(line => !line.StartsWith(guid.ToString()));
            RemoveDataStore();

            CommitRecords(survivingLines);
        }

        private void CommitRecords(IEnumerable<string> lines)
        {
            File.AppendAllLines(Path, lines);
        }

        public string[] ReadRecords()
        {

            return File.ReadAllLines(Path);
        }


        public bool IdExists(Guid id)
        {
            var lines = ReadRecords();
            return lines.Any(line => line.StartsWith(id.ToString()));
        }

        public EmployeeModel GetRecordById(Guid id)
        {
            var lines = ReadRecords();
            var record = lines.First(line => line.StartsWith(id.ToString()));
            return EmployeeModel.Parse(Delimiter, record);
        }


        public List<EmployeeModel> GetAllRecords()
        {
            var lines = ReadRecords();
            var employeeModels = lines.Select(line => EmployeeModel.Parse(Delimiter, line)).ToList();
            return employeeModels;
        }
    }
}