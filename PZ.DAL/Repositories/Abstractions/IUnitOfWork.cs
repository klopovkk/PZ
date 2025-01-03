﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZ.DAL.Entities;

namespace PZ.DAL.Repositories.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        public IEmployeeRepository GetEmpRepository();
        Task<int> SaveChangesAsync();
    }
}
    