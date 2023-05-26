using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using System.Data;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace Term_Project_Lebed_2023
{
    public class WorkWithAdvertismentData
    {
        public string excelFilePath { get; set; }
        public string excelFileName { get; set; }

        public List<AdvertisementData> DeserializeFromExcel()
        {
            List<AdvertisementData> advertisementDataList = new List<AdvertisementData>();

            using (SpreadsheetDocument spreadsheetDoc = SpreadsheetDocument.Open(excelFilePath, false))
            {
                WorkbookPart workbookPart = spreadsheetDoc.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                foreach (Row row in sheetData.Elements<Row>())
                {
                    AdvertisementData advertisementData = new AdvertisementData();

                    Cell subjectNameCell = row.Elements<Cell>().ElementAt(0);
                    advertisementData.SubjectName = GetCellValue(subjectNameCell, workbookPart);

                    Cell contractAndDateCell = row.Elements<Cell>().ElementAt(1);
                    string contractAndDateValue = GetCellValue(contractAndDateCell, workbookPart);

                    Cell permitAndDateCell = row.Elements<Cell>().ElementAt(4);
                    string permitAndDateValue = GetCellValue(permitAndDateCell, workbookPart);
                    advertisementData.Permit = permitAndDateValue;

                    Cell contractt = row.Elements<Cell>().ElementAt(1);
                    string contracttValue = GetCellValue(contractt, workbookPart);
                    advertisementData.Contract = contracttValue;

                    // Обработка продолжения договора
                    string continuationValue;
                    if (TryExtractContinuation(contractAndDateValue, out continuationValue))
                    {
                        ParseContinuation(continuationValue, advertisementData);
                        contractAndDateValue = contractAndDateValue.Replace(continuationValue, string.Empty).Trim();
                    }

                    // Обработка номера и даты договора
                    ParseContract(contractAndDateValue, advertisementData);

                    // Обработка продолжения разрешения
                    string permitContinuationValue;
                    if (TryExtractContinuation(permitAndDateValue, out permitContinuationValue))
                    {
                        ParseContinuation(permitContinuationValue, advertisementData);
                        permitAndDateValue = permitAndDateValue.Replace(permitContinuationValue, string.Empty).Trim();
                    }

                    // Обработка номера и даты разрешения
                    ParsePermit(permitAndDateValue, advertisementData);

                    Cell advertisementTypeCell = row.Elements<Cell>().ElementAt(2);
                    advertisementData.AdvertisementType = GetCellValue(advertisementTypeCell, workbookPart);

                    Cell advertisementInfoCell = row.Elements<Cell>().ElementAt(3);
                    advertisementData.AdvertisementInfo = GetCellValue(advertisementInfoCell, workbookPart);

                    advertisementDataList.Add(advertisementData);
                }
            }

            return advertisementDataList;
        }

        private bool TryExtractContinuation(string value, out string continuationValue)
        {
            continuationValue = string.Empty;

            string pattern = @"Продовжено\s*(.+)";
            Match match = Regex.Match(value, pattern);
            if (match.Success)
            {
                continuationValue = match.Groups[1].Value.Trim();
                return true;
            }

            return false;
        }

        private void ParseContract(string contractValue, AdvertisementData advertisementData)
        {
            string pattern = @"(.+)\s+від\s+(.+)";
            Match match = Regex.Match(contractValue, pattern);
            if (match.Success)
            {
                string contractNumberValue = match.Groups[1].Value.Trim();
                advertisementData.ContractNumberValue = contractNumberValue;

                string contractNumberPattern = @"(\d+)(?:\-(\d+))?(?:\/(\d+))?";
                Match contractNumberMatch = Regex.Match(contractNumberValue, contractNumberPattern);
                if (contractNumberMatch.Success)
                {
                    if (int.TryParse(contractNumberMatch.Groups[1].Value.Trim(), out int contractNumber))
                    {
                        advertisementData.ContractNumber = contractNumber;
                    }

                    string subContractNumberValue = contractNumberMatch.Groups[2].Value.Trim();
                    if (!string.IsNullOrEmpty(subContractNumberValue) && int.TryParse(subContractNumberValue, out int subContractNumber))
                    {
                        advertisementData.ContractNumber = subContractNumber;
                    }

                    string yearValue = contractNumberMatch.Groups[3].Value.Trim();
                    if (!string.IsNullOrEmpty(yearValue) && int.TryParse(yearValue, out int year))
                    {
                        advertisementData.ContractDate = new DateTime(year, 1, 1);
                    }
                }

                string contractDateValue = match.Groups[2].Value.Trim();
                DateTime contractDate;
                if (DateTime.TryParse(contractDateValue, out contractDate))
                {
                    advertisementData.ContractDate = contractDate;
                }
            }
        }

        private void ParseContinuation(string continuationValue, AdvertisementData advertisementData)
        {
            string continuationNumberValue = "";
            string continuationDateValue = "";

            string pattern = @"(.+)\s+рік(?:\.|\s+)(.+)";
            Match match = Regex.Match(continuationValue, pattern);
            if (match.Success)
            {
                continuationNumberValue = match.Groups[2].Value.Trim();
                continuationDateValue = match.Groups[1].Value.Trim();
            }

            if (!string.IsNullOrEmpty(continuationNumberValue))
            {
                if (int.TryParse(continuationNumberValue, out int continuationNumber))
                {
                    advertisementData.ContinuationNumber = continuationNumber;
                }
            }

            if (!string.IsNullOrEmpty(continuationDateValue))
            {
                DateTime continuationDate;
                if (DateTime.TryParse(continuationDateValue, out continuationDate))
                {
                    advertisementData.ContinuationDate = continuationDate;
                }
            }
        }

        private void ParsePermit(string permitValue, AdvertisementData advertisementData)
        {
            string[] permitArray = permitValue.Split(new string[] { "від" }, StringSplitOptions.RemoveEmptyEntries);
            if (permitArray.Length == 2)
            {
                string permitNumberValue = permitArray[0].Trim();
                advertisementData.PermitNumberValue = permitNumberValue;
                if (int.TryParse(permitNumberValue, out int permitNumber))
                {
                    advertisementData.PermitNumber = permitNumber;
                }

                string permitDateValue = permitArray[1].Trim();
                if (DateTime.TryParse(permitDateValue, out DateTime permitDate))
                {
                    advertisementData.PermitDate = permitDate;
                }
            }
        }

        public void addInfoToEmptyFile(ref DataGridView dgv)
        {
            // Очистка DataGridView
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            // Создание колонок с заголовками
            dgv.Columns.Add("Column1", "Назва субєкта господарювання");
            dgv.Columns.Add("Column2", "№ договору, дата");
            dgv.Columns.Add("Column3", "Тип зовнішньої реклами");
            dgv.Columns.Add("Column4", "Інформація про рекламні засоби (адреса розміщення реклами)");
            dgv.Columns.Add("Column5", "№ дозволу, дата");
            dgv.Columns.Add("Column6", "Статус");
        }

        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            string cellValue = cell.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                SharedStringTablePart stringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                cellValue = stringTablePart.SharedStringTable.ChildElements[int.Parse(cellValue)].InnerText;
            }

            return cellValue;
        }

        public void addInfoToDataGrid(ref DataGridView dgv)
        {
            List<AdvertisementData> advertisementDataList = DeserializeFromExcel();

            // Создаем таблицу данных
            DataTable dataTable = new DataTable();

            // Добавляем столбцы в таблицу
            dataTable.Columns.Add("Назва субєкта господарювання");
            dataTable.Columns.Add("№ договору, дата");
            dataTable.Columns.Add("Тип зовнішньої реклами");
            dataTable.Columns.Add("Інформація про рекламні засоби (адреса розміщення реклами)");
            dataTable.Columns.Add("№ дозволу, дата");
            dataTable.Columns.Add("Статус");

            // Заполняем таблицу данными из списка advertisementDataList
            for (int i = 1; i < advertisementDataList.Count; i++)
            {
                AdvertisementData advertisementData = advertisementDataList[i];

                if (advertisementData == null)
                    continue;

                DataRow row = dataTable.NewRow();
                row["Назва субєкта господарювання"] = advertisementData.SubjectName;
                row["№ договору, дата"] = advertisementData.Contract;
                row["Тип зовнішньої реклами"] = advertisementData.AdvertisementType;
                row["Інформація про рекламні засоби (адреса розміщення реклами)"] = advertisementData.AdvertisementInfo;
                row["№ дозволу, дата"] = advertisementData.Permit;

                // Определяем значение статуса в зависимости от наличия продления
                if (advertisementData.ContinuationDate != DateTime.MinValue || 
                        advertisementData.Contract.Contains("Продовжено") || 
                        advertisementData.Contract.Contains("продовжено") || 
                        advertisementData.Contract.Contains(",") || 
                        advertisementData.Contract.Contains(";"))
                {
                    string statusValue = "продовжено ";
                    row["Статус"] = statusValue;
                }
                else
                {
                    row["Статус"] = "дійсний";
                }

                dataTable.Rows.Add(row);
            }

            // Устанавливаем источник данных для DataGridView
            dgv.DataSource = dataTable;

            // Устанавливаем заголовки столбцов
            dgv.Columns[0].HeaderText = "Назва суб'єкта господарювання";
            dgv.Columns[1].HeaderText = "№ договору, дата";
            dgv.Columns[2].HeaderText = "Тип зовнішньої реклами";
            dgv.Columns[3].HeaderText = "Інформація про рекламні засоби (адреса розміщення реклами)";
            dgv.Columns[4].HeaderText = "№ дозволу, дата";
            dgv.Columns[5].HeaderText = "Статус";

            // Выравниваем данные в столбце "Інформація про рекламні засоби (адреса розміщення реклами)"
            dgv.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void createExcelFile(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                package.SaveAs(new FileInfo(filePath));
            }

            MessageBox.Show("Excel-файл успешно создан.");
        }

        public void SerializeToExcel(List<AdvertisementData> advertisementDataList, string excelFilePath)
        {
            // Создание нового документа Excel
            using (ExcelPackage package = new ExcelPackage())
            {
                // Добавление листа в документ
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("AdvertisementData");

                // Запись заголовков столбцов
                worksheet.Cells[1, 1].Value = "Назва суб'єкта господарювання";
                worksheet.Cells[1, 2].Value = "№ договору, дата";
                worksheet.Cells[1, 3].Value = "Тип зовнішньої реклами";
                worksheet.Cells[1, 4].Value = "Інформація про рекламні засоби (адреса розміщення реклами)";
                worksheet.Cells[1, 5].Value = "№ дозволу, дата";

                // Запись данных в ячейки
                for (int i = 0; i < advertisementDataList.Count; i++)
                {
                    AdvertisementData advertisementData = advertisementDataList[i];
                    int row = i + 2;

                    worksheet.Cells[row, 1].Value = advertisementData.SubjectName;
                    worksheet.Cells[row, 2].Value = advertisementData.Contract;
                    worksheet.Cells[row, 3].Value = advertisementData.AdvertisementType;
                    worksheet.Cells[row, 4].Value = advertisementData.AdvertisementInfo;
                    worksheet.Cells[row, 5].Value = advertisementData.Permit;
                }

                // Сохранение документа в файл
                package.SaveAs(new FileInfo(excelFilePath));
            }
        }

        public void searchBySubject(ref DataGridView dgv1, ref DataGridView dgv2, ref DataTable dataTable2, string searchText)
        {
            foreach (DataGridViewColumn column in dgv1.Columns)
            {
                dataTable2.Columns.Add(column.Name, column.ValueType);
            }

            foreach (DataGridViewRow row in dgv1.Rows)
            {
                if (row.Cells["Назва субєкта господарювання"].Value != null && row.Cells["Назва субєкта господарювання"].Value.ToString().Contains(searchText))
                {
                    DataRow newRow = dataTable2.NewRow();
                    // Копируем данные из найденной строки в новую строку таблицы dataTable2
                    for (int i = 0; i < dgv1.Columns.Count; i++)
                    {
                        newRow[i] = row.Cells[dgv1.Columns[i].Name].Value;
                    }
                    dataTable2.Rows.Add(newRow); // Добавляем новую строку в dataTable2
                }
            }

            if (dataTable2.Rows.Count > 0)
            {
                dgv2.AutoGenerateColumns = true; // Включаем автоматическую генерацию столбцов
                dgv2.DataSource = dataTable2; // Устанавливаем источник данных для dataGridView2
            }
            else
            {
                dgv2.Columns.Clear();
                MessageBox.Show("Совпадения в свойстве 'Назва суб'єкта господарювання' не найдены.");
            }
        }

        public void searchByContract(ref DataGridView dgv1, ref DataGridView dgv2, ref DataTable dataTable2, string searchText)
        {
            foreach (DataGridViewColumn column in dgv1.Columns)
            {
                dataTable2.Columns.Add(column.Name, column.ValueType);
            }

            foreach (DataGridViewRow row in dgv1.Rows)
            {
                if (row.Cells["№ договору, дата"].Value != null && row.Cells["№ договору, дата"].Value.ToString().Contains(searchText))
                {
                    DataRow newRow = dataTable2.NewRow();
                    // Копируем данные из найденной строки в новую строку таблицы dataTable2
                    for (int i = 0; i < dgv1.Columns.Count; i++)
                    {
                        newRow[i] = row.Cells[dgv1.Columns[i].Name].Value;
                    }
                    dataTable2.Rows.Add(newRow); // Добавляем новую строку в dataTable2
                }
            }

            if (dataTable2.Rows.Count > 0)
            {
                dgv2.AutoGenerateColumns = true; // Включаем автоматическую генерацию столбцов
                dgv2.DataSource = dataTable2; // Устанавливаем источник данных для dataGridView2
            }
            else
            {
                dgv2.Columns.Clear();
                MessageBox.Show("Совпадения в свойстве '№ договору, дата' не найдены.");
            }
        }

        public void searchByType(ref DataGridView dgv1, ref DataGridView dgv2, ref DataTable dataTable2, string searchText)
        {
            foreach (DataGridViewColumn column in dgv1.Columns)
            {
                dataTable2.Columns.Add(column.Name, column.ValueType);
            }

            foreach (DataGridViewRow row in dgv1.Rows)
            {
                if (row.Cells["Тип зовнішньої реклами"].Value != null && row.Cells["Тип зовнішньої реклами"].Value.ToString().Contains(searchText))
                {
                    DataRow newRow = dataTable2.NewRow();
                    // Копируем данные из найденной строки в новую строку таблицы dataTable2
                    for (int i = 0; i < dgv1.Columns.Count; i++)
                    {
                        newRow[i] = row.Cells[dgv1.Columns[i].Name].Value;
                    }
                    dataTable2.Rows.Add(newRow); // Добавляем новую строку в dataTable2
                }
            }

            if (dataTable2.Rows.Count > 0)
            {
                dgv2.AutoGenerateColumns = true; // Включаем автоматическую генерацию столбцов
                dgv2.DataSource = dataTable2; // Устанавливаем источник данных для dataGridView2
            }
            else
            {
                dgv2.Columns.Clear();
                MessageBox.Show("Совпадения в свойстве 'Тип зовнішньої реклами' не найдены.");
            }
        }

        public void searchByInfo(ref DataGridView dgv1, ref DataGridView dgv2, ref DataTable dataTable2, string searchText)
        {
            foreach (DataGridViewColumn column in dgv1.Columns)
            {
                dataTable2.Columns.Add(column.Name, column.ValueType);
            }

            foreach (DataGridViewRow row in dgv1.Rows)
            {
                if (row.Cells["Інформація про рекламні засоби (адреса розміщення реклами)"].Value != null 
                    && row.Cells["Інформація про рекламні засоби (адреса розміщення реклами)"].Value.ToString().Contains(searchText))
                {
                    DataRow newRow = dataTable2.NewRow();
                    // Копируем данные из найденной строки в новую строку таблицы dataTable2
                    for (int i = 0; i < dgv1.Columns.Count; i++)
                    {
                        newRow[i] = row.Cells[dgv1.Columns[i].Name].Value;
                    }
                    dataTable2.Rows.Add(newRow); // Добавляем новую строку в dataTable2
                }
            }

            if (dataTable2.Rows.Count > 0)
            {
                dgv2.AutoGenerateColumns = true; // Включаем автоматическую генерацию столбцов
                dgv2.DataSource = dataTable2; // Устанавливаем источник данных для dataGridView2
            }
            else
            {
                dgv2.Columns.Clear();
                MessageBox.Show("Совпадения в свойстве 'Інформація про рекламні засоби (адреса розміщення реклами)' не найдены.");
            }
        }

        public void searchByPermit(ref DataGridView dgv1, ref DataGridView dgv2, ref DataTable dataTable2, string searchText)
        {
            foreach (DataGridViewColumn column in dgv1.Columns)
            {
                dataTable2.Columns.Add(column.Name, column.ValueType);
            }

            foreach (DataGridViewRow row in dgv1.Rows)
            {
                if (row.Cells["№ дозволу, дата"].Value != null
                    && row.Cells["№ дозволу, дата"].Value.ToString().Contains(searchText))
                {
                    DataRow newRow = dataTable2.NewRow();
                    // Копируем данные из найденной строки в новую строку таблицы dataTable2
                    for (int i = 0; i < dgv1.Columns.Count; i++)
                    {
                        newRow[i] = row.Cells[dgv1.Columns[i].Name].Value;
                    }
                    dataTable2.Rows.Add(newRow); // Добавляем новую строку в dataTable2
                }
            }

            if (dataTable2.Rows.Count > 0)
            {
                dgv2.AutoGenerateColumns = true; // Включаем автоматическую генерацию столбцов
                dgv2.DataSource = dataTable2; // Устанавливаем источник данных для dataGridView2
            }
            else
            {
                dgv2.Columns.Clear();
                MessageBox.Show("Совпадения в свойстве '№ дозволу, дата' не найдены.");
            }
        }
    }

}
