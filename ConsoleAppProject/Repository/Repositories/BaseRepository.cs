using System.Reflection;
using Domain.Common;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
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
            AppDbContext<T>.datas.Remove(entity);
        }

        public void Edit(T entity)
        {
            T existingEntity = AppDbContext<T>.datas.FirstOrDefault(e => e.Id == entity.Id);

            if (existingEntity != null)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(existingEntity, property.GetValue(entity));
                    }
                }
            }
        }

        public List<T> GetAll()
        {
            return AppDbContext<T>.datas;
        }

        public T GetById(int id)
        {
            return AppDbContext<T>.datas.Find(entity => entity.Id == id);
        }
    }
}