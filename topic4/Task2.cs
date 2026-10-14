using System;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        private string currentFilePath = "";
        private bool isTextChanged = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
            lblCharCount.Text = "Simvol sayi: " + richTextBox1.Text.Length;
        }

        private bool PromptSaveIfChanged()
        {
            if (!isTextChanged) return true;

            DialogResult result = MessageBox.Show("Deyisiklikleri saxlamaq isteyirsiniz?", "Xeberdarliq", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                SaveFile();
                return true;
            }
            return result == DialogResult.No;
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Text Files (*.txt)|*.txt" };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                }
                else return;
            }

            File.WriteAllText(currentFilePath, richTextBox1.Text);
            isTextChanged = false;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PromptSaveIfChanged())
            {
                richTextBox1.Clear();
                currentFilePath = "";
                isTextChanged = false;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PromptSaveIfChanged())
            {
                OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Text Files (*.txt)|*.txt" };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = openFileDialog.FileName;
                    richTextBox1.Text = File.ReadAllText(currentFilePath);
                    isTextChanged = false;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) => SaveFile();

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PromptSaveIfChanged()) Application.Exit();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK) richTextBox1.Font = fontDialog.Font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) richTextBox1.ForeColor = colorDialog.Color;
        }
    }
}