using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Term_Project_Lebed_2023
{
    public partial class Form1 : Form
    {
        private DataTable originalTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = string.Empty;
            label1.Text = "Info text";
            acceptButton.Enabled = uploadFileRadio.Checked || deleteFileRadio.Checked || renameFileRadio.Checked;
            if (createFileRadio.Checked)
            {
                label1.Text = "Enter file name";
                textBox1.Enabled = true;
            }
            if (renameFileRadio.Checked) { label1.Text = "Enter new file's name"; textBox1.Enabled = true; }
        }

        private void enableSearch_button(object sender, EventArgs e)
        {
            searchTextBox.Enabled = false;
            searchTextBox.Text = string.Empty;
            if (searchBySubject_rButton.Checked || searchByContract_rButton.Checked
                || searchByType_rButton.Checked || searchByInfo_rButton.Checked || searchByPermit_rButton.Checked)
            {
                searchTextBox.Enabled = true;
            }

        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                searchByParam_button.Enabled = true;
            }
            else
            {
                searchByParam_button.Enabled = false;
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
                        originalTable = dataGridView1.DataSource as DataTable;
                        dataGridView1.DataSource = originalTable.Copy();
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
            if (deleteFileRadio.Checked)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xlsx";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string excelFilePath = openFileDialog.FileName;

                    try
                    {
                        // �������� ����� Excel
                        File.Delete(excelFilePath);
                        MessageBox.Show("���� ������� ������.", "�������� �����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"������ ��� �������� �����: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (renameFileRadio.Checked)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        string newFileName = textBox1.Text.Trim();

                        try
                        {
                            RenameAndSaveExcelFile(selectedFilePath, newFileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"������ ��� �������������� �����: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("������� ����� ��� �����.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void addRow_Button_Click(object sender, EventArgs e)
        {
            // ��������� ������ �� �������� ������
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            if (dataTable == null)
            {
                // �������������� dataTable
                dataTable = new DataTable();

            }

            // �������� ����� ������
            DataRow newRow = dataTable.NewRow();

            // ���������� ����� ������ � DataTable
            dataTable.Rows.Add(newRow);

            // ���������� ��������� ������ DataGridView
            dataGridView1.DataSource = dataTable;

            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
        }

        private void deleteRow_Button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;

                if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
                {
                    // ��������, �������� �� ��������� ������ ����� ���������������� �������
                    if (dataGridView1.Rows[rowIndex].IsNewRow)
                    {
                        // ������ ���������� ����� ������
                        dataGridView1.CancelEdit();
                    }

                    // �������� ������ �� DataGridView
                    if (!dataGridView1.Rows[rowIndex].IsNewRow)
                    {
                        dataGridView1.Rows.RemoveAt(rowIndex);
                    }
                }
            }
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            // �������� ������ ��� �������� ���� ������ �� DataGridView
            List<AdvertisementData> advertisementDataList = new List<AdvertisementData>();

            // ������� ���� ����� DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // �������� ���������� ������ AdvertisementData
                AdvertisementData advertisementData = new AdvertisementData();

                // ������������ �������� ����� ������� AdvertisementData �� ����� ������
                advertisementData.SubjectName = row.Cells[0].Value?.ToString();
                advertisementData.Contract = row.Cells[1].Value?.ToString();
                advertisementData.AdvertisementType = row.Cells[2].Value?.ToString();
                advertisementData.AdvertisementInfo = row.Cells[3].Value?.ToString();
                advertisementData.Permit = row.Cells[4].Value?.ToString();

                // ���������� ������� AdvertisementData � ������
                advertisementDataList.Add(advertisementData);
            }

            WorkWithAdvertismentData work = new WorkWithAdvertismentData();
            // ����� ������ ������������ � Excel � ������ ���� ����������
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string excelFilePath = saveFileDialog.FileName;
                work.SerializeToExcel(advertisementDataList, excelFilePath);
            }
            originalTable = dataGridView1.DataSource as DataTable;
            dataGridView1.DataSource = originalTable.Copy();
        }

        private void RenameAndSaveExcelFile(string filePath, string newFileName)
        {
            // ��������� ���������� ��������� �����
            string fileExtension = Path.GetExtension(filePath);

            // ��������, �������� �� ��������� ���� ���������� Excel
            if (fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || fileExtension.Equals(".xls", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    // ������������ ������ ���� � ����� ����� � ����������� ��������� ����������
                    string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName + fileExtension);

                    // �������������� �����
                    File.Move(filePath, newFilePath);

                    // ���������� ����� Excel � ����� ������ � ��������
                    // ��������� ���� ���� ���� � ������������ � ����� ������� ���������� ����� Excel
                    // ��������, ���� �� ����������� ���������� OpenXML ��� ������ � Excel, ����� ����� �������� ��� ���������� �����

                    // ����� ��������� �� �������� ����������
                    MessageBox.Show("���� ������� ������������ � �������� ��� �������� Excel.");
                }
                catch (Exception ex)
                {
                    // ��������� ����������, ���� ��������� ������ ��� �������������� ��� ���������� �����
                    MessageBox.Show("��������� ������ ��� �������������� � ���������� �����: " + ex.Message);
                }
            }
            else
            {
                // ���� ��������� ���� �� �������� ���������� Excel, ��������� ���������
                MessageBox.Show("��������� ���� �� �������� ���������� Excel.");
            }
        }

        private void cancelEdit_Button_Click(object sender, EventArgs e)
        {
            if (originalTable != null)
            {
                // ������� DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();

                // ��������������� �������� ������ � DataGridView

                dataGridView1.DataSource = originalTable.Copy();
                // ������������� ����� �� ��������� ��� �����, ���� ���������
                // dataGridView1.DefaultCellStyle = new DataGridViewCellStyle();

                MessageBox.Show("��������� ��������.", "������ ���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("��� ����������� �������� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchByParam_button_Click(object sender, EventArgs e)
        {
            WorkWithAdvertismentData data = new WorkWithAdvertismentData();
            string searchText = searchTextBox.Text; // �������� ����� ��� ������
            DataTable dataTable2 = new DataTable(); // ������� ����� ������ DataTable

            if (searchBySubject_rButton.Checked)
            {
                data.searchBySubject(ref dataGridView1, ref dataGridView2, ref dataTable2, searchText);
            }
            if(searchByContract_rButton.Checked)
            {
                data.searchByContract(ref dataGridView1, ref dataGridView2, ref dataTable2, searchText);
            }
            if(searchByType_rButton.Checked)
            {
                data.searchByType(ref dataGridView1, ref dataGridView2, ref dataTable2, searchText);
            }
            if (searchByInfo_rButton.Checked)
            {
                data.searchByInfo(ref dataGridView1, ref dataGridView2, ref dataTable2, searchText);    
            }
            if(searchByPermit_rButton.Checked)
            {
                data.searchByPermit(ref dataGridView1, ref dataGridView2, ref dataTable2, searchText);
            }
        }





    }
}
