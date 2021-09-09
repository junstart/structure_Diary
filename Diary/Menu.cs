using System;

namespace Diary
{
    public class Menu
    {
        /// <summary>
        /// Печать меню - оптимизация кода
        /// </summary>
        public static char PrintMenuDelete()
        {
            Console.WriteLine("Повторите выбор меню: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Меню удаления");
            Console.WriteLine("Для удаления записи по номеру - введите 1");
            Console.WriteLine("Для удаления записи по типу события - введите 2");
            Console.WriteLine("Для удаления записи по описанию события - введите 3");
            Console.WriteLine("Для удаления записи по времени события - введите 4");
            Console.WriteLine("Для удаления записи по дате создания записи - введите 5");
            Console.WriteLine("Для выхода из меню - введите Q");
            Console.ResetColor();
            return Console.ReadKey().KeyChar;
        }

        public static char PrintMenuLoadSave()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Меню редактирования");
            Console.WriteLine("Для загрузки ежедневника из файла - введите 1");
            Console.WriteLine("Для сохранения записей в файл - введите 2");
            Console.WriteLine("Для загрузка записей из файла по диапазону дат - введите 3");
            Console.WriteLine("Для упорядочивания записей по выбранному полю - нажмите 4");
            Console.WriteLine("Для выхода - введите Q");
            Console.ResetColor();
            return Console.ReadKey().KeyChar;
        }

        public static char PrintMenuCreateDeleteRecord()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Дневник для записи");
            Console.WriteLine("Меню");
            Console.WriteLine("Для создания записи - введите 1");
            Console.WriteLine("Для удаления записи - введите 2");
            Console.WriteLine("Для редактирования записи - введите 3");
            Console.WriteLine("Для выхода - введите Q");
            Console.ResetColor();
            return Console.ReadKey().KeyChar;
        }

        public static void PrintDiary(DiaryStruct t)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Ежедневник");
            if (!t.Equals(null))
            {
                foreach (DiaryStruct.Record r in t.GetRecords())
                {
                    Console.WriteLine("номер записи: {0} тип события: {1}  описание события: {2} время события: {3} дата создания записи: {4}", r.Index_record, r.Type_of_action, r.Desc_action, r.Time_of_action, r.Time_create);
                }
            }
            Console.ResetColor();
        }


    }
}
