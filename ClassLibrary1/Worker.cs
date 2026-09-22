using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Сущность «Работник предприятия».
    /// </summary>
    public class Worker
    {
        public int ID {  get; set; }

        public string Name { get; set; }

        public string Department {get ; set; }

        public decimal Salary { get; set; }

    }
}
