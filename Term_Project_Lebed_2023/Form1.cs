using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;

namespace Term_Project_Lebed_2023
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            acceptButton.Enabled = uploadFileRadio.Checked || deleteFileRadio.Checked || renameFileRadio.Checked;
            if (createFileRadio.Checked)
            {
                label1.Text = "Enter file name";
                textBox1.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string fileName = textBox1.Text;
            string pattern = @"^[a-zA-Z0-9_]+$";

            if (Regex.IsMatch(fileName, pattern))
            {
                acceptButton.Enabled = true;

            }
            else
            {
                acceptButton.Enabled = false;
            }
        }


        private void acceptButton_Click(object sender, EventArgs e)
        {
            WorkWithAdvertismentData dataProcessor = new WorkWithAdvertismentData();
            if (uploadFileRadio.Checked)
            {
                try
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "����� Excel (*.xlsx, *.xls)|*.xlsx;*.xls";
                    openFileDialog.Title = "�������� ���� Excel";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        
                        dataProcessor.excelFilePath = filePath;
                        textBox1.Text = "File successfully uploaded";
                        dataProcessor.addInfoToDataGrid(ref dataGridView1);

                        tabControl1.SelectedIndex = 1;
                    }
                }
                catch (Exception ex)
                {
                    // ��������� ������
                    MessageBox.Show("��������� ������ ��� ��������������: " + ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (createFileRadio.Checked)
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx"; // ������ ��� ���������� ������ � ������� Excel
                saveFileDialog.Title = "�������� ���� � ��� ����� Excel";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    dataProcessor.excelFileName = textBox1.Text;
                    // ����� ������� �������� �����, ��������� ��������� ���� � �����
                    dataProcessor.createExcelFile(filePath);
                    label1.Text = "File was successfully created";
                    dataProcessor.addInfoToEmptyFile(ref dataGridView1);
                    tabControl1.SelectedIndex = 1;
                }
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars(); // �������� ������ ������������ �������� ��� ����� �����

            if (invalidChars.Contains(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // �������� ���� ������������� �������, �� ����������� ������ Backspace
            }
        }
    }
}
