using Microsoft.EntityFrameworkCore;
using PZ.DAL.Entities;
using PZ.DAL.Repositories.Abstractions;

namespace PZ.DAL.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Employee> _dbSet;


    public EmployeeRepository(DbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<Employee>();
    }

    public async Task<ICollection<Employee>> GetEmpWithoutRole()
    {
        return await _dbSet.Where(e => e.RoleId == null).ToListAsync();
    }
}