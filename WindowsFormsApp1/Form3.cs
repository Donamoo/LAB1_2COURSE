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
using Model;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3(Logic logic)
        {
            InitializeComponent();

            Dictionary<string, List<Worker>> groups = logic.GroupByDepartment();
            foreach (var pair in groups)
            {
                dataGridView1.Rows.Add(pair.Key, pair.Value.Count);
            }
        }
    }
}
