using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogical;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2(Logic logic)
        {
            InitializeComponent();

            Dictionary<string, decimal> averages = logic.AvgSalary();
            foreach (var pair in averages)
            {
                dataGridView1.Rows.Add(pair.Key, pair.Value);
            }

            dataGridView1.Columns["Column2"].DefaultCellStyle.Format = "N2";
        }
    }
}
