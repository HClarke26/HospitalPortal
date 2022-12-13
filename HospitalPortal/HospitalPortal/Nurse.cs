using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPortal
{
    internal class Nurse : Employee, IPageable
    {
        public string Level { get; set; }

        public void Page()
        {
            Console.WriteLine($"Paging Nurse {LastName}!");
        }

        public override void View()
        {
            Console.WriteLine();
            Console.WriteLine($"Deatils for {EmployeeNumber}:");
            Console.WriteLine($"{LastName}, {FirstName}");
            Console.WriteLine($"Nurse ({Level})");
            Console.WriteLine();
        }
    }
}


