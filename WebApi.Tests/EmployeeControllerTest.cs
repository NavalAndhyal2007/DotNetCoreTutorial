using DIWebApiTutorial.Controllers;
using DIWebApiTutorial.EmployeeService;
using DIWebApiTutorial.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.Tests
{
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeService> employeeService;
        
        //private readonly IEmployeeService employeeService;


        public EmployeeControllerTest()
        //public EmployeeControllerTest()
        {
            this.employeeService = new Mock<IEmployeeService>();
            //this.employeeService = employeeService;
        }

        [Fact]
        public async void AddEmployeeTest()
        {
            Employee employee = null;

            employeeService.Setup(srvc => srvc.AddEmployee(It.IsAny<Employee>())).Callback<Employee>(x => employee = x);
            Employee addEmployee = new Employee()
            {
                EmpID = 2,
                EmpName = "Naval Andhyal1",
                EmpDOB = "22/07/1998",
                PrevExperience = 4,
                Salary = 60000
            };
            await employeeService.Object.AddEmployee(addEmployee);

            //Assert
            employeeService.Verify(x => x.AddEmployee(It.IsAny<Employee>()), Times.Once);
            Assert.Equal(employee.EmpID, addEmployee.EmpID);
            Assert.Equal(employee.EmpName, addEmployee.EmpName);
            Assert.Equal(employee.EmpDOB, addEmployee.EmpDOB);
            Assert.Equal(employee.PrevExperience, addEmployee.PrevExperience);
            Assert.Equal(employee.Salary, addEmployee.Salary);

        }

        [Fact]
        public async void DeleteEmployeeTest()
        {
            //Arrange
            var EmpId = 2;
            employeeService.Setup(srvc => srvc.DeleteEmployee(EmpId));
            //Act
            await employeeService.Object.DeleteEmployee(EmpId);
            //Assert
            employeeService.Verify(repo => repo.DeleteEmployee(EmpId), Times.Once);
        }

        [Fact]
        public async void UpdateEmployeeTest()
        {
            Employee employee = null;

            employeeService.Setup(srvc => srvc.UpdateEmployee(It.IsAny<Employee>())).Callback<Employee>(x => employee = x);
            Employee addEmployee = new Employee()
            {
                EmpID = 3,
                EmpName = "Naval Andhyal1_Updated",
                EmpDOB = "22/07/1998",
                PrevExperience = 5,
                Salary = 70000
            };
            await employeeService.Object.UpdateEmployee(addEmployee);

            //Assert
            employeeService.Verify(x => x.UpdateEmployee(It.IsAny<Employee>()), Times.Once);
            Assert.Equal(employee.EmpID, addEmployee.EmpID);
            Assert.Equal(employee.EmpName, addEmployee.EmpName);
            Assert.Equal(employee.EmpDOB, addEmployee.EmpDOB);
            Assert.Equal(employee.PrevExperience, addEmployee.PrevExperience);
            Assert.Equal(employee.Salary, addEmployee.Salary);

        }

        [Fact]
        public async void GetEmployeeTest()
        {
           // Employee employee = null;


            employeeService.Setup(emp => emp.GetEmployees()).ReturnsAsync(
                new List<Employee>()
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
                }
                );

            //int n = 0;
            //employeeService.Setup(srvc => srvc.GetEmployee(It.IsAny<int>())).Callback<int>(x => n = x);
            Employee existingEmployee = new()
            {
                EmpID = 3,
                EmpName = "Naval Andhyal2",
                EmpDOB = "23/07/1998",
                PrevExperience = 4,
                Salary = 67000
            };
            Employee gotEmployee = employeeService.Object.GetEmployees().Result.Find(emp => emp.EmpID == 3);
            //if()
            //Assert
            //employeeService.Verify(x => x.GetEmployee(It.IsAny<int>()), Times.Once);
            Assert.Equal(gotEmployee.EmpID, existingEmployee.EmpID);
            Assert.Equal(gotEmployee.EmpName, existingEmployee.EmpName);
            Assert.Equal(gotEmployee.EmpDOB, existingEmployee.EmpDOB);
            Assert.Equal(gotEmployee.PrevExperience, existingEmployee.PrevExperience);
            Assert.Equal(gotEmployee.Salary, existingEmployee.Salary);
        }
        [Fact]
        public async void GetAllEmployeesTest()
        {

            employeeService.Setup(emp => emp.GetEmployees()).ReturnsAsync(
                new List<Employee>()
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
                }
                );
            var GetAllEmployeesResult = await employeeService.Object.GetEmployees();
            //var GetEmployeeResult = await employeeService.Object.GetEmployee(3);

            Assert.True(GetAllEmployeesResult.Count == 4);
            //Assert.IsType<Employee>(GetEmployeeResult);

        }
    }
}
