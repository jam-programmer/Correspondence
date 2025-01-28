using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract;

public interface IContext
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    IExecutionStrategy CreateExecutionStrategy();
    void ClearTracker();
    IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
}
