using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
using ActiveDs;
using ClosedXML.Excel;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Collections;
using System.Globalization;

namespace GUI_SecChecker_v2
{
    public partial class Form1 : Form
    {

        string currPath = Environment.CurrentDirectory;
        string [] listDomain;

        ///////////////////////////////////Переменные для Исходные Данные/////////////////
        DataTable tblWithADReport;
        DataTable tblWithMPReport;
        DataTable tblWithKSCReport;
        DataTable tblWithSEPReport;
        DataTable tblWithSCCMReport;

        TimeSpan daySpan30 = new TimeSpan(30, 0, 0, 0);

        string dateFormatForAD = "yyyy.MM.dd HH.mm";

        string dateFormatForKSC = "dd.MM.yyyy";

        string dateFormatForSEP = "MM/dd/yyyy";

        string dateFormatForSCCM = "dd.MM.yyyy";

        ///////////////////////////////////Переменые для Обработанных Данных/////////////////
        DataTable tblWithCleanMPReport;
        DataTable tblWithCleanADReport;
        DataTable tblWithCleanKSCReport;
        DataTable tblWithCleanSEPReport;
        DataTable tblWithCleanSCCMReport;



        public Form1()
        {
            InitializeComponent();
        }



        ////////////////////////////////////////////////////////////////////////////////Чтение данных из AD\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// Получение данных из нескольких доменов
        /// </summary>
        private DataTable GetComputersFromMultipleDomains(string[] _listDomain)
        {

            DataTable adComp = new DataTable();
            DataTable _tblWithCompAD = new DataTable();

            for (int i = 0; i < _listDomain.Length; i++)
            {
                adComp = new DataTable();
                adComp = GetComputers(_listDomain[i], tb_login.Text, tb_pass.Text);
                _tblWithCompAD.Merge(adComp);
            }

            return _tblWithCompAD;
        }


        /// <summary>
        /// Получить ПК из AD в DataTable
        /// </summary>
        public DataTable GetComputers(string domain, string login, string pass)
        {
            DataTable tblWithADComp = new DataTable();

            List<string> ComputerNames = new List<string>();

            DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain, login, pass);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            mySearcher.Filter = ("(objectClass=computer)");
            mySearcher.SizeLimit = int.MaxValue;
            mySearcher.PageSize = int.MaxValue;


            tblWithADComp.Clear();
            tblWithADComp.Columns.Add("Name");
            tblWithADComp.Columns.Add("Description");
            tblWithADComp.Columns.Add("OperatingSystem");
            tblWithADComp.Columns.Add("DistinguishedName");
            tblWithADComp.Columns.Add("LastLogonTimeStamp");
            tblWithADComp.Columns.Add("Disabled");



            foreach (SearchResult resEnt in mySearcher.FindAll())
            {
                //"CN=SGSVG007DC"
                //string ComputerName = resEnt.GetDirectoryEntry().Name;
                //string ComputerName = resEnt.Properties["lastlogontimestamp"][0].ToString();
                ////Int64 lastLogonThisServer = new Int64();
                ////IADsLargeInteger lgInt =
                ////(IADsLargeInteger)resEnt.Properties["lastlogontimestamp"][0]; 
                ////lastLogonThisServer = ((long)lgInt.HighPart << 32) + lgInt.LowPart;

                //ComputerName = DateTime.FromFileTime(Convert.ToInt64(resEnt.Properties["lastlogontimestamp"][0])).ToString("yyyy.MM.dd.HH.mm.ss");
                //ComputerName = DateTime.FromFileTime(Convert.ToInt64(resEnt.Properties["lastlogontimestamp"][0])).ToShortDateString();

                //if (ComputerName.StartsWith("CN="))
                //    ComputerName = ComputerName.Remove(0, "CN=".Length);
                //ComputerNames.Add(ComputerName);


                DataRow _rowComp = tblWithADComp.NewRow();
                try
                {
                    _rowComp["Name"] = resEnt.Properties["Name"][0].ToString();
                }
                catch { _rowComp["Name"] = "null"; }

                try
                {
                    _rowComp["Description"] = resEnt.Properties["Description"][0].ToString();
                }
                catch { _rowComp["Description"] = "null"; }

                try
                {
                    _rowComp["OperatingSystem"] = resEnt.Properties["OperatingSystem"][0].ToString();
                }
                catch { _rowComp["OperatingSystem"] = "null"; }

                try
                {
                    _rowComp["DistinguishedName"] = resEnt.Properties["DistinguishedName"][0].ToString();
                }
                catch { _rowComp["DistinguishedName"] = "null"; }

                try
                {
                    _rowComp["LastLogonTimeStamp"] = DateTime.FromFileTime(Convert.ToInt64(resEnt.Properties["LastLogonTimeStamp"][0])).ToString("yyyy.MM.dd HH.mm");
                }
                catch { _rowComp["LastLogonTimeStamp"] = "null"; }

                try
                {
                    int userAccountControl = Convert.ToInt32(resEnt.Properties["userAccountControl"][0]);
                    bool disabled = ((userAccountControl & 2) > 0);
                    _rowComp["Disabled"] = disabled.ToString();
                }
                catch { _rowComp["Disabled"] = "null"; }

                tblWithADComp.Rows.Add(_rowComp);


            }

            mySearcher.Dispose();
            entry.Dispose();

            return tblWithADComp;
        }



        ////////////////////////////////////////////////////////////////////////////////Чтение и обработка CSV\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// Чтение CSV Отчета с заголовками в DataTable
        /// </summary>
        public DataTable ReadCSVWithHeadersToDataTable(string CSVWithHeadersFilepath, char delimiter)
        {
            DataTable tblCsvKSCReport = new DataTable();
            using (CsvReader csv =
                       new CsvReader(new StreamReader(CSVWithHeadersFilepath), true, delimiter))
            {
                //string[] headers = csv.GetFieldHeaders();
                 

                //tblCsvKSCReport.Clear();
                //tblCsvKSCReport.Columns.Add("MP_IP1");
                //tblCsvKSCReport.Columns.Add("MP_IP2");
                //tblCsvKSCReport.Columns.Add("MP_Name");
                //tblCsvKSCReport.Columns.Add("MP_NameFull");
                //tblCsvKSCReport.Columns.Add("MP_OS");
                csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;
                tblCsvKSCReport.Load(csv);

                
            }

            return tblCsvKSCReport;
        }

        /// <summary>
        /// Чтение CSV Отчета MP в DataTable
        /// </summary>
        public DataTable ReadMPReportToDataTable(string mpReportFilepath)
        {
            DataTable tblCsvMPReport = new DataTable();
            using (CsvReader csv =
                       new CsvReader(new StreamReader(mpReportFilepath), false, ';'))
            {
                string[] headers = csv.GetFieldHeaders();


                tblCsvMPReport.Clear();
                tblCsvMPReport.Columns.Add("MP_IP1");
                tblCsvMPReport.Columns.Add("MP_IP2");
                tblCsvMPReport.Columns.Add("MP_Name");
                tblCsvMPReport.Columns.Add("MP_NameFull");
                tblCsvMPReport.Columns.Add("MP_OS");



                while (csv.ReadNextRecord())
                {
                    DataRow _rowCsv = tblCsvMPReport.NewRow();
                    try
                    {
                        _rowCsv["MP_IP1"] = csv[0];
                    }
                    catch
                    {
                        _rowCsv["MP_IP1"] = "null";
                    }

                    try
                    {
                        _rowCsv["MP_IP2"] = csv[1];
                    }
                    catch
                    {
                        _rowCsv["MP_IP2"] = "null";
                    }
                    try
                    {
                        _rowCsv["MP_Name"] = csv[2];
                    }
                    catch
                    {
                        _rowCsv["MP_Name"] = "null";
                    }

                    try
                    {
                        _rowCsv["MP_NameFull"] = csv[3];
                    }
                    catch
                    {
                        _rowCsv["MP_NameFull"] = "null";
                    }

                    try
                    {
                        _rowCsv["MP_OS"] = csv[4];
                    }
                    catch
                    {
                        _rowCsv["MP_OS"] = "null";
                    }
                    tblCsvMPReport.Rows.Add(_rowCsv);


                }
            }

            return tblCsvMPReport;
        }

        /// <summary>
        /// Объединение всех CSV в указанной папке в один файл с именем папки
        /// </summary>
        public string MergeCSVInFolder(string pathToCSV)
        {
            string mergeFilePath = pathToCSV + "\\" + pathToCSV.Substring(pathToCSV.LastIndexOf(@"\") + 1) + ".csv";
            string mergeFileName = pathToCSV.Substring(pathToCSV.LastIndexOf(@"\") + 1) + ".csv";

            var dirInfo = new DirectoryInfo(pathToCSV);

            foreach (var file in dirInfo.EnumerateFiles("*.csv", SearchOption.TopDirectoryOnly))
            {
                var fileLines = File.ReadAllLines(file.FullName, Encoding.GetEncoding("UTF-8"));
                //var fileLines = File.ReadAllLines(file.FullName);
                File.AppendAllLines(mergeFilePath, fileLines);
            }

            return mergeFilePath;
        }







        ////////////////////////////////////////////////////////////////////////////////ОБРАБОТКА DataTable\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        ////////////////////////////////////////////////////////////////////////////////ОБРАБОТКА DataTable Общие Методы\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\    

        /// <summary>
        /// Удаление дубликатов из DataTable
        /// </summary>
        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        /// <summary>
        /// Удаление строк из DataTable содержащих в определенном столбце определенное слово 
        /// </summary>
        public DataTable RemoveRowsContainsSpecificWordInColumn(DataTable dt, string colName, string searchWord)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][colName].ToString().Contains(searchWord))
                {
                    dt.Rows[i].Delete();
                }

            }
            dt.AcceptChanges();

            return dt;
        }
        /// <summary>
        /// Удаление строк из DataTable равнающихся определенной строке в определенном столбце
        /// </summary>
        public DataTable RemoveRowsEqualsSpecificWordInColumn(DataTable dt, string colName, string searchWord)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i][colName].ToString() == searchWord)
                {
                    dt.Rows[i].Delete();
                }

            }
            dt.AcceptChanges();

            return dt;
        }


        /// <summary>
        /// Удаление записей с датой больше чем daySpan
        /// </summary>
        public DataTable RemoveRowsWithDateOldestTimeSpan(DataTable dt, string colName, TimeSpan daySpan, string dateFormat)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DateTime debugDT = new DateTime();
                if (DateTime.TryParseExact(dt.Rows[i][colName].ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out debugDT))
                {
                    if (DateTime.Now - DateTime.ParseExact(dt.Rows[i][colName].ToString(), dateFormat, CultureInfo.InvariantCulture) > daySpan)                    
                    {
                        dt.Rows[i].Delete();
                    }
                }

                int spaceIndex = (dt.Rows[i][colName].ToString().IndexOf(' '));
                if (spaceIndex > 0)
                {
                    if (DateTime.TryParseExact(dt.Rows[i][colName].ToString().Remove(spaceIndex), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out debugDT))
                    {

                        if (DateTime.Now - DateTime.ParseExact(dt.Rows[i][colName].ToString().Remove(spaceIndex), dateFormat, CultureInfo.InvariantCulture) > daySpan)
                        {
                            dt.Rows[i].Delete();
                        }
                    }
                }
               

               
                

            }
            dt.AcceptChanges();

            return dt;
        }


        ////////////////////////////////////////////////////////////////////////ОБРАБОТКА DataTable MP\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        /// <summary>
        /// Удаление строк с пустыми именами из отчета MP
        /// </summary>
        public DataTable RemoveRowsWithEmptyNameFromMPReport(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MP_Name"].ToString() == "null" && dt.Rows[i]["MP_NameFull"].ToString() == "null")
                {
                    dt.Rows[i].Delete();
                }
                if (dt.Rows[i]["MP_Name"].ToString() == "" && dt.Rows[i]["MP_NameFull"].ToString() == "")
                {
                    dt.Rows[i].Delete();
                }

            }
            dt.AcceptChanges();

            return dt;
        }

    
        /// <summary>
        /// Удаление дубликатов и строк с пустыми именами  и с доменом omega из отчета MP
        /// </summary>
        private DataTable RemoveDuplicateAndRowsWithEmptyNameFromMPReport()
        {
            DataTable _tblWithCleanMPReport = new DataTable();
            _tblWithCleanMPReport = tblWithMPReport.Copy();
            _tblWithCleanMPReport = RemoveRowsWithEmptyNameFromMPReport(_tblWithCleanMPReport);
            _tblWithCleanMPReport = RemoveDuplicateRows(_tblWithCleanMPReport, "MP_Name");
            _tblWithCleanMPReport = RemoveDuplicateRows(_tblWithCleanMPReport, "MP_NameFull");
            _tblWithCleanMPReport = RemoveRowsContainsSpecificWordInColumn(_tblWithCleanMPReport, "MP_NameFull", "omega");
            return _tblWithCleanMPReport;
        }

        ////////////////////////////////////////////////////////////////////////////////ОБРАБОТКА DataTable AD\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


        /// <summary>
        /// Удаление дубликтов, Disable, LastLogon из AD
        /// </summary>
        private DataTable RemoveDuplicateAndDisableAndOldLastLogonFromADReport()
        {
            DataTable _tblWithCleanADReport = new DataTable();
            _tblWithCleanADReport = tblWithADReport.Copy();
            _tblWithCleanADReport = RemoveDuplicateRows(_tblWithCleanADReport, "name");
            _tblWithCleanADReport = RemoveRowsContainsSpecificWordInColumn(_tblWithCleanADReport, "Disabled", "True");            
            _tblWithCleanADReport = RemoveRowsWithDateOldestTimeSpan(_tblWithCleanADReport, "LastLogonTimeStamp", daySpan30, dateFormatForAD);


            return _tblWithCleanADReport;            
        }

        ////////////////////////////////////////////////////////////////////////////////ОБРАБОТКА DataTable KSC\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


        /// <summary>
        /// Удаление без IP, дата соединения > 30, дубликатов из KSC
        /// </summary>
        private DataTable RemoveDuplicateAndNoIPAndOldLastConnectionFromKSCReport()
        {
            DataTable _tblWithCleanKSCReport = new DataTable();
            _tblWithCleanKSCReport = tblWithKSCReport.Copy();
            _tblWithCleanKSCReport = RemoveDuplicateRows(_tblWithCleanKSCReport, "Имя");
            _tblWithCleanKSCReport = RemoveRowsEqualsSpecificWordInColumn(_tblWithCleanKSCReport, "IP-адрес", "");
            _tblWithCleanKSCReport = RemoveRowsEqualsSpecificWordInColumn(_tblWithCleanKSCReport, "Соединение с Сервером", "");
            _tblWithCleanKSCReport = RemoveRowsWithDateOldestTimeSpan(_tblWithCleanKSCReport, "Соединение с Сервером", daySpan30, dateFormatForKSC);


            return _tblWithCleanKSCReport;
        }
        
        ////////////////////////////////////////////////////////////////////////////////ОБРАБОТКА DataTable SEP\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private DataTable RemoveDuplicateAndOldLastConnectionFromSEPReport()
        {
            DataTable _tblWithCleanSEPReport = new DataTable();
            _tblWithCleanSEPReport = tblWithSEPReport.Copy();
            try
            {
                _tblWithCleanSEPReport = RemoveDuplicateRows(_tblWithCleanSEPReport, "Computer Name");
            }
            catch
            {

            }
            try
            {
                _tblWithCleanSEPReport = RemoveDuplicateRows(_tblWithCleanSEPReport, "Имя компьютера");
            }
            catch
            {

            }
            try
            {
                _tblWithCleanSEPReport = RemoveRowsWithDateOldestTimeSpan(_tblWithCleanSEPReport, "Время последнего изменения состояния", daySpan30, dateFormatForSEP);
            }
            catch
            {

            }
            try
            {
                _tblWithCleanSEPReport = RemoveRowsWithDateOldestTimeSpan(_tblWithCleanSEPReport, "Last time status changed", daySpan30, dateFormatForSEP);
            }
            catch
            {

            }


            return _tblWithCleanSEPReport;
        }

        ////////////////////////////////////////////////////////////////////////////////ОБРАБОТКА DataTable SCCM\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        private DataTable RemoveDuplicateAndOldLastConnectionFromSCCMReport()
        {

            DataTable _tblWithCleanSCCMReport = new DataTable();
            _tblWithCleanSCCMReport = tblWithSCCMReport.Copy();
            _tblWithCleanSCCMReport = RemoveDuplicateRows(_tblWithCleanSCCMReport, "Name0");
            _tblWithCleanSCCMReport = RemoveRowsWithDateOldestTimeSpan(_tblWithCleanSCCMReport, "LastLogon", daySpan30, dateFormatForSCCM);
            
            return _tblWithCleanSCCMReport;
        }


        ////////////////////////////////////////////////////////////////////////////////ОБРАБОТКА КНОПОК\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

        //////// Кнопка Получить данные из AD
        private void bt_GetDataAD_Click(object sender, EventArgs e)
        {
            listDomain = tb_domain.Text.Split(';');
            tblWithADReport = GetComputersFromMultipleDomains(listDomain);

            MessageBox.Show("AD Done!");

        }

        //////// Кнопка Тест удаления мусора из отчета AD
        private void bt_RemoveTrashFromAD_Click(object sender, EventArgs e)
        {
            tblWithCleanADReport = RemoveDuplicateAndDisableAndOldLastLogonFromADReport().Copy();
        }


        //////// Кнопка Указать путь к отчетам MP
        private void bt_BrowseMPReport_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            tb_PathMPReport.Text = dlg.SelectedPath;

        }

        //////// Кнопка Прочитать данные из отчета MP
        private void bt_ReadMPReport_Click(object sender, EventArgs e)
        {

            tblWithMPReport = new DataTable();
            tblWithMPReport = ReadMPReportToDataTable(MergeCSVInFolder(tb_PathMPReport.Text));

            MessageBox.Show("MP Done!");
        }

        //////// Кнопка Тест удаления мусора из отчета MP
        private void bt_RemoveTrashFromMP_Click(object sender, EventArgs e)
        {
            tblWithCleanMPReport = RemoveDuplicateAndRowsWithEmptyNameFromMPReport().Copy();
        }



        //////// Кнопка Указать путь к отчетам KSC
        private void bt_BrowseKSCReport_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            tb_PathKSCReport.Text = dlg.SelectedPath;

        }

        //////// Кнопка Прочитать данные из отчета KSC
        private void bt_ReadKSCReport_Click(object sender, EventArgs e)
        {
            tblWithKSCReport = new DataTable();
            tblWithKSCReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(tb_PathKSCReport.Text), '\t');

            MessageBox.Show("KSC Done!");
        }
        
        //////// Кнопка Тест удаления мусора из отчета KSC
        private void bt_RemoveTrashFromKSC_Click(object sender, EventArgs e)
        {
            tblWithCleanKSCReport = RemoveDuplicateAndNoIPAndOldLastConnectionFromKSCReport().Copy();
        }

        //////// Кнопка Тест отобразить результаты таблици tblWithKSCReport
        private void bt_DisplayKSC_Click(object sender, EventArgs e)
        {
            dgv_ksc.DataSource = tblWithKSCReport;
            
        }



        //////// Кнопка Указать путь к отчетам SEP
        private void bt_BrowseSEPReport_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            tb_PathSEPReport.Text = dlg.SelectedPath;
        }

        //////// Кнопка Прочитать данные из отчета SEP
        private void bt_ReadSEPReport_Click(object sender, EventArgs e)
        {
            tblWithSEPReport = new DataTable();
            tblWithSEPReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(tb_PathSEPReport.Text), ',');

            MessageBox.Show("SEP Done!");
        }

        //////// Кнопка Тест удаления мусора из отчета SEP
        private void bt_RemoveTrashFromSEP_Click(object sender, EventArgs e)
        {
            tblWithCleanSEPReport = RemoveDuplicateAndOldLastConnectionFromSCCMReport().Copy();
        }



        //////// Кнопка Указать путь к отчетам SCCM
        private void bt_BrowseSCCMReport_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            tb_PathSCCMReport.Text = dlg.SelectedPath;
        }

        //////// Кнопка Прочитать данные из отчета SCCM
        private void bt_ReadSCCMReport_Click(object sender, EventArgs e)
        {
            tblWithSCCMReport = new DataTable();
            tblWithSCCMReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(tb_PathSCCMReport.Text), ',');

            MessageBox.Show("SCCM Done!");
        }

        //////// Кнопка Тест удаления мусора из отчета SCCM
        private void bt_RemoveTrashFromSCCM_Click(object sender, EventArgs e)
        {
            tblWithCleanSCCMReport = RemoveDuplicateAndOldLastConnectionFromSCCMReport().Copy();
        }

    }
}
