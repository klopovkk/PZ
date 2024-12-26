namespace PZ.BLL.Services.Abstractions;

public interface IEmployeeService
{
    Task SetRoleAsync(Guid employeeId, Guid roleId);
}