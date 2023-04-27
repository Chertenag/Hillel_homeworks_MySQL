namespace Hillel_hw_23.Consol
{
    public class Case
    {
        public static readonly string ChooseMainAction = "Выберите действие: Добавить запись - 1, Обновить запись - 2 (не доступно), Поиск записи(ей) - 3 , Удалить запись - 4 (не доступно), Отмена - 0.";

        public static async Task ConsoleMainInterface()
        {
            while (true)
            {
                Console.WriteLine("====== Таблица Case ======");
                Console.WriteLine(ChooseMainAction);
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Add record.
                        case 1:
                            await AddNew(CancellationToken.None);
                            break;

                        //Update record.
                        //case 2:
                        //    break;

                        //Search record.
                        case 3:
                            await ConsoleSearchInterface(CancellationToken.None);
                            break;

                        //Delete record.
                        //case 4:
                        //    break;

                        //Return from Agent table.
                        case 0:
                            return;
                        default:
                            Console.WriteLine(CustomMessages.UnknownId);
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

        private static async Task AddNew(CancellationToken token)
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
            Console.WriteLine("Введите дату открытия дела (обязательное поле - 1999.12.31).");
            string openDate = Console.ReadLine();
            Console.WriteLine("Введите дату закрытия дела (не обязательное поле - 1999.12.31).");
            string clDate = Console.ReadLine();

            await Core.Case.AddNew(depId, primId, secId, openDate, clDate, token);

            Console.WriteLine("Запись добавлена.");
        }

        private static async Task ConsoleSearchInterface(CancellationToken token)
        {
            while (true)
            {
                Console.WriteLine("====== Таблица Case ======");
                Console.WriteLine("Выберите параметр поиска: Department ID - 1, Department name - 2 (не доступно), Agent ID - 3, Agent FullName - 4 (не доступно), " +
                    "Date open - 5 (не доступно), Date close - 6 (не доступно), Отмена - 0.");
                if (!int.TryParse(Console.ReadLine(), out int idAction))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                try
                {
                    switch (idAction)
                    {
                        //Department ID.
                        case 1:
                            await Search_ByDepartmentId(token);
                            break;

                        //Department name.
                        //case 2:
                        //    break;

                        //Agent ID.
                        //case 3:
                        //    break;

                        //Agent FullName.
                        //case 4:
                        //    break;

                        //Date open.
                        //case 5:
                        //    break;

                        //Date close.
                        //case 6:
                        //    break;

                        //Return from Agent table.
                        case 0:
                            return;
                        default:
                            Console.WriteLine(CustomMessages.UnknownId);
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


        private static async Task Search_ByDepartmentId(CancellationToken token)
        {
            Console.WriteLine("Введите ID отдела (обязательное поле).");
            int depId = Int32.Parse(Console.ReadLine());

            var rez = await Core.Case.Search_ByDepartmentId(depId, token);

            Console.WriteLine($"Найдено совпадений: {rez.Count}");
            rez.ForEach(x => Console.WriteLine($"ID департамента: {x.DepartmentID}, ID основного агента: {x.PrimaryAgentID}, " +
                $"ID второго агента - {(x.SecondaryAgentID.HasValue ? x.SecondaryAgentID : "(отсутсвует)")}, " +
                $"дата открытия дела - {x.DateOpen:yyyy.MM.dd}, " +
                $"дата закрытия дела - {(x.DateClosed.HasValue ? x.DateClosed.Value.ToString("yyyy.MM.dd") : "(отсутсвует)")}."));
        }

        private static void Search_ByAgentId()
        {

        }

        private static void Search_ByDateOpen() 
        {
            
        }

        private static void Search_ByDateClose()
        {

        }
    }
}