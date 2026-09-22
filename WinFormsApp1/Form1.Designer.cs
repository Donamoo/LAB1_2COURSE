namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBox1 = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(638, 56);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(405, 548);
            listBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(42, 75);
            label1.Name = "label1";
            label1.Size = new Size(37, 32);
            label1.TabIndex = 1;
            label1.Text = "ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 131);
            label2.Name = "label2";
            label2.Size = new Size(67, 32);
            label2.TabIndex = 2;
            label2.Text = "ФИО";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 191);
            label3.Name = "label3";
            label3.Size = new Size(81, 32);
            label3.TabIndex = 3;
            label3.Text = "Отдел";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 260);
            label4.Name = "label4";
            label4.Size = new Size(114, 32);
            label4.TabIndex = 4;
            label4.Text = "Зарплата";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(181, 75);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(385, 39);
            textBox1.TabIndex = 5;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(181, 131);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(385, 39);
            textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(181, 191);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(385, 39);
            textBox4.TabIndex = 8;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(181, 260);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(385, 39);
            textBox5.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(43, 340);
            button1.Name = "button1";
            button1.Size = new Size(524, 101);
            button1.TabIndex = 10;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 725);
            Controls.Add(button1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Button button1;
    }
}
