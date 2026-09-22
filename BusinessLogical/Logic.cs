using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BusinessLogical
{
    /// <summary>
    /// Класс бизнес-логики для работы с работниками предприятия.
    /// </summary>
    public class Logic
    {
        List<Worker> Workers = new List<Worker>();

        /// <summary>
        /// Добавляет нового работника в коллекцию.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <param name="name">ФИО работника.</param>
        /// <param name="departament">Название отдела.</param>
        /// <param name="salary">Заработная плата.</param>
        public bool AddWorker(int id, string name, string departament, decimal salary)
        {
            if (Workers.Any(w => w.ID == id))
                return false;

            Worker worker = new Worker()
            {
                ID = id,
                Name = name,
                Department = departament,
                Salary = salary
            };
            Workers.Add(worker);
            return true;
        }

        /// <summary>
        /// Возвращает копию списка всех работников.
        /// </summary>
        /// <returns>Список объектов <see cref="Worker"/>.</returns>
        public List<Worker> GetAll()
        {
            return new List<Worker>(Workers);
        }

        /// <summary>
        /// Удаляет работника по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого работника.</param>
        /// <returns><c>true</c>, если работник найден и удалён; иначе <c>false</c>.</returns>
        public bool DeleteWorker(int id)
        {
            Worker worker = Workers.FirstOrDefault(w => w.ID == id);
            if (worker == null) return false;
            Workers.Remove(worker);
            return true;
        }

        /// <summary>
        /// Изменяет данные работника по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор изменяемого работника.</param>
        /// <param name="name">Новое ФИО.</param>
        /// <param name="department">Новый отдел.</param>
        /// <param name="salary">Новая зарплата.</param>
        /// <returns><c>true</c>, если работник найден и обновлён; иначе <c>false</c>.</returns>
        public bool UpdateWorker(int id, string name, string department, decimal salary)
        {
            Worker worker = Workers.FirstOrDefault(w => w.ID == id);
            if (worker == null) return false;

            worker.Name = name;
            worker.Department = department;
            worker.Salary = salary;
            return true;
        }

        /// <summary>
        /// Бизнес-функция: группировка работников по отделам.
        /// </summary>
        /// <returns>
        /// Словарь, где ключ — название отдела,
        /// значение — список работников этого отдела.
        /// </returns>
        public Dictionary<string, List<Worker>> GroupByDepartment()
        {
            return Workers
                .GroupBy(w => w.Department)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Бизнес-функция: средняя зарплата по каждому отделу.
        /// </summary>
        /// <returns>
        /// Словарь, где ключ — название отдела,
        /// значение — средняя заработная плата в этом отделе.
        /// </returns>
        public Dictionary<string, decimal> AverageSalaryByDepartment()
        {
            return Workers
                .GroupBy(w => w.Department)
                .ToDictionary(g => g.Key, g => g.Average(w => w.Salary));
        }
    }
}
