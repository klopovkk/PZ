using System.Net;
using PZ.BLL.Services.Abstractions;
using PZ.CCL;
using PZ.CCL.Actors;
using PZ.DAL.Entities;
using PZ.DAL.Repositories.Abstractions;

namespace PZ.BLL.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _empRepository;
    private readonly IRepository<Role> _roleRepository;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _empRepository = unitOfWork.GetEmpRepository();
        _roleRepository = unitOfWork.GetRepository<Role>();
    }

    public async Task SetRoleAsync(Guid employeeId, Guid roleId)
    {
        var user = AuthContext.getUser();
        var userType = user.GetType();
        if (userType != typeof(Admin))
        {
            throw new UnauthorizedAccessException("Доступ заблокований!");
        }

        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
        {
            throw new NullReferenceException("Неправильний ввід");
        }
        var employees = await _empRepository.GetEmpWithoutRole();
        var employer = employees.Where(e => e.Id == employeeId).FirstOrDefault();
        if (employer == null)
        {
            throw new NullReferenceException("Неправильний ввід");
        }
        employer.RoleId = roleId;
        _empRepository.Update(employer);
        await _unitOfWork.SaveChangesAsync();
    }
}