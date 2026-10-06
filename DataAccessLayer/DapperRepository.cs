using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Model;

namespace DataAccessLayer.Dapper
{
    /// <summary>
    /// Реализация <see cref="IRepository{T}"/> на базе Dapper.
    /// Обеспечивает CRUD-операции над сущностями.
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующий <see cref="IDomainObject"/>.</typeparam>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
       private string connectionString;

        /// <summary>
        /// Инициализирует новый экземпляр репозитория:
        /// читает строку подключения DBContext из App.config
        /// и создаёт таблицу Workers, если её ещё нет.
        /// </summary>
        public DapperRepository()
        {
            var cs = ConfigurationManager.ConnectionStrings["DBContext"];        
            connectionString = cs.ConnectionString;

            IsTable();  
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Создаёт таблицу Workers, если её ещё нет.
        /// </summary>
        private void IsTable()
        {
            const string sql = @"
                IF OBJECT_ID('dbo.Workers', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.Workers
                    (
                        ID          INT            NOT NULL PRIMARY KEY,
                        Name        NVARCHAR(MAX)  NOT NULL,
                        Department  NVARCHAR(MAX)  NOT NULL,
                        Salary      DECIMAL(18, 2) NOT NULL
                    );
                END";

            using (var conn = CreateConnection())
            {
                conn.Execute(sql);
            }
        }

        /// <summary>
        /// Добавляет нового работника в таблицу <c>dbo.Workers</c>.
        /// </summary>
        /// <param name="entity">Сущность работника для добавления.</param>
        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            const string sql = @"
                INSERT INTO dbo.Workers (ID, Name, Department, Salary)
                VALUES (@ID, @Name, @Department, @Salary);";

            using (var conn = CreateConnection())
            {
                conn.Execute(sql, entity);
            }
        }
        /// <summary>
        /// Удаляет работника по идентификатору.
        /// Если работник с таким <paramref name="id"/> не найден — ничего не происходит.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого работника.</param>
        public void Delete(int id)
        {
            const string sql = "DELETE FROM dbo.Workers WHERE ID = @Id;";

            using (var conn = CreateConnection())
            {
                conn.Execute(sql, new { Id = id });
            }
        }
        /// <summary>
        /// Возвращает список всех работников из таблицы dbo.Workers.
        /// </summary>
        /// <returns>
        /// Коллекция объектов <typeparamref name="T"/>.
        /// Если таблица пуста — пустая коллекция.
        /// </returns>
        public IEnumerable<T> ReadAll()
        {
            const string sql = @"
                SELECT ID, Name, Department, Salary
                FROM dbo.Workers;";

            using (var conn = CreateConnection())
            {
                return conn.Query<T>(sql).ToList();
            }
        }
        /// <summary>
        /// Возвращает работника по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор искомого работника.</param>
        /// <returns>
        /// Объект <typeparamref name="T"/> с указанным <paramref name="id"/>
        /// или null, если работник не найден.
        /// </returns>
        public T ReadById(int id)
        {
            const string sql = @"
                SELECT ID, Name, Department, Salary
                FROM dbo.Workers
                WHERE ID = @Id;";

            using (var conn = CreateConnection())
            {
                return conn.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }
        /// <summary>
        /// Обновляет данные существующего работника по его идентификатору.
        /// Если работник с таким <c>ID</c> не найден — ничего не происходит.
        /// </summary>
        /// <param name="entity">Сущность с обновлёнными данными.</param>
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            const string sql = @"
                UPDATE dbo.Workers
                SET Name       = @Name,
                    Department = @Department,
                    Salary     = @Salary
                WHERE ID = @ID;";

            using (var conn = CreateConnection())
            {
                conn.Execute(sql, entity);
            }
        }
    }
}