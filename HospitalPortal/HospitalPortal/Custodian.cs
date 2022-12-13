using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalPortal
{
    internal class Custodian : Employee
    {
        public override void View()
        {
            Console.WriteLine();
            Console.WriteLine($"Deatils for {EmployeeNumber}:");
            Console.WriteLine($"{LastName}, {FirstName}");
            Console.WriteLine($"Custodian");
            Console.WriteLine();

        }
    }
}
