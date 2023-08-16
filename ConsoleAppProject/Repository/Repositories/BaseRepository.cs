using Domain.Common;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
    {
        public BaseRepository()
        {
        }

        public void Create(T entity)
        {
            AppDbContext<T>.datas.Add(entity);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            return AppDbContext<T>.datas;
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

