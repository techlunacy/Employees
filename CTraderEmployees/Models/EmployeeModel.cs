using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CTraderEmployees.Models
{
    public class EmployeeModel

    {
        public Guid Id;
        internal const int NumberOfFields = 6;
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [Range(0, 200, ErrorMessage = "Please Enter a Valid Age")]
        public int Age { get; set; }
        [Required]
        [DisplayName("Is Employee?")]
        [UIHint("Boolean")]
        public bool IsCurrentEmployee { get; set; }
        [Required]
        [DisplayName("Gender")]
        [UIHint("Enum")]
        [EnumDataType(typeof(EmployeeGender))]
        public EmployeeGender Gender { get; set; }

        public EmployeeModel()
        {
            Id = Guid.NewGuid();
        }

        public string FormatRecord(string delimiter)
        {
            return Id + delimiter + DataStore.EncryptData(delimiter, FirstName) + delimiter + DataStore.EncryptData(delimiter, LastName) + delimiter +
                   Gender + delimiter + Age + delimiter + IsCurrentEmployee;
        }

        public static EmployeeModel Parse(string delimiter, string record)
        {
            var fields = record.Split(new[] { delimiter }, StringSplitOptions.None);
            EmployeeGender employeeGender;
            EmployeeModel employeeModel;
            if (fields.Length == NumberOfFields && Enum.TryParse(fields[3], true, out employeeGender))
            {
                employeeModel = new EmployeeModel
                                    {
                                        Id = Guid.Parse(fields[0]),
                                        FirstName = DataStore.DecryptData(delimiter, fields[1]),
                                        LastName = DataStore.DecryptData(delimiter, fields[2]),
                                        Gender = employeeGender,
                                        Age = int.Parse(fields[4]),
                                        IsCurrentEmployee = Boolean.Parse(fields[5])
                                    };
            }
            else
            {
                throw new InvalidDataException();
            }
            return employeeModel;
        }
    }
}