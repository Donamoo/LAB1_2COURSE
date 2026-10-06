using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;

namespace DataAccessLayer.EntityFramework
{
    /// <summary>
    /// Реализация <see cref="IRepository{T}"/> на базе Entity Framework.
    /// </summary>
    public class EntityRepository<T> : IRepository<T>
        where T : class, IDomainObject
    {
        private DBContext context;
        private DbSet<T> set;

        public EntityRepository()
        {
            context = new DBContext();
            set = context.Set<T>();
        }

        /// <summary>Добавить сущность.</summary>
        public void Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            set.Add(entity);
            context.SaveChanges();
        }

        /// <summary>Удалить сущность по ID.</summary>
        public void Delete(int id)
        {
            var entity = set.Find(id);
            if (entity == null) return;

            set.Remove(entity);
            context.SaveChanges();
        }

        /// <summary>Получить все сущности.</summary>
        public IEnumerable<T> ReadAll()
        {
            return set.ToList();
        }

        /// <summary>Получить сущность по ID.</summary>
        public T ReadById(int id)
        {
            return set.Find(id);
        }

        /// <summary>Обновить сущность.</summary>
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existing = set.Find(entity.ID);
            if (existing == null) return;

            context.Entry(existing).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }
    }
}