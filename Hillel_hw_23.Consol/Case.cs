namespace Hillel_hw_23.Consol
{
    public class Case
    {
        public static void ConsoleInterface()
        {
            while (true)
            {
                Console.WriteLine(CustomMessages.ChooseAction);
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

        public static void ConsoleCaseAddNew_Steps()
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
    }
}