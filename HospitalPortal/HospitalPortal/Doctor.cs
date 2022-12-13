using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPortal
{
    internal class Doctor : Employee, IPageable
    {
        public string Speciality { get; set; }

        public void Page()
        {
            Console.WriteLine($"Paging Doctor {LastName}!");
        }

        public override void View()
        {
            Console.WriteLine();
            Console.WriteLine($"Deatils for {EmployeeNumber}:");
            Console.WriteLine($"{LastName}, {FirstName}");
            Console.WriteLine($"Doctor ({Speciality})");
            Console.WriteLine();
        }
    }
    
}
