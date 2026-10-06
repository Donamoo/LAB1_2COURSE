using DataAccessLayer;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogical
{
    /// <summary>
    /// Класс бизнес-логики для работы с работниками предприятия.
    /// </summary>
    public class Logic
    {
        /// <summary>
        /// Репозиторий для доступа к данным работников.
        /// Есть выбор:
        /// public IRepository<Worker> repository = new EntityRepository<Worker>();
        /// или
        /// IRepository<Worker> repository = new DapperRepository<Worker>();
        /// </summary>
        public IRepository<Worker> repository = new EntityRepository<Worker>();


        /// <summary>
        /// Добавляет нового работника.
        /// </summary>
        public bool AddWorker(int id, string name, string departament, decimal salary)
        {
            if (repository.ReadById(id) != null)
                return false;

            var worker = new Worker
            {
                ID = id,
                Name = name,
                Department = departament,
                Salary = salary
            };
            repository.Add(worker);
            return true;
        }

        /// <summary>Возвращает список всех работников.</summary>
        public List<Worker> GetAll()
        {
            return repository.ReadAll().ToList();
        }

        /// <summary>Удаляет работника по идентификатору.</summary>
        public bool DeleteWorker(int id)
        {
            var worker = repository.ReadById(id);
            if (worker == null) return false;

            repository.Delete(id);
            return true;
        }

        /// <summary>Изменяет данные работника.</summary>
        public bool UpdateWorker(int id, string name, string department, decimal salary)
        {
            var worker = repository.ReadById(id);
            if (worker == null) return false;

            worker.Name = name;
            worker.Department = department;
            worker.Salary = salary;
            repository.Update(worker);
            return true;
        }

        /// <summary>Группировка работников по отделам.</summary>
        public Dictionary<string, List<Worker>> GroupByDepartment()
        {
            return repository.ReadAll()
                .GroupBy(w => w.Department)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>Средняя зарплата по каждому отделу.</summary>
        public Dictionary<string, decimal> AvgSalary()
        {
            return repository.ReadAll()
                .GroupBy(w => w.Department)
                .ToDictionary(g => g.Key, g => g.Average(w => w.Salary));
        }
    }
}