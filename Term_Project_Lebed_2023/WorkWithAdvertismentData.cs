using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using OfficeOpenXml;

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

                    // Удаление ненужных символов
                    contractAndDateValue = contractAndDateValue.Replace("р.", "").Replace("рік", "").Replace("продовжено", "").Trim();

                    string[] contractAndDateArray = contractAndDateValue.Split(new string[] { "від" }, StringSplitOptions.RemoveEmptyEntries);

                    if (contractAndDateArray.Length == 2)
                    {
                        // Обработка номера договора
                        string contractNumberValue = contractAndDateArray[0].Trim();
                        if (contractNumberValue.StartsWith("№"))
                        {
                            contractNumberValue = contractNumberValue.Substring(1);
                        }
                        int contractNumber;
                        if (int.TryParse(contractNumberValue, out contractNumber))
                        {
                            advertisementData.ContractNumber = contractNumber;
                        }

                        // Обработка даты договора
                        string contractDateValue = contractAndDateArray[1].Trim();
                        DateTime contractDate;
                        if (DateTime.TryParse(contractDateValue, out contractDate))
                        {
                            advertisementData.ContractDate = contractDate;
                        }
                    }

                    Cell advertisementTypeCell = row.Elements<Cell>().ElementAt(2);
                    advertisementData.AdvertisementType = GetCellValue(advertisementTypeCell, workbookPart);

                    Cell advertisementInfoCell = row.Elements<Cell>().ElementAt(3);
                    advertisementData.AdvertisementInfo = GetCellValue(advertisementInfoCell, workbookPart);

                    Cell permitAndDateCell = row.Elements<Cell>().ElementAt(4);
                    string permitAndDateValue = GetCellValue(permitAndDateCell, workbookPart);

                    // Удаление ненужных символов
                    permitAndDateValue = permitAndDateValue.Replace("р.", "").Replace("рік", "").Replace("продовжено", "").Trim();

                    string[] permitAndDateArray = permitAndDateValue.Split(new string[] { "від" }, StringSplitOptions.RemoveEmptyEntries);

                    if (permitAndDateArray.Length == 2)
                    {
                        // Обработка номера разрешения
                        string permitNumberValue = permitAndDateArray[0].Trim();
                        if (permitNumberValue.StartsWith("№"))
                        {
                            permitNumberValue = permitNumberValue.Substring(1);
                        }
                        int permitNumber;
                        if (int.TryParse(permitNumberValue, out permitNumber))
                        {
                            advertisementData.PermitNumber = permitNumber;
                        }

                        // Обработка даты разрешения
                        string permitDateValue = permitAndDateArray[1].Trim();
                        DateTime permitDate;
                        if (DateTime.TryParse(permitDateValue, out permitDate))
                        {
                            advertisementData.PermitDate = permitDate;
                        }
                    }

                    advertisementDataList.Add(advertisementData);
                }
            }

            return advertisementDataList;
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

            // Заполняем таблицу данными из списка advertisementDataList
            for (int i = 1; i < advertisementDataList.Count; i++)
            {
                AdvertisementData advertisementData = advertisementDataList[i];

                if (advertisementData == null)
                    continue;

                DataRow row = dataTable.NewRow();
                row["Назва субєкта господарювання"] = advertisementData.SubjectName;
                row["№ договору, дата"] = advertisementData.ContractNumber + " " + advertisementData.ContractDate;
                row["Тип зовнішньої реклами"] = advertisementData.AdvertisementType;
                row["Інформація про рекламні засоби (адреса розміщення реклами)"] = advertisementData.AdvertisementInfo;
                row["№ дозволу, дата"] = advertisementData.PermitNumber + " " + advertisementData.PermitDate;
                dataTable.Rows.Add(row);
            }

            // Устанавливаем источник данных для DataGridView
            dgv.DataSource = dataTable;

            // Устанавливаем заголовки столбцов
            dgv.Columns[0].HeaderText = "Назва субєкта господарювання";
            dgv.Columns[1].HeaderText = "№ договору, дата";
            dgv.Columns[2].HeaderText = "Тип зовнішньої реклами";
            dgv.Columns[3].HeaderText = "Інформація про рекламні засоби (адреса розміщення реклами)";
            dgv.Columns[4].HeaderText = "№ дозволу, дата";

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

        private void ExtractContractAndPermitNumbers(string text, out int contractNumber, out int permitNumber)
        {
            string[] parts = text.Split(' ');
            contractNumber = 0;
            permitNumber = 0;

            foreach (string part in parts)
            {
                if (part.StartsWith("№"))
                {
                    string numberText = part.Substring(1);
                    if (int.TryParse(numberText, out int number))
                    {
                        if (contractNumber == 0)
                            contractNumber = number;
                        else
                            permitNumber = number;
                    }
                }
            }
        }

        private DateTime ParseDate(string text)
        {
            DateTime date = DateTime.MinValue;

            // Check if the text contains "від"
            if (text.Contains("від"))
            {
                // Remove any unnecessary characters and trim the text
                string cleanedText = text.Replace("від", "").Trim();

                // Split the cleaned text into day, month, and year parts
                string[] dateParts = cleanedText.Split('.');

                if (dateParts.Length == 3)
                {
                    int day = int.Parse(dateParts[0]);
                    int month = int.Parse(dateParts[1]);
                    int year = int.Parse(dateParts[2]);

                    // Assuming the date format is DD.MM.YYYY
                    date = new DateTime(year, month, day);
                }
            }

            return date;
        }
    }
}
