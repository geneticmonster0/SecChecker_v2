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
using System.Threading;

namespace GUI_SecChecker_v2
{
    public partial class Form1 : Form
    {

        string currPath = Environment.CurrentDirectory;
        string tempPath = Environment.CurrentDirectory + "\\" + "Temp";
        string reportPath = Environment.CurrentDirectory + "\\" + "Report";
        string[] listDomain;

        //Делегаты для отрисовки интерфейса
        public delegate void delUpdateUITextBox(string text);

        
        ///////////////////////////////////Переменные для Исходные Данные/////////////////
        DataTable tblWithADReport;
        DataTable tblWithMPReport;
        DataTable tblWithKSCReport;
        DataTable tblWithSEPReport;
        DataTable tblWithSCCMReport;

        TimeSpan daySpan30 = new TimeSpan(30, 0, 0, 0);

        TimeSpan daySpan10 = new TimeSpan(10, 0, 0, 0);

        string dateFormatForAD = "yyyy.MM.dd HH.mm";

        string dateFormatForKSC = "dd.MM.yyyy";

        string dateFormatForSEP = "MM/dd/yyyy";

        string dateFormatForSEPBase = "yyyy-MM-dd";

        string dateFormatForSCCM = "dd.MM.yyyy";

        ///////////////////////////////////Переменые для Обработанных Данных/////////////////
        DataTable tblWithCleanMPReport;
        DataTable tblWithCleanADReport;
        DataTable tblWithCleanKSCReport;
        DataTable tblWithCleanSEPReport;
        DataTable tblWithCleanSCCMReport;


        ///////////////////////////////////Переменые для Отчетности/////////////////
        DataTable tblWithAllHost;
        DataTable tblWithHostNotInAD;
        DataTable tblWithHostWithoutKES;
        DataTable tblWithHostWithoutSEP;
        DataTable tblWithHostOldBaseKES;
        DataTable tblWithHostOldBaseSEP;
        DataTable tblWithHostOldClientKES;
        DataTable tblWithHostOldClientSEP;
        //DataTable tblWithHostWithoutSCCM;





        public Form1()
        {
            InitializeComponent();



            Load += new EventHandler(Form1_Load);

            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            // This event will be raised when we call ReportProgress
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        ////////////////////////////////////////////////////////////////////////////////Технические методы\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        /// <summary>
        /// Удаление всех файлов в папке
        /// </summary>
        public void DeleteFileInDir(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
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
            //string mergeFilePath = pathToCSV + "\\" + pathToCSV.Substring(pathToCSV.LastIndexOf(@"\") + 1) + ".csv";
            string mergeFilePath = tempPath + "\\" + pathToCSV.Substring(pathToCSV.LastIndexOf(@"\") + 1) + ".csv";
            //string mergeFileName = currPath + "\\" + pathToCSV.Substring(pathToCSV.LastIndexOf(@"\") + 1) + ".csv";

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
                else
                {
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




            }
            dt.AcceptChanges();

            return dt;
        }

        /// <summary>
        /// Удаление записей с датой меньше чем daySpan
        /// </summary>
        public DataTable RemoveRowsWithDateNewestTimeSpan(DataTable dt, string colName, TimeSpan daySpan, string dateFormat)
        {


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DateTime debugDT = new DateTime();
                if (DateTime.TryParseExact(dt.Rows[i][colName].ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out debugDT))
                {
                    if (DateTime.Now - DateTime.ParseExact(dt.Rows[i][colName].ToString(), dateFormat, CultureInfo.InvariantCulture) < daySpan)
                    {
                        dt.Rows[i].Delete();
                    }
                }
                else
                {
                    int spaceIndex = (dt.Rows[i][colName].ToString().IndexOf(' '));

                    if (spaceIndex > 0)
                    {
                        if (DateTime.TryParseExact(dt.Rows[i][colName].ToString().Remove(spaceIndex), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out debugDT))
                        {

                            if (DateTime.Now - DateTime.ParseExact(dt.Rows[i][colName].ToString().Remove(spaceIndex), dateFormat, CultureInfo.InvariantCulture) < daySpan)
                            {
                                dt.Rows[i].Delete();
                            }
                        }
                    }
                }




            }
            dt.AcceptChanges();

            return dt;
        }

        /// <summary>
        /// Left Outer Join A and B
        /// </summary>
        private DataTable GetLeftOuterJoin(DataTable tblA, string tblAID, DataTable tblB, string tblBID)
        {
            DataTable tblLeftOuterJoin = new DataTable();
            tblLeftOuterJoin = tblA.Clone();


            for (int i = 0; i < tblA.Rows.Count; i++)
            {
                bool thisRowContainsInTableB = false;

                for (int j = 0; j < tblB.Rows.Count; j++)
                {
                    //string strA = tblA.Rows[i]["MP_Name"].ToString();
                    //string strB = tblB.Rows[j]["name"].ToString();
                    if (tblA.Rows[i][tblAID].ToString().ToUpper() == tblB.Rows[j][tblBID].ToString().ToUpper())
                    {

                        thisRowContainsInTableB = true;
                        break;
                    }

                }


                if (!thisRowContainsInTableB)
                {
                    tblLeftOuterJoin.ImportRow(tblA.Rows[i]);
                }
            }

            return tblLeftOuterJoin;
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
            if (!chb_ADFromFile.Checked)
            {
                _tblWithCleanADReport = RemoveRowsContainsSpecificWordInColumn(_tblWithCleanADReport, "Disabled", "True");
            }
            else
            {
                _tblWithCleanADReport = RemoveRowsContainsSpecificWordInColumn(_tblWithCleanADReport, "Enabled", "False");
            }

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
            if (chb_ADFromFile.Checked)
            {
                tblWithADReport = new DataTable();
                tblWithADReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(tb_PathMPReport.Text), ';');


            }
            else
            {
                listDomain = tb_domain.Text.Split(';');
                tblWithADReport = new DataTable();
                tblWithADReport = GetComputersFromMultipleDomains(listDomain);
            }


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
            tblWithCleanSEPReport = RemoveDuplicateAndOldLastConnectionFromSEPReport().Copy();
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


        //////// Кнопка Тест Получение всех Хостов
        private void bt_GetAllHost_Click(object sender, EventArgs e)
        {
            lb_TimeStart.Text = DateTime.Now.ToShortTimeString();

            DateTime timeStart = DateTime.Now;
            DeleteFileInDir(tempPath);
            backgroundWorker1.RunWorkerAsync();

            

            
        }

        /// <summary>
        /// Метод помечает хосты без SCCM в таблице AllHost
        /// </summary>
        private void AddInfoAboutHostNotInSCCMToAllHostTable()
        {
            for (int i = 0; i < tblWithCleanSCCMReport.Rows.Count; i++)
            {
                string query = "name = " + "'" + tblWithCleanSCCMReport.Rows[i]["Name0"] + "'";
                DataRow[] row = tblWithAllHost.Select(query);
                if (row.Length != 0)
                {
                    row[0]["NotInSCCM"] = "True";
                }

            }
        }

        /// <summary>
        /// Получает чистый отчет SCCM в tblWithCleanSCCMReport
        /// </summary>
        private void GetTblWithCleanSCCMReport()
        {
            tblWithSCCMReport = new DataTable();
            tblWithSCCMReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(tb_PathSCCMReport.Text), ',');
            tblWithCleanSCCMReport = RemoveDuplicateAndOldLastConnectionFromSCCMReport().Copy();
        }

        /// <summary>
        /// Метод помечает хосты со старыми клиентами SEP в таблице AllHost
        /// </summary>
        private void AddInfoAboutHostOldClientSEPToAllHostTable()
        {
            for (int i = 0; i < tblWithHostOldClientSEP.Rows.Count; i++)
            {
                string query = "";

                try
                {
                    query = "name = " + "'" + tblWithHostOldClientSEP.Rows[i]["Computer Name"] + "'";
                }
                catch { }
                try
                {
                    query = "name = " + "'" + tblWithHostOldClientSEP.Rows[i]["Имя компьютера"] + "'";
                }
                catch { }
                DataRow[] row = tblWithAllHost.Select(query);
                if (row.Length != 0)
                {
                    row[0]["OldClientSEP"] = "True";
                }

            }
        }

        /// <summary>
        /// Получает таблицу tblWithHostOldBaseSEP со списком хостов со старыми клиентами SEP
        /// </summary>
        private void GetTblWithHostOldClientSEP()
        {
            tblWithHostOldClientSEP = new DataTable();
            tblWithHostOldClientSEP = tblWithAllHost.Clone();
            tblWithHostOldClientSEP = tblWithCleanSEPReport.Copy();
            try
            {
                tblWithHostOldClientSEP = RemoveRowsContainsSpecificWordInColumn(tblWithHostOldClientSEP, "Client Version", "12.");
            }
            catch { }
            try
            {
                tblWithHostOldClientSEP = RemoveRowsContainsSpecificWordInColumn(tblWithHostOldClientSEP, "Версия клиента", "12.");
            }
            catch { }
        }

        /// <summary>
        /// Метод помечает хосты со старыми клиентами KES в таблице AllHost
        /// </summary>
        private void AddInfoAboutHostOldClientKESToAllHostTable()
        {
            for (int i = 0; i < tblWithHostOldClientKES.Rows.Count; i++)
            {
                string query = "name = " + "'" + tblWithHostOldClientKES.Rows[i]["Имя"] + "'";
                DataRow[] row = tblWithAllHost.Select(query);
                if (row.Length != 0)
                {
                    row[0]["OldClientKES"] = "True";
                }

            }
        }

        /// <summary>
        /// Получает таблицу tblWithHostOldBaseKES со списком хостов со старыми клиентами KES
        /// </summary>
        private void GetTblWithHostOldClientKES()
        {
            tblWithHostOldClientKES = new DataTable();
            tblWithHostOldClientKES = tblWithAllHost.Clone();
            tblWithHostOldClientKES = tblWithCleanKSCReport.Copy();
            tblWithHostOldClientKES = RemoveRowsContainsSpecificWordInColumn(tblWithHostOldClientKES, "Версия защиты", "10.");
        }

        /// <summary>
        /// Метод помечает хосты со старыми базами SEP в таблице AllHost
        /// </summary>
        private void AddInfoAboutHostOldBaseSEPToAllHostTable()
        {
            for (int i = 0; i < tblWithHostOldBaseSEP.Rows.Count; i++)
            {
                string query = "";
                try
                { query = "name = " + "'" + tblWithHostOldBaseSEP.Rows[i]["Computer Name"] + "'"; }
                catch { }
                try
                { query = "name = " + "'" + tblWithHostOldBaseSEP.Rows[i]["Имя компьютера"] + "'"; }
                catch { }

                DataRow[] row = tblWithAllHost.Select(query);
                if (row.Length != 0)
                {
                    row[0]["OldBaseSEP"] = "True";
                }

            }
        }

        /// <summary>
        /// Получает таблицу tblWithHostOldBaseSEP со списком хостов со старыми базами SEP
        /// </summary>
        private void GetTblWithHostOldBaseSEP()
        {
            tblWithHostOldBaseSEP = new DataTable();
            tblWithHostOldBaseSEP = tblWithAllHost.Clone();
            tblWithHostOldBaseSEP = tblWithCleanSEPReport.Copy();
            try
            {
                tblWithHostOldBaseSEP = RemoveRowsWithDateNewestTimeSpan(tblWithHostOldBaseSEP, "Version", daySpan10, dateFormatForSEPBase);
            }
            catch { }
            try
            {
                tblWithHostOldBaseSEP = RemoveRowsWithDateNewestTimeSpan(tblWithHostOldBaseSEP, "Описания вирусов", daySpan10, dateFormatForSEPBase);
            }
            catch { }
        }

        /// <summary>
        /// Метод помечает хосты со старыми базами KES в таблице AllHost
        /// </summary>
        private void AddInfoAboutHostOldBaseKESToAllHostTable()
        {
            for (int i = 0; i < tblWithHostOldBaseKES.Rows.Count; i++)
            {
                string query = "name = " + "'" + tblWithHostOldBaseKES.Rows[i]["Имя"] + "'";
                DataRow[] row = tblWithAllHost.Select(query);
                if (row.Length != 0)
                {
                    row[0]["OldBaseKES"] = "True";
                }

            }
        }

        /// <summary>
        /// Получает таблицу tblWithHostOldBaseKES со списком хостов со старыми базами KES
        /// </summary>
        private void GetTblWithHostOldBaseKES()
        {
            tblWithHostOldBaseKES = new DataTable();
            tblWithHostOldBaseKES = tblWithAllHost.Clone();
            tblWithHostOldBaseKES = tblWithCleanKSCReport.Copy();
            tblWithHostOldBaseKES = RemoveRowsWithDateNewestTimeSpan(tblWithHostOldBaseKES, "Версия баз", daySpan10, dateFormatForKSC);
        }

        /// <summary>
        /// Метод помечает хосты без SEP в таблице AllHost
        /// </summary>
        private void AddInfoAboutHostNotInSEPToAllHostTable()
        {
            for (int i = 0; i < tblWithHostWithoutSEP.Rows.Count; i++)
            {
                string query = "name = " + "'" + tblWithHostWithoutSEP.Rows[i]["name"] + "'";
                DataRow[] row = tblWithAllHost.Select(query);
                row[0]["NotInSEP"] = "True";

            }
        }

        /// <summary>
        /// Метод записывает данные о хостах без SEP в таблицу tblWithHostWithoutSEP
        /// </summary>
        private void GetTblWithHostNotInSEP()
        {
            tblWithHostWithoutSEP = new DataTable();
            tblWithHostWithoutSEP = tblWithAllHost.Clone();

            try
            {
                tblWithHostWithoutSEP = GetLeftOuterJoin(tblWithAllHost, "name", tblWithCleanSEPReport, "Computer Name");
            }
            catch { }
            try
            {
                tblWithHostWithoutSEP = GetLeftOuterJoin(tblWithAllHost, "name", tblWithCleanSEPReport, "Имя компьютера");
            }
            catch { }
        }

        /// <summary>
        /// Добавлят чистые данные в tblWithCleanSEPReport из отчета SEP
        /// </summary>
        private void GetTblWithCleanSEPReport()
        {
            tblWithSEPReport = new DataTable();
            tblWithSEPReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(tb_PathSEPReport.Text), ',');
            tblWithCleanSEPReport = RemoveDuplicateAndOldLastConnectionFromSEPReport().Copy();
        }

        /// <summary>
        /// Метод помечает хосты без KES в таблице AllHost
        /// </summary>
        private void AddInfoAboutHostNotInKSCToAllHostTable()
        {
            for (int i = 0; i < tblWithHostWithoutKES.Rows.Count; i++)
            {
                string query = "name = " + "'" + tblWithHostWithoutKES.Rows[i]["name"] + "'";
                DataRow[] row = tblWithAllHost.Select(query);
                row[0]["NotInKSC"] = "True";

            }
        }

        /// <summary>
        /// Метод записывает данные о хостах без KES в таблицу tblWithHostWithoutKES
        /// </summary>
        private void GetTblWithHostNotInKSC()
        {
            tblWithHostWithoutKES = new DataTable();
            tblWithHostWithoutKES = tblWithAllHost.Clone();
            tblWithHostWithoutKES = GetLeftOuterJoin(tblWithAllHost, "name", tblWithCleanKSCReport, "Имя");
        }

        /// <summary>
        /// Добавлят чистые данные в tblWithCleanKSCReport из отчета KSC
        /// </summary>
        private void GetTblWithCleanKSCReport()
        {
            tblWithKSCReport = new DataTable();
            tblWithKSCReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(tb_PathKSCReport.Text), '\t');
            tblWithCleanKSCReport = RemoveDuplicateAndNoIPAndOldLastConnectionFromKSCReport().Copy();
        }

        /// <summary>
        /// Добавление в таблицу AllHost хостов не в AD
        /// </summary>
        private void AddHostNotInADToAllHostTable()
        {
            for (int i = 0; i < tblWithHostNotInAD.Rows.Count; i++)
            {
                DataRow rowWithNotInAD = tblWithAllHost.NewRow();
                rowWithNotInAD["name"] = tblWithHostNotInAD.Rows[i]["MP_Name"];
                rowWithNotInAD["description"] = tblWithHostNotInAD.Rows[i]["MP_IP1"];
                rowWithNotInAD["operatingSystem"] = tblWithHostNotInAD.Rows[i]["MP_OS"];
                rowWithNotInAD["DistinguishedName"] = "empty";
                rowWithNotInAD["LastLogonTimeStamp"] = "empty";
                if (chb_ADFromFile.Checked)
                {
                    rowWithNotInAD["Enabled"] = "empty";
                }
                else
                {
                    rowWithNotInAD["Disabled"] = "empty";
                }

                rowWithNotInAD["NotInAD"] = "True";

                tblWithAllHost.Rows.Add(rowWithNotInAD);
            }
        }

        /// <summary>
        /// Метод для создания таблицы для всех хостов (добавлены поля для пометки состояния АВПО, SCCM, AD)
        /// </summary>
        private void CreateTblForAllHost()
        {
            tblWithAllHost = new DataTable();
            tblWithAllHost = tblWithCleanADReport.Copy();

            tblWithAllHost.Columns.Add("NotInAD");
            tblWithAllHost.Columns.Add("NotInKSC");
            tblWithAllHost.Columns.Add("NotInSEP");
            tblWithAllHost.Columns.Add("OldBaseKES");
            tblWithAllHost.Columns.Add("OldBaseSEP");
            tblWithAllHost.Columns.Add("OldClientKES");
            tblWithAllHost.Columns.Add("OldClientSEP");
            tblWithAllHost.Columns.Add("NotInSCCM");


            for (int i = 0; i < tblWithAllHost.Rows.Count; i++)
            {
                tblWithAllHost.Rows[i]["NotInAD"] = "False";
                tblWithAllHost.Rows[i]["NotInKSC"] = "False";
                tblWithAllHost.Rows[i]["NotInSEP"] = "False";
                tblWithAllHost.Rows[i]["OldBaseKES"] = "False";
                tblWithAllHost.Rows[i]["OldBaseSEP"] = "False";
                tblWithAllHost.Rows[i]["OldClientKES"] = "False";
                tblWithAllHost.Rows[i]["OldClientSEP"] = "False";
                tblWithAllHost.Rows[i]["NotInSCCM"] = "False";


            }
        }

        private void GetTblWithHostNotInAD()
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение хостов, которых нет в AD...");
            tblWithHostNotInAD = new DataTable();
            tblWithHostNotInAD = GetLeftOuterJoin(tblWithCleanMPReport.Copy(), "MP_Name", tblWithCleanADReport.Copy(), "name").Copy();
        }

        /// <summary>
        /// Получение чистого отчета MP в переменную tblWithCleanMPReport
        /// </summary>
        private void GetTblWithCleanMPReport()
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            tblWithMPReport = new DataTable();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение данных из MP...");
            tblWithMPReport = ReadMPReportToDataTable(MergeCSVInFolder(tb_PathMPReport.Text));
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Обработка данных из MP...");
            tblWithCleanMPReport = RemoveDuplicateAndRowsWithEmptyNameFromMPReport().Copy();
        }

        /// <summary>
        /// Получение чистого отчета AD в переменную tblWithCleanADReport
        /// </summary>
        private void GetTblWithCleanADReport()
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            listDomain = tb_domain.Text.Split(';');
            tblWithADReport = new DataTable();
            // TODO Убрать коммент и удалить строку с получением AD из файла
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение данных из AD...");
            tblWithADReport = GetComputersFromMultipleDomains(listDomain);
            //tblWithADReport = ReadCSVWithHeadersToDataTable(MergeCSVInFolder(@"C:\Users\KartashevVS\Desktop\2016-10-21\2016-11-15\SZB\AD"), ';');
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Обработка данных из AD...");
            tblWithCleanADReport = RemoveDuplicateAndDisableAndOldLastLogonFromADReport().Copy();

            
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            DeleteFileInDir(tempPath);
            
        }

        private void bt_ExportMainResultToExcel_Click(object sender, EventArgs e)
        {
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(tblWithAllHost, "Сводный отчет");
            wb.SaveAs(reportPath = "\\" + "Сводный отчет.xlsx");

            wb = new XLWorkbook();
            wb.Worksheets.Add(tblWithAllHost, "Сводный отчет");
            wb.SaveAs(reportPath = "\\" + "Сводный отчет.xlsx");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Начало работы...");

            Thread threadGetTblWithCleanADReport = new Thread(new ThreadStart(GetTblWithCleanADReport));
            Thread threadGetTblWithCleanMPReport = new Thread(new ThreadStart(GetTblWithCleanMPReport));

            threadGetTblWithCleanADReport.Start();
            threadGetTblWithCleanMPReport.Start();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Ожидание AD...");
            threadGetTblWithCleanADReport.Join();
            backgroundWorker1.ReportProgress(5);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Ожидание MP...");
            threadGetTblWithCleanMPReport.Join();
            backgroundWorker1.ReportProgress(6);

            GetTblWithHostNotInAD(); //по порядку
            backgroundWorker1.ReportProgress(7);
            CreateTblForAllHost(); //по порядку
            backgroundWorker1.ReportProgress(8);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Добавление хостов не в AD в общий список хостов...");
            AddHostNotInADToAllHostTable(); //по порядку
            backgroundWorker1.ReportProgress(10);

            ////////////////////////////////////////////////////////////////////////////////////////////MP и AD\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


            





            ////////////////////////////////////////////////////////////////////////////////////////////ALL и KSC\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            Thread threadGetHostNotInKSC = new Thread(new ThreadStart(For_Thread_GetHostNotInKSC));

            
            threadGetHostNotInKSC.Start();


            Thread threadGetHostNotInSEP = new Thread(new ThreadStart(For_Thread_GetHostNotInSEP));
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Начата обработка SEP...");
            threadGetHostNotInSEP.Start();

            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Ожидание KES...");
            threadGetHostNotInKSC.Join();
            backgroundWorker1.ReportProgress(12);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Ожидание SEP...");
            threadGetHostNotInSEP.Join();
            backgroundWorker1.ReportProgress(14);

            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Добавления информации о хостах без KES в общий список...");
            AddInfoAboutHostNotInKSCToAllHostTable();
            backgroundWorker1.ReportProgress(16);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Добавления информации о хостах без SEP в общий список...");
            AddInfoAboutHostNotInSEPToAllHostTable();



            backgroundWorker1.ReportProgress(20);
            

            ////////////////////////////////////////////////////////////////////////////////////////////ALL и SEP\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            

            

            ////////////////////////////////////////////////////////////////////////////////////////////ALL KES OLD BASE\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            Thread threadGetTblWithHostOldBaseAndClientKES = new Thread(new ThreadStart(For_Thread_GetHostOldBaseAndClientKES));
            Thread threadGetTblWithHostOldBaseAndClientSEP = new Thread(new ThreadStart(For_Thread_GetHostOldBaseAndClientSEP));


            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Начата обработка старых баз и клиентов KES...");
            threadGetTblWithHostOldBaseAndClientKES.Start();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Начата обработка старых баз и клиентов SEP...");
            threadGetTblWithHostOldBaseAndClientSEP.Start();

            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Ожидание KES...");
            threadGetTblWithHostOldBaseAndClientKES.Join();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Ожидание SEP...");
            threadGetTblWithHostOldBaseAndClientSEP.Join();

            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Добавление инфы о старых базах и клиентов KES SEP...");
            AddInfoAboutHostOldBaseKESToAllHostTable();
            AddInfoAboutHostOldBaseSEPToAllHostTable();
            AddInfoAboutHostOldClientKESToAllHostTable();
            AddInfoAboutHostOldClientSEPToAllHostTable();

            backgroundWorker1.ReportProgress(40);
            ////////////////////////////////////////////////////////////////////////////////////////////ALL SEP OLD BASE\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            


            
            ////////////////////////////////////////////////////////////////////////////////////////////ALL KES OLD Client\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            
            
            backgroundWorker1.ReportProgress(60);
            ////////////////////////////////////////////////////////////////////////////////////////////ALL SEP OLD Client\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            
            

            backgroundWorker1.ReportProgress(70);

            ////////////////////////////////////////////////////////////////////////////////////////////ALL SCCM\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Обработка SCCM...");
            GetTblWithCleanSCCMReport();

            AddInfoAboutHostNotInSCCMToAllHostTable();
            backgroundWorker1.ReportProgress(100);

            DateTime timeStop = DateTime.Now;
            lb_TimeStop.Text = DateTime.Now.ToShortTimeString();

            //TimeSpan timeAll = timeStop - timeStart;

            //lb_WorkingTime.Text = timeAll.TotalMinutes.ToString();

            MessageBox.Show("Complete " + DateTime.Now.ToShortTimeString());




        }

        private void For_Thread_GetHostOldBaseAndClientSEP()
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение хостов со старыми базами из SEP...");
            GetTblWithHostOldBaseSEP();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение хостов со старыми клиентами из SEP...");
            GetTblWithHostOldClientSEP();
        }

        private void For_Thread_GetHostOldBaseAndClientKES()
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение хостов со старыми базами из KES...");
            GetTblWithHostOldBaseKES();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение хостов со старыми клиентами из KES...");
            GetTblWithHostOldClientKES();
        }

        private void For_Thread_GetHostNotInSEP()
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение данных из SEP...");
            GetTblWithCleanSEPReport();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение хостов без SEP...");
            GetTblWithHostNotInSEP();
        }

        private void For_Thread_GetHostNotInKSC()
        {
            delUpdateUITextBox DelUpdateUItextBox = new delUpdateUITextBox(UpdateUITextBox);
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение данных из KSC...");
            GetTblWithCleanKSCReport();
            this.lb_Status.BeginInvoke(DelUpdateUItextBox, "Получение хостов без KSC...");
            GetTblWithHostNotInKSC();
        }

        public void UpdateUITextBox(string textBoxString)
        {
            this.lb_Status.Text = textBoxString;
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            progressBar1.Value = e.ProgressPercentage;            
        }


    }
}
