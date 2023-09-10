using ETicaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<TEntity> :  IRepository<TEntity> where TEntity : EntityBase
    {
        Task<bool> AddAsync(TEntity model);
        Task<bool> AddRangeAsync(List<TEntity> datas);
        bool Update(TEntity model);
        bool Remove(TEntity model);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<TEntity> datas);
        Task<int> SaveAsync();
    }
}
