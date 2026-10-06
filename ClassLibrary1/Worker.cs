using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// Интерфейс сущности.
    /// Гарантирует наличие уникального идентификатора <see cref="ID"/>.
    /// </summary>
    public interface IDomainObject
    {
        int ID { get; set; }
    }
    /// <summary>
    /// Сущность «Работник предприятия».
    /// </summary>
    public class Worker : IDomainObject
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public decimal Salary { get; set; }
    }
}
