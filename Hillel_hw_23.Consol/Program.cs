namespace Hillel_hw_23.Consol
{
    internal class Program
    {
        private static readonly string selectTable = "Выберите таблицу: Agent - 1, Department - 2 (не доступно), Case - 3, Target - 4. Проверка подключения к БД - 0 (не доступно).";
        private static readonly string chooseAction = "Выберите действие: Добавить запись - 1, Обновить запись - 2 (не доступно), Поиск записи(ей) - 3 (не доступно), Удалить запись - 4 (не доступно), Отмена - 0";
        private static readonly string unknownId = "ERROR: Необходимо ввести ID из списка, указанного выше.";
        private static readonly string notAnId = "ERROR: Ведденое значение не является ID.";

        static void Main(string[] args)
        {
            Core.Settings.ConnectionStr = "Server=localhost;Port=3306;Database=contora;Uid=VSuser;Pwd=VisualStudio;";

            while (true)
            {
                Console.WriteLine(selectTable);
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine(notAnId);
                    continue;
                }
                switch (id)
                {
                    //Agent table.
                    case 1:
                        ConsoleAgent_Steps();
                        break;

                    //Department table.
                    //case 2:
                    //    ConsoleDepartment_Steps()
                    //    break;

                    //Case table.
                    case 3:
                        ConsoleCase_Steps();
                        break;

                    //Target table.
                    case 4:
                        ConsoleTarget_Steps();
                        break;

                    //Check BD connection.
                    //case 0:
                    //    break;

                    default:
                        Console.WriteLine(unknownId);
                        break;
                }
            }
        }

        private static void ConsoleAgent_Steps()
        {
            while (true)
            {
                Console.WriteLine(chooseAction);
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(notAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Add record.
                        case 1:
                            ConsoleAgentAddNew_Steps();
                            break;

                        //Update record.
                        //case 2:
                        //    break;

                        //Search record.
                        //case 3:
                        //    break;

                        //Delete record.
                        //case 4:
                        //    break;

                        //Return from Agent table.
                        case 0:
                            return;
                        
                        default:
                            Console.WriteLine(unknownId);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }
            }
        }

        private static void ConsoleAgentAddNew_Steps()
        {
            Console.WriteLine("Введите имя (обязательное поле <= 50 символов).");
            string fName = Console.ReadLine();
            Console.WriteLine("Введите фамилию (обязательное поле <= 50 символов).");
            string lName = Console.ReadLine();
            Console.WriteLine("Введите отчество (не обязательное поле <= 50 символов).");
            string mName = Console.ReadLine();
            Console.WriteLine("Введите ID отдела (обязательное поле).");
            int depId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID должности (обязательное поле).");
            int posId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID звания (обязательное поле).");
            int rankId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID текущего статуса (обязательное поле).");
            int statusId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите телефон (не обязательное поле <= 20 символов).");
            string phone = Console.ReadLine();
            Console.WriteLine("Введите адрес (не обязательное поле <= 100 символов).");
            string address = Console.ReadLine();

            Core.Agent.AddNew(fName, lName, mName, depId, posId, rankId, statusId, phone, address);

            Console.WriteLine("Запись добавлена.");
        }
        private static void ConsoleCase_Steps()
        {
            while (true)
            {
                Console.WriteLine(chooseAction);
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(notAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Add record.
                        case 1:
                            ConsoleCaseAddNew_Steps();
                            break;

                        //Update record.
                        //case 2:
                        //    break;

                        //Search record.
                        //case 3:
                        //    break;

                        //Delete record.
                        //case 4:
                        //    break;

                        //Return from Agent table.
                        case 0:
                            return;
                        default:
                            Console.WriteLine(unknownId);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }
            }
        }

        private static void ConsoleCaseAddNew_Steps()
        {
            Console.WriteLine("Введите ID отдела (обязательное поле).");
            int depId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID главного агента (обязательное поле).");
            int primId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ID второго агента (не обязательное поле).");
            var rez = Console.ReadLine();
            int secId = -1; 
            if (!string.IsNullOrEmpty(rez))
            {
                secId = Int32.Parse(rez);
            }
            Console.WriteLine("Введите открытия дела (обязательное поле - 1999.12.31).");
            string openDate = Console.ReadLine();
            Console.WriteLine("Введите закрытия дела (не обязательное поле - 1999.12.31).");
            string clDate = Console.ReadLine();

            Core.Case.AddNew(depId, primId, secId, openDate, clDate);

            Console.WriteLine("Запись добавлена.");
        }

        private static void ConsoleTarget_Steps()
        {
            while (true)
            {
                Console.WriteLine(chooseAction);
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(notAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Add record.
                        case 1:
                            ConsoleTargetAddNew_Steps();
                            break;

                        //Update record.
                        //case 2:
                        //    break;

                        //Search record.
                        //case 3:
                        //    break;

                        //Delete record.
                        //case 4:
                        //    break;

                        //Return from Agent table.
                        case 0:
                            return;
                        default:
                            Console.WriteLine(unknownId);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    continue;
                }
            }
        }

        private static void ConsoleTargetAddNew_Steps()
        {
            Console.WriteLine("Введите имя (обязательное поле <= 50 символов).");
            string fName = Console.ReadLine();
            Console.WriteLine("Введите фамилию (обязательное поле <= 50 символов).");
            string lName = Console.ReadLine();
            Console.WriteLine("Введите отчество (не обязательное поле <= 50 символов).");
            string mName = Console.ReadLine();
            Console.WriteLine("Введите ID дела (обязательное поле).");
            int caseId = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите телефон (не обязательное поле <= 20 символов).");
            string phone = Console.ReadLine();
            Console.WriteLine("Введите дату рождения (не обязательное поле - 1999.12.31).");
            string bDay = Console.ReadLine();
            Console.WriteLine("Введите адрес (не обязательное поле <= 100 символов).");
            string address = Console.ReadLine();
            Console.WriteLine("Введите дополнительную информацию (не обязательное поле <= 250 символов).");
            string info = Console.ReadLine();

            Core.Target.AddNew(fName, lName, mName, caseId, phone, bDay, address, info);

            Console.WriteLine("Запись добавлена.");
        }
    }
}