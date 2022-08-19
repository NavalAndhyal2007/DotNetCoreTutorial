using DIWebApiTutorial.Controllers;
using DIWebApiTutorial.EmployeeService;
using DIWebApiTutorial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WebApi.Tests
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestPriorityAttribute : Attribute
    {
        public int Priority { get; private set; }

        public TestPriorityAttribute(int priority) => Priority = priority;
    }

    public class PriorityOrderer : ITestCaseOrderer
    {

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            string assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
            foreach (TTestCase testCase in testCases)
            {
                int priority = testCase.TestMethod.Method
                    .GetCustomAttributes(assemblyName)
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (TTestCase testCase in
                sortedMethods.Keys.SelectMany(
                    priority => sortedMethods[priority].OrderBy(
                        testCase => testCase.TestMethod.Method.Name)))
            {
                yield return testCase;
            }
        }

        private static TValue GetOrCreate<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary, TKey key)
            where TKey : struct
            where TValue : new() =>
            dictionary.TryGetValue(key, out TValue result)
                ? result
                : (dictionary[key] = new TValue());
    }

    [TestCaseOrderer("OrechstrationService.Project.Orderers.PriorityOrderer", "OrechstrationService.Project")]
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeService> employeeService;
        private readonly IEmployeeService employeeService1;

        private readonly EmployeeController employeeController;
        private readonly IMemoryCache memoryCache;
        public DbContextOptions<EmployeeContext> dbContextOptions { get; }
        public string ConnectionString = "Data Source=EPINHYDW05C1\\MSSQLSERVER1;Initial Catalog=EmployeeDB;Integrated Security=True";

        public EmployeeControllerTest()
        {
            employeeService = new Mock<IEmployeeService>();
            memoryCache = new MemoryCache(new MemoryCacheOptions());
            dbContextOptions = new DbContextOptionsBuilder<EmployeeContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            var context = new EmployeeContext(dbContextOptions);
            employeeService1 = new EmployeeRepository(context);
            employeeController = new EmployeeController(employeeService1, memoryCache);
        }

        [Fact, TestPriority(6)]
        public async void AddEmployeeTest()
        {
            //Arrange
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

            //Act
            await employeeService.Object.AddEmployee(addEmployee);

            //Assert
            employeeService.Verify(x => x.AddEmployee(It.IsAny<Employee>()), Times.Once);
            Assert.Equal(employee.EmpID, addEmployee.EmpID);
            Assert.Equal(employee.EmpName, addEmployee.EmpName);
            Assert.Equal(employee.EmpDOB, addEmployee.EmpDOB);
            Assert.Equal(employee.PrevExperience, addEmployee.PrevExperience);
            Assert.Equal(employee.Salary, addEmployee.Salary);

        }

        [Fact, TestPriority(5)]
        public async void AddEmployee_Moq_Test()
        {
            //Arrange
            Employee addEmployee = new Employee()
            {
                EmpID = 101,
                EmpName = "Onkar Mehta",
                EmpDOB = "22/07/1998",
                PrevExperience = 5,
                Salary = 70000
            };

            //Act
            Employee gotEmployee = await employeeService1.AddEmployee(addEmployee);

            //Assert
            Assert.Equal(gotEmployee.EmpID, addEmployee.EmpID);
            Assert.Equal(gotEmployee.EmpName, addEmployee.EmpName);
            Assert.Equal(gotEmployee.EmpDOB, addEmployee.EmpDOB);
            Assert.Equal(gotEmployee.PrevExperience, addEmployee.PrevExperience);
            Assert.Equal(gotEmployee.Salary, addEmployee.Salary);

        }

        [Fact, TestPriority(0)]
        public async void DeleteEmployeeTest()
        {
            //Arrange
            var EmpId = 101;

            //Act
            Employee gotEmployee = await employeeService1.DeleteEmployee(EmpId);

            //Assert
            Assert.Equal(gotEmployee.EmpID, EmpId);
        }


        [Fact, TestPriority(2)]
        public async void DeleteEmployee_Moq_Test()
        {
            //Arrange
            var EmpId = 2;
            employeeService.Setup(srvc => srvc.DeleteEmployee(EmpId));

            //Act
            await employeeService.Object.DeleteEmployee(EmpId);

            //Assert
            employeeService.Verify(repo => repo.DeleteEmployee(EmpId), Times.Once);
        }


        [Fact, TestPriority(3)]
        public async void UpdateEmployee_Moq_Test()
        {
            //Arrange
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
            //Act
            await employeeService.Object.UpdateEmployee(addEmployee);

            //Assert
            employeeService.Verify(x => x.UpdateEmployee(It.IsAny<Employee>()), Times.Once);
            Assert.Equal(employee.EmpID, addEmployee.EmpID);
            Assert.Equal(employee.EmpName, addEmployee.EmpName);
            Assert.Equal(employee.EmpDOB, addEmployee.EmpDOB);
            Assert.Equal(employee.PrevExperience, addEmployee.PrevExperience);
            Assert.Equal(employee.Salary, addEmployee.Salary);

        }

        [Fact, TestPriority(7)]
        public async void GetEmployeeTest()
        {

            //Arrange
            Employee existingEmployee = new Employee()
            {
                EmpID = 101,
                EmpName = "Onkar Mehta",
                EmpDOB = "22/07/1998",
                EmpJoiningDate = "10/10/2019",
                PrevExperience = 5,
                Salary = 70000,
            };

            //Act
            Employee gotEmployee = await employeeService1.GetEmployee(101);

            //Assert
            Assert.Equal(gotEmployee.EmpID, existingEmployee.EmpID);
            Assert.Equal(gotEmployee.EmpName, existingEmployee.EmpName);
            Assert.Equal(gotEmployee.EmpDOB, existingEmployee.EmpDOB);
            Assert.Equal(gotEmployee.PrevExperience, existingEmployee.PrevExperience);
            Assert.Equal(gotEmployee.Salary, existingEmployee.Salary);
        }

        [Fact,TestPriority(9)]
        public async void GetAllEmployeesTest()
        {
            //Arrange
            //Act
            var GetAllEmployeesResult = await employeeService1.GetEmployees();

            //Assert
            Assert.True(GetAllEmployeesResult.Count == 105);
        }
        [Fact, TestPriority(8)]
        public async void GetAllEmployees_Moq_Test()
        {

            //Arrange
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

            //Act
            var GetAllEmployeesResult = await employeeService.Object.GetEmployees();

            //Assert
            Assert.True(GetAllEmployeesResult.Count == 4);

        }
    }
}
