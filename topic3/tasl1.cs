using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Ad hissesi bos ola bilmez!");
                return;
            }

            string gender = rbMale.Checked ? "Kisi" : "Qadin";
            string studentInfo = $"{txtName.Text} | Yas: {numAge.Value} | Kurs: {cmbCourse.SelectedItem} | Cins: {gender}";
            
            listBoxStudents.Items.Add(studentInfo);
            txtName.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxStudents.SelectedIndex == -1)
            {
                MessageBox.Show("Silmek ucun telebe secin!");
                return;
            }

            DialogResult result = MessageBox.Show("Secilmis telebeni silmeye eminsiniz?", "Təsdiq", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                listBoxStudents.Items.RemoveAt(listBoxStudents.SelectedIndex);
                lblSelectedInfo.Text = "Secilmis Telebe: ";
            }
        }

        private void listBoxStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxStudents.SelectedItem != null)
            {
                lblSelectedInfo.Text = "Secilmis Telebe: " + listBoxStudents.SelectedItem.ToString();
            }
        }
    }
}
