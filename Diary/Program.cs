using System;


namespace Diary
{
    class Program
    {
        static void Main(string[] args)
        {
            Char input;//переменная для сохранения ввода пользователя
            bool textisDate;
            string message = "";//данные о результате работы метода
            DateTime time;
            DiaryStruct diary = new DiaryStruct(new DiaryStruct.Record[] { });//создали ежедневник
            input = Menu.PrintMenuCreateDeleteRecord();
            while (input != 'Q')
            {
                switch (input)
                {
                    case '1':
                        Console.WriteLine();
                        DiaryStruct.Record InputData = new DiaryStruct.Record();
                        Console.WriteLine("Введите тип события: ");
                        InputData.Type_of_action = Console.ReadLine();
                        Console.WriteLine("Введите описание события: ");
                        InputData.Desc_action = Console.ReadLine();
                        Console.WriteLine("Введите время события в формате чч:мм ");
                        do
                        {
                            textisDate = DiaryStruct.TextIsDate(Console.ReadLine(), out time);
                        }
                        while (!textisDate);
                        InputData.Time_of_action = time;
                        InputData.Time_create = DateTime.Now;
                        Console.WriteLine("Ввод данных записи окончен. Запись сохранена");
                        diary.AddRecord(InputData);
                        Menu.PrintDiary(diary);
                        Console.WriteLine("Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                    case '2':
                        input = Menu.PrintMenuDelete();

                        while (input != 'Q')
                        {
                            switch (input)
                            {
                                case '1':
                                    Console.WriteLine("Введите номер записи: ");
                                    diary.DeleteRecordForNumber(int.Parse(Console.ReadKey().ToString()));
                                    Console.WriteLine();
                                    break;
                                case '2':
                                    Console.WriteLine("Введите тип события: ");
                                    diary.DeleteRecordForTypeAction(Console.ReadLine().ToString());
                                    Console.WriteLine();
                                    break;
                                case '3':
                                    Console.WriteLine("Введите описание события: ");
                                    diary.DeleteRecordForDescAction(Console.ReadLine().ToString());
                                    Console.WriteLine();
                                    break;
                                case '4':
                                    Console.WriteLine("Введите время события в формате чч:мм : ");
                                    do
                                    {
                                        textisDate = DiaryStruct.TextIsDate(Console.ReadLine(), out time);
                                    }
                                    while (!textisDate);
                                    diary.DeleteRecordForTimeAction(time);
                                    Console.WriteLine();
                                    break;
                                case '5':
                                    Console.WriteLine("Введите время записи записи события в формате чч:мм: ");
                                    diary.DeleteRecordForTimeRecords(DateTime.Parse(Console.ReadLine().ToString()));
                                    Console.WriteLine();
                                    break;
                                default:
                                    Menu.PrintMenuDelete();
                                    break;
                            }
                            Menu.PrintDiary(diary);
                            Console.WriteLine();
                        }
                        input = Menu.PrintMenuDelete();
                        break;
                    case '3':
                        int vvod;
                        int numberRecord;
                        Console.Write("Введите номер записи для редактирования: ");
                        numberRecord = int.Parse(Console.ReadLine());
                        Console.WriteLine("Редактирование Типа события - нажмите 1");
                        Console.WriteLine("Редактирование Описания события - нажмите 2");
                        Console.WriteLine("Редактирование Времени события - нажмите 3");
                        vvod = int.Parse(Console.ReadLine());
                        while (vvod != 'Q')
                        {
                            switch (vvod)
                            {
                                case 1:
                                    diary.ChangeRecordForTypeAction(numberRecord, Console.ReadLine());
                                    break;
                                case 2:
                                    diary.ChangeRecordForDescAction(numberRecord, Console.ReadLine());
                                    break;
                                case 3:
                                    diary.ChangeRecordForTimeAction(numberRecord, DateTime.Parse(Console.ReadLine()));
                                    break;
                                default:
                                    Console.Write("Введите номер записи для редактирования: ");
                                    numberRecord = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Тип события - нажмите 1");
                                    Console.WriteLine("Описание события - нажмите 2");
                                    Console.WriteLine("Время события - нажмите 3");
                                    vvod = int.Parse(Console.ReadLine());
                                    break;
                            }
                        }
                        Console.WriteLine("Редактирование записей завершено");
                        Menu.PrintDiary(diary);
                        Console.WriteLine("Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Введен некорректный символ. Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            input = Menu.PrintMenuLoadSave();
            Console.WriteLine();
            while (input != 'Q')
            {
                switch (input)
                {
                    case '1':
                        diary.LoadDiaryfromFile(out message);
                        Console.WriteLine(message);//информация о результате операции
                        Menu.PrintDiary(diary);
                        Console.WriteLine("File loaded succesfull");
                        Console.WriteLine("Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                    case '2':
                        diary.SaveDairyinFile();
                        Console.WriteLine("Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                    case '3':
                        Console.Write("Введите первую дату диапазона по времени события в формате чч:мм :");
                        DateTime time_1 = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите вторую дату диапазона по времени события в формате чч:мм :");
                        DateTime time_2 = DateTime.Parse(Console.ReadLine());
                        diary.LoadDiaryfromFile(time_1, time_2, message);
                        Console.WriteLine(message);
                        Menu.PrintDiary(diary);
                        Console.WriteLine("Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                    case '4':
                        //diary.SortDiary();
                        Console.WriteLine("Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Введен некорректный символ. Повторите выбор меню: ");
                        input = Console.ReadKey().KeyChar;
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
