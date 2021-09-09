using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Diary
{
    public struct DiaryStruct
    {
        private Record[] records;
        private string baseFolder;
        public string path;

        public Record[] GetRecords()
        {
            return records;
        }
        /// <summary>
        /// Индексатор по номеру записи
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Record this[int index]
        {
            get { return records[index]; }
            set { records[index] = value; }
        }
        /// <summary>
        /// Проверка ввода даты
        /// </summary>
        /// <param name="text"></param>
        /// <param name="scheduleDate"></param>
        /// <returns></returns>
        public static bool TextIsDate(string text, out DateTime scheduleDate)
        {
            var dateFormat = "hh:mm";

            if (!DateTime.TryParseExact(text, dateFormat, provider: CultureInfo.InvariantCulture, DateTimeStyles.None, out scheduleDate))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="args"></param>
        public DiaryStruct(params Record[] args)
        {
            records = args;
            baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(baseFolder, "MyTest.txt");
        }
        /// <summary>
        /// Тип данных запись
        /// </summary>
        public struct Record
        {
            private int index_record;//номер записи
            private string type_of_action;//тип события
            private string desc_action;//описание события
            private DateTime time_of_action;//время события
            private DateTime time_create;//дата создания записи

            public DateTime Time_create { get => time_create; set => time_create = value; }
            public string Type_of_action { get => type_of_action; set => type_of_action = value; }
            public string Desc_action { get => desc_action; set => desc_action = value; }
            public DateTime Time_of_action { get => time_of_action; set => time_of_action = value; }
            public int Index_record { get => index_record; set => index_record = value; }

        }     
        /// <summary>
        /// Ввод данных в ручную
        /// </summary>
        public void AddRecord(Record record)
        {
             if (this.GetRecords().Length == 0)
             {
                records = new Record[1];
                records[0].Index_record = 0;
                records[0].Time_create = record.Time_create;
                records[0].Time_of_action = record.Time_of_action;
                records[0].Type_of_action = record.Type_of_action;
             }
             else
             {
                int newsize = records.Length;
                Array.Resize(ref records, newsize + 1);
                records[newsize].Index_record = records.Length - 1;
             }          
        }
        /// <summary>
        /// Перегрузка метода добавление записи в Ежедневник из файла
        /// </summary>
        /// <param name="line"></param>
        public void AddRecord(string[] line, out string messageResult)
        {
            if (line != null && line.Length >= 5)
            {
                if (records == null)
                {
                    records = new Record[1];
                }
                else Array.Resize(ref records, records.Length + 1);
                ////////////////////////////////////

                records[records.Length - 1].Index_record = records.Length - 1;
                records[records.Length - 1].Type_of_action = line[1].Replace(" ", "");
                records[records.Length - 1].Desc_action = line[2].Replace(" ", "");
                records[records.Length - 1].Time_of_action = DateTime.Parse(line[3] + " " + line[4]);
                records[records.Length - 1].Time_create = DateTime.Parse(line[5] + " " + line[6]);
                messageResult = "Ввод данных записи окончен. Запись сохранена";
            }
            else { messageResult = "Входные данные некорректны"; }
        }
        /// <summary>
        /// Удаление записи по номеру
        /// </summary>
        public void DeleteRecordForNumber(int temp)
        {
            records = records.Where((val, idx) => idx != temp).ToArray();
        }
        /// <summary>
        /// Удаление записи по типу события
        /// </summary>
        public void DeleteRecordForTypeAction(string tempTypeAction)
        {
            records = records.Where(val => val.Type_of_action != tempTypeAction).ToArray();
        }

        /// <summary>
        /// Удаление записи по описанию события
        /// </summary>
        public void DeleteRecordForDescAction(string DescAction)
        {
            records = records.Where(val =>
            {
                return val.Desc_action != DescAction;
            }).ToArray();
        }

        /// <summary>
        /// Удаление записи по времени записи
        /// </summary>
        public void DeleteRecordForTimeRecords(DateTime dataRecord)
        {
            records = records.Where(val => val.Time_create != dataRecord).ToArray();
        }

        /// <summary>
        /// Удаление записи по времени события
        /// </summary>
        public void DeleteRecordForTimeAction(DateTime timeofAction)
        {
            records = records.Where(val => val.Time_of_action != timeofAction).ToArray();
        }
        /// <summary>
        /// Изменение записи в части типа события
        /// </summary>
        /// <param name="numberRecord"></param>
        /// <param name="typeAction"></param>
        public void ChangeRecordForTypeAction(int numberRecord, string typeAction)
        {
            records[numberRecord].Type_of_action = typeAction;
        }
        /// <summary>
        /// Изменение записи в части описания события
        /// </summary>
        /// <param name="numberRecord"></param>
        /// <param name="descAction"></param>
        public void ChangeRecordForDescAction(int numberRecord, string descAction)
        {
            if (descAction == null)
            {
                throw new ArgumentNullException(nameof(descAction));
            }

            records[numberRecord].Desc_action = descAction;
        }
        /// <summary>
        /// Изменение записи в части времени события
        /// </summary>
        /// <param name="numberRecord"></param>
        /// <param name="timeAction"></param>
        public void ChangeRecordForTimeAction(int numberRecord, DateTime timeAction)
        {
            if (timeAction == null)
            {
                throw new ArgumentNullException(nameof(timeAction));
            }

            records[numberRecord].Time_of_action = timeAction;
        }
        /// <summary>
        /// Запись в файл данных ежедневника
        /// </summary>
        public void SaveDairyinFile()
        {
            Console.WriteLine();
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] grass = new UTF8Encoding(true).GetBytes($"{"Номер записи"}\t{"Тип события",8}\t{"Описание события",10}\t{"Время события",14}\t{"Дата создания записи",16}\t\n");
                    fs.Write(grass, 0, grass.Length);
                    foreach (Record t in records)
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes($"{t.Index_record.ToString(),6}\t{t.Type_of_action.ToString(),14}\t{t.Desc_action.ToString(),16}\t{t.Time_of_action.ToString(),22}\t{t.Time_create,18}\t\n");
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                    fs.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("File created");

        }
        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        /// <returns></returns>
        public void LoadDiaryfromFile(out string message)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] fileText = s.Split(new char[] { ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fileText[0] != "Номер записи") AddRecord(fileText,out message);
                }             
            }
            message = "Загрузка из файла выполнена";
        }
        /// <summary>
        /// Загрузка записей в выбранном диапазоне дат
        /// </summary>
        /// <param name="time_1"></param>
        /// <param name="time_2"></param>
        public void LoadDiaryfromFile(DateTime time_1, DateTime time_2, string message)
        {
            string[] fileText;
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    fileText = s.Split(new char[] { ',', '\t',' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (fileText[0] != "Номер")
                    {
                        s = fileText[3] + " " + fileText[4];
                        if (DateTime.Parse(s) <= time_1 || DateTime.Parse(s) >= time_2)
                        {
                            continue;
                        }
                        AddRecord(fileText,out message);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /*
        public void SortDiary()
        {

        }


        */

    }
}
