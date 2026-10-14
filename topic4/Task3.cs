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

        private void Calculate(char op)
        {
            if (!int.TryParse(txtA.Text, out int a) || !int.TryParse(txtB.Text, out int b))
            {
                lblResult.Text = "Xeta: Duzgun eded daxil edin!";
                return;
            }

            switch (op)
            {
                case '+': lblResult.Text = "Netice: " + (a + b); break;
                case '-': lblResult.Text = "Netice: " + (a - b); break;
                case '*': lblResult.Text = "Netice: " + (a * b); break;
                case '/':
                    if (b == 0)
                    {
                        lblResult.Text = "Sifira bolmek olmaz!";
                    }
                    else
                    {
                        lblResult.Text = "Netice: " + ((double)a / b);
                    }
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) => Calculate('+');
        private void btnSub_Click(object sender, EventArgs e) => Calculate('-');
        private void btnMult_Click(object sender, EventArgs e) => Calculate('*');
        private void btnDiv_Click(object sender, EventArgs e) => Calculate('/');
    }
}