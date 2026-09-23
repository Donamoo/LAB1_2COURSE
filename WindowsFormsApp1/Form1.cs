using BusinessLogical;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Главная форма приложения. Отображает список работников,
    /// позволяет добавлять, удалять, изменять их и просматривать бизнес-отчёты.
    /// </summary>
    public partial class Form1 : Form
    {
        public Logic Logic { get; set; }

        public Form1()
        {
            InitializeComponent();
            Logic = new Logic();

            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            dataGridView1.Rows.Clear();
            foreach (var w in Logic.GetAll())
            {
                dataGridView1.Rows.Add(w.ID, w.Name, w.Department, w.Salary);
            }
        }

        private bool FoolCheck(out int id, out string name, out string department, out decimal salary)
        {
            id = 0;
            name = "";
            department = "";
            salary = 0m;

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введите ID.");
                return false;
            }
            if (!int.TryParse(textBox1.Text, out id))
            {
                MessageBox.Show("ID должен быть целым числом.");
                return false;
            }
            if (id <= 0)
            {
                MessageBox.Show("ID должен быть положительным числом.");
                return false;
            }

          
            name = textBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введите ФИО.");
                return false;
            }
            if (!name.All(c => char.IsLetter(c) || c == ' '))
            {
                MessageBox.Show("ФИО должно содержать только буквы.");
                return false;
            }


            department = textBox3.Text.Trim();
            if (string.IsNullOrWhiteSpace(department))
            {
                MessageBox.Show("Введите отдел.");
                return false;
            }



            if (!decimal.TryParse(textBox4.Text, out salary))
            {
                MessageBox.Show("Зарплата должна быть числом.");
                return false;
            }
            if (salary <= 0)
            {
                MessageBox.Show("Зарплата должна быть положительным числом.");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!FoolCheck(out int id, out string name, out string department, out decimal salary))
                return;

            if (!Logic.AddWorker(id, name, department, salary))
            {
                MessageBox.Show("Работник с таким ID уже существует.");
                return;
            }

            dataGridView1.Rows.Clear();
            foreach (var w in Logic.GetAll())
            {
                dataGridView1.Rows.Add(w.ID, w.Name, w.Department, w.Salary);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int id) || id <= 0)
            {
                MessageBox.Show("Введите корректный ID.");
                return;
            }

            if (Logic.DeleteWorker(id))
            {
                dataGridView1.Rows.Clear();
                foreach (var w in Logic.GetAll())
                {
                    dataGridView1.Rows.Add(w.ID, w.Name, w.Department, w.Salary);
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("Работник с таким ID не найден.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!FoolCheck(out int id, out string name, out string department, out decimal salary))
                return;

            if (Logic.UpdateWorker(id, name, department, salary))
            {
                dataGridView1.Rows.Clear();
                foreach (var w in Logic.GetAll())
                {
                    dataGridView1.Rows.Add(w.ID, w.Name, w.Department, w.Salary);
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("Работник с таким ID не найден.");
            }
        }


        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var row = dataGridView1.CurrentRow;
            textBox1.Text = row.Cells["colID"].Value.ToString();
            textBox2.Text = row.Cells["colName"].Value.ToString();
            textBox3.Text = row.Cells["colDepartment"].Value.ToString();
            textBox4.Text = row.Cells["colSalary"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form3(Logic).ShowDialog(this);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            new Form2(Logic).ShowDialog(this);
        }
    }
}