namespace Hillel_hw_23.Consol
{
    public class Target
    {
        public static async Task ConsoleInterface()
        {
            while (true)
            {
                Console.WriteLine(CustomMessages.ChooseMainAction);
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
                            await ConsoleTargetAddNew_Steps(CancellationToken.None);
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

        public static async Task ConsoleTargetAddNew_Steps(CancellationToken token)
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

            await Hillel_hw_25.EFCore.Target.Create_async(0/*В БД автоинкремент, но мог бы быть GUID,*/, 
                fName, lName, mName, caseId, phone, bDay, address, info, token);

            Console.WriteLine("Запись добавлена.");
        }
    }
}