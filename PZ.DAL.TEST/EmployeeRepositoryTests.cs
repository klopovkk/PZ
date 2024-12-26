using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Toolkit.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using PZ.DAL.Entities;
using PZ.DAL.Repositories;
using PZ.DAL.Repositories.Abstractions;

namespace PZ.DAL.TEST
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public async Task GetEmpWithoutRole_ReturnsEmployeesWithoutRole()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), FullName = "John DOU", RoleId = null },
                new Employee { Id = Guid.NewGuid(), FullName = "Jane DOU", RoleId = Guid.NewGuid() },
                new Employee { Id = Guid.NewGuid(), FullName = "Tom DOU", RoleId = null }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            mockDbSet.As<IAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Employee>()).Returns(mockDbSet.Object);

            var unitOfWork = new UnitOfWork(mockContext.Object);
            var repository = unitOfWork.GetEmpRepository();

            // Act
            var result = await repository.GetEmpWithoutRole();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); // Expect 2 employees without roles
            Assert.All(result, e => Assert.Null(e.RoleId));

        }

        [Fact]
        public async Task GetEmpWithoutRole_ReturnsEmptyList_WhenNoEmployees()
        {
            // Arrange
            var employees = new List<Employee>().AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            mockDbSet.As<IAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Employee>()).Returns(mockDbSet.Object);

            var unitOfWork = new UnitOfWork(mockContext.Object);
            var repository = unitOfWork.GetEmpRepository();

            // Act
            var result = await repository.GetEmpWithoutRole();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEmpWithoutRole_ReturnsEmptyList_WhenAllEmployeesHaveRoles()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), FullName = "John DOU", RoleId = Guid.NewGuid() },
                new Employee { Id = Guid.NewGuid(), FullName = "Jane DOU", RoleId = Guid.NewGuid() },
                new Employee { Id = Guid.NewGuid(), FullName = "Tom DOU", RoleId = Guid.NewGuid() }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Employee>>();
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockDbSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            mockDbSet.As<IAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Set<Employee>()).Returns(mockDbSet.Object);

            var unitOfWork = new UnitOfWork(mockContext.Object);
            var repository = unitOfWork.GetEmpRepository();

            // Act
            var result = await repository.GetEmpWithoutRole();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); 
        }
    }
}