using System;

namespace CTraderEmployees.Models
{
    public class EmployeeModel
    {
        public string FirstName;
        public string LastName;
        public int Age;
        public bool IsCurrentEmployee;
        public EmployeeGender Gender;
        public Guid Id;

        public string FormatRecord(string delimiter)
        {
            return this.Id + delimiter + this.FirstName + delimiter + this.LastName + delimiter +
                   this.Gender + delimiter + this.Age + delimiter + this.IsCurrentEmployee;
        }
    }
}