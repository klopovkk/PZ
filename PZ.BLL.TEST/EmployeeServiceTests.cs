using Moq;
using PZ.BLL.Services;
using PZ.CCL;
using PZ.CCL.Actors;
using PZ.DAL.Entities;
using PZ.DAL.Repositories.Abstractions;

namespace PZ.BLL.TEST;

public class EmployeeServiceTests
{
    [Fact]
    public async Task SetRoleAsync_SuccessfullySetsRoleForEmployee()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var employees = new List<Employee>
            {
                new Employee { Id = new Guid(), RoleId = roleId },

                new Employee { Id = employeeId, RoleId = null },
            };
        var roles = new List<Role>
            {
                new Role { Id = roleId }
            };

        var mockEmployeeRepo = new Mock<IEmployeeRepository>();
        mockEmployeeRepo.Setup(r => r.GetEmpWithoutRole()).ReturnsAsync(employees);

        var mockRoleRepo = new Mock<IRepository<Role>>();
        mockRoleRepo.Setup(r => r.GetByIdAsync(roleId)).ReturnsAsync(roles.First());

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.GetEmpRepository()).Returns(mockEmployeeRepo.Object);
        mockUnitOfWork.Setup(u => u.GetRepository<Role>()).Returns(mockRoleRepo.Object);

        var service = new EmployeeService(mockUnitOfWork.Object);

        var adminUser = new Admin(Guid.NewGuid(), "admin");
        AuthContext.SetUser(adminUser);

        // Act
        await service.SetRoleAsync(employeeId, roleId);

        // Assert
        mockEmployeeRepo.Verify(r => r.Update(It.Is<Employee>(e => e.Id == employeeId && e.RoleId == roleId)), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task SetRoleAsync_ThrowsUnauthorizedAccessException_WhenUserIsNotAdmin()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new EmployeeService(mockUnitOfWork.Object);

        var nonAdminUser = new EmployeeActor(Guid.NewGuid(), "no_admin");
        AuthContext.SetUser(nonAdminUser);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.SetRoleAsync(employeeId, roleId));
    }

    [Fact]
    public async Task SetRoleAsync_ThrowsNullReferenceException_WhenRoleNotFound()
    {

        // Arrange
        var employeeId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var roles = new List<Role>
            {
                new Role { Id = roleId }
            };
        var mockEmployeeRepo = new Mock<IEmployeeRepository>();
        var mockRoleRepo = new Mock<IRepository<Role>>();
        mockRoleRepo.Setup(r => r.GetByIdAsync(roleId)).ReturnsAsync((Role)null);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.GetEmpRepository()).Returns(mockEmployeeRepo.Object);
        mockUnitOfWork.Setup(u => u.GetRepository<Role>()).Returns(mockRoleRepo.Object);

        var service = new EmployeeService(mockUnitOfWork.Object);

        var adminUser = new Admin(Guid.NewGuid(), "admin");
        AuthContext.SetUser(adminUser);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.SetRoleAsync(employeeId, roleId));
    }

    [Fact]
    public async Task SetRoleAsync_ThrowsNullReferenceException_WhenEmployeeNotFound()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var roleId = Guid.NewGuid();

        var employees = new List<Employee>();
        var roles = new List<Role>
            {
                new Role { Id = roleId }
            };

        var mockEmployeeRepo = new Mock<IEmployeeRepository>();
        mockEmployeeRepo.Setup(r => r.GetEmpWithoutRole()).ReturnsAsync(employees);

        var mockRoleRepo = new Mock<IRepository<Role>>();
        mockRoleRepo.Setup(r => r.GetByIdAsync(roleId)).ReturnsAsync(roles.First());

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.GetEmpRepository()).Returns(mockEmployeeRepo.Object);
        mockUnitOfWork.Setup(u => u.GetRepository<Role>()).Returns(mockRoleRepo.Object);

        var service = new EmployeeService(mockUnitOfWork.Object);

        var adminUser = new Admin(Guid.NewGuid(), "admin");
        AuthContext.SetUser(adminUser);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.SetRoleAsync(employeeId, roleId));
    }
}
