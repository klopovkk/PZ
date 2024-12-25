using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZ.DAL.Entities;
using PZ.DAL.Repositories.Abstractions;

namespace PZ.DAL.Repositories.Abstractions
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Task<ICollection<Employee>> GetEmpWithoutRole();
    }
}
