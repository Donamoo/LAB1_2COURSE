using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс репозитория, обеспечивающий основные CRUD-операции
    /// над сущностями, реализующими <see cref="IDomainObject"/>.
    /// </summary>
    /// <typeparam name="T">Тип сущности (должен реализовывать IDomainObject).</typeparam>
    public interface IRepository<T> where T : class, IDomainObject
    {
    
        void Add(T entity);

      
        void Delete(int id);

       
        IEnumerable<T> ReadAll();

        
        T ReadById(int id);

        
        void Update(T entity);
    }
}
