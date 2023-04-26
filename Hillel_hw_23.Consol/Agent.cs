namespace Hillel_hw_23.Consol
{
    public class Agent
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

        public static void ConsoleAgentAddNew_Steps()
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
    }
}