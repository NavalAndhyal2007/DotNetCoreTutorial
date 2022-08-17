using DIWebApiTutorial.EmployeeService;
using DIWebApiTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Tests.FakeService
{
    public class EmployeeRepositoryFake : IEmployeeService
    {
        private readonly List<Employee> _employees;

        public EmployeeRepositoryFake()
        {
            _employees = new List<Employee>
            {
                new Employee()
                {
                    EmpID = 1, EmpName="Naval Andhyal",EmpDOB = "20/07/1998",PrevExperience=4,Salary=57000
                },
                new Employee()
                {
                    EmpID = 2, EmpName="Naval Andhyal1",EmpDOB = "22/07/1998",PrevExperience=4,Salary=60000
                },
                new Employee()
                {
                    EmpID = 3, EmpName="Naval Andhyal2",EmpDOB = "23/07/1998",PrevExperience=4,Salary=67000
                },
                new Employee()
                {
                    EmpID = 4, EmpName="Naval Andhyal3",EmpDOB = "24/07/1998",PrevExperience=4,Salary=70000
                }
            };
        }
        public Task<Employee> AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> DeleteEmployee(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployee(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
