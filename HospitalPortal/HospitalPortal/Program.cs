using HospitalPortal;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace PracticeProject
{
    class Program
    {
        public static string employeesFilePath = @"employeefiles";
        private static List<Employee> employees = new List<Employee>();
        private static bool running = true;

        static void Main(string[] args)
        {
            Initialise();

            while (running)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                ProcessInput(input);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void Initialise()
        {
            try
            {
                Directory.CreateDirectory(employeesFilePath);

                WriteHeader();
                LoadEmployees();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error creating data directroy. Please contact your system adminsitrator. ");
                Console.WriteLine($"Message: {ex.Message}");
                running = false;
            }
        }

        static void WriteHeader()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Welcome to Hospital Portal");
            Console.WriteLine("Type \"help\" for available commands");
            Console.WriteLine("-----------------------------");
        }

        static void ProcessInput(string input)
        {
            switch (input)
            {
                case "help":
                    PrintHelp();
                    break;
                case "add":
                    AddEmployee();
                    break;
                case "remove":
                    RemoveEmployee();
                    break;
                case "load":
                    LoadEmployees();
                    break;
                case "view":
                    ViewEmployee();
                    break;
                case "page":
                    PageEmployee();
                    break;
                default:
                    PrintInvalidCommand();
                    break;

            }

        }


        #region Adding Employees
        private static void AddEmployee()
        {
            Console.WriteLine("");

            
            Console.Write("Employee ID: ");
            string employeeID = Console.ReadLine();

            string filePath = Path.Combine(employeesFilePath, employeeID);
            if (File.Exists(filePath))
            {
                Console.WriteLine($"An employee with ID {employeeID} already exists. \n");
                return;
            }

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            bool hasValidJobTitle = false;
            string jobTitle = "";

            while (!hasValidJobTitle)
            {
                Console.Write("Job Title (doctor, nurse, custodian): ");
                jobTitle = Console.ReadLine().ToLower();

                if (jobTitle == "doctor" || jobTitle == "nurse" || jobTitle == "custodian")
                    hasValidJobTitle = true;
                else
                    Console.WriteLine("Invalid input, please try again.");
            }

            switch (jobTitle)
            {
                case "doctor":
                    AddDoctor(employeeID, firstName, lastName);
                    break;
                case "nurse":
                    AddNurse(employeeID, firstName, lastName);
                    break;
                case "custodian":
                    AddCustodian(employeeID, firstName, lastName);
                    break;
            }



        }
        static void AddDoctor(string id, string first, string last)
        {
            Console.Write("Speciality: ");
            string speciality = Console.ReadLine();

            string[] doctorData =
            {
                "doctor", id, first, last, speciality
            };

            WriteEmployeeFile(id, doctorData);
        }
        static void AddNurse(string id, string first, string last)
        {
            Console.Write("Level: ");
            string level = Console.ReadLine();

            string[] nurseData =
            {
                "nurse", id, first, last, level
            };

            WriteEmployeeFile(id, nurseData);

        }
        static void AddCustodian(string id, string first, string last)
        {
            string[] custodianData =
            {
                "custodian", id, first, last
            };

            WriteEmployeeFile(id, custodianData);
        }
        static void WriteEmployeeFile(string employeeId, string[] data)
        {
            string filePath = Path.Combine(employeesFilePath, employeeId);

            try
            {
                File.WriteAllLines(filePath, data);
                Console.WriteLine($"Employee {employeeId} added successfully.");
                LoadEmployees();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erroe creating employee file. Please contact your system administrator.");
                Console.WriteLine($"Message: {ex.Message}");
                running = false;
            }
        }

        #endregion

        #region Loading Employees
        static void LoadEmployees()
        {
            employees.Clear();

            try
            {
                string[] employeeFiles = Directory.GetFiles(employeesFilePath);
                foreach (var employeeFile in employeeFiles)
                {
                    string[] employeeData = File.ReadAllLines(employeeFile);
                    switch (employeeData[0])
                    {
                        case "doctor":
                            LoadDoctor(employeeData);
                            break;

                        case "nurse":
                            LoadNurse(employeeData);
                            break;

                        case "custodian":
                            LoadCustodian(employeeData);
                            break;



                    }
                }
                Console.WriteLine($"Loaded {employees.Count} employees. \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading employee files. Please contact your system administrator");
                Console.WriteLine($"Message: {ex.Message}");
                running = false;
            }

        }


        static void LoadDoctor(string[] employeeData)
        {
            Doctor doctor = new Doctor
            {
                EmployeeNumber = employeeData[1],
                FirstName = employeeData[2],
                LastName = employeeData[3],
                Speciality = employeeData[4]
            };
            employees.Add(doctor);

        }
        static void LoadNurse(string[] employeeData)
        {
            Nurse nurse = new Nurse
            {
                EmployeeNumber = employeeData[1],
                FirstName = employeeData[2],
                LastName = employeeData[3],
                Level = employeeData[4]
            };
            employees.Add(nurse);

        }
        static void LoadCustodian(string[] employeeData)
        {
            Custodian custodian = new Custodian
            {
                EmployeeNumber = employeeData[1],
                FirstName = employeeData[2],
                LastName = employeeData[3],
            };
            employees.Add(custodian);
        }

        #endregion

        static void RemoveEmployee()
        {
            Console.Write("Employee ID: ");
            string id = Console.ReadLine();

            string filePath = Path.Combine(employeesFilePath, id);
            if (!File.Exists(filePath))
                Console.WriteLine($"No employee with ID {id} found.");
            else
            {
                try
                {
                    File.Delete(filePath);
                    Console.WriteLine($"Employye {id} deleetd sucessfully");
                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting employee file. Please contact your system administrator.");
                    Console.WriteLine($"Message: {ex.Message}");
                    running = false;
            }
        }
    }
        static void ViewEmployee()
        {
                if(employees.Count == 0)
                {
                    Console.WriteLine("No employees loaded. \n");
                }
                else
                {
                    Console.Write("Employee ID: ");
                    string id = Console.ReadLine();

                    bool foundId = false;
                    foreach(Employee employee in employees)
                    {
                        
                        if (employee.EmployeeNumber == id)
                        {
                            employee.View();
                            foundId = true;
                        }

                    }
                    if(!foundId)
                        Console.WriteLine($"No employee found with ID: {id}\n");
                }
        }
        static void PageEmployee()
        {
                if (employees.Count == 0)
                {
                    Console.WriteLine("Failed to page! No employees loaded. \n");
                }
                else
                {
                    foreach(Employee employee in employees)
                    {
                        if(employee is IPageable)
                        {
                            IPageable pageableEmployee = (IPageable)employee;
                            pageableEmployee.Page();   
                        }
                    }
                }

        }
        static void PrintHelp()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine("add - Add a new employee to the portal.");
            Console.WriteLine("remove - Remove an employee from the portal.");
            Console.WriteLine("load - Load existing employees from file.");
            Console.WriteLine("view - View an employee.");
            Console.WriteLine("page - Page all medical employees.");


        }
        static void PrintInvalidCommand()
        {
            Console.WriteLine("Command not recognised, please try again.");
            Console.WriteLine("Type \"help\" for available commands");
        }

    }
}
