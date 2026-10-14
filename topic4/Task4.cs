using System;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private int currentQuestion = 0;
        private int score = 0;

        private string[] questions = {
            "C# hansi sirket terefindan yaradilib?",
            "C#-da deyisen taypi deyismeyen olan hansidir?",
            "WinForms-da duyme componenti hansidir?",
            "Giris noqtesi olan metod hansidir?",
            "OOP-nin esas prinsipi hansidir?"
        };

        private string[,] options = {
            { "Google", "Apple", "Microsoft", "Oracle" },
            { "int", "const", "var", "string" },
            { "TextBox", "Label", "Button", "ListBox" },
            { "Start()", "Main()", "Run()", "Init()" },
            { "Loop", "Encapsulation", "Condition", "Compilation" }
        };

        private int[] correctAnswers = { 2, 1, 2, 1, 1 };

        public Form1()
        {
            InitializeComponent();
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (currentQuestion < questions.Length)
            {
                lblQuestion.Text = questions[currentQuestion];
                rbOpt1.Text = options[currentQuestion, 0];
                rbOpt2.Text = options[currentQuestion, 1];
                rbOpt3.Text = options[currentQuestion, 2];
                rbOpt4.Text = options[currentQuestion, 3];

                rbOpt1.Checked = rbOpt2.Checked = rbOpt3.Checked = rbOpt4.Checked = false;
                progressBar1.Value = (currentQuestion + 1) * 20;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int selectedOption = -1;
            if (rbOpt1.Checked) selectedOption = 0;
            else if (rbOpt2.Checked) selectedOption = 1;
            else if (rbOpt3.Checked) selectedOption = 2;
            else if (rbOpt4.Checked) selectedOption = 3;

            if (selectedOption == -1)
            {
                MessageBox.Show("Xahis olunur cavab secin!");
                return;
            }

            if (selectedOption == correctAnswers[currentQuestion]) score += 20;

            currentQuestion++;

            if (currentQuestion < questions.Length)
            {
                LoadQuestion();
            }
            else
            {
                DialogResult result = MessageBox.Show($"Test bitdi! Topladiginiz bal: {score}\nYeniden baslamaq isteyirsiniz?", "Netice", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    currentQuestion = 0;
                    score = 0;
                    LoadQuestion();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}