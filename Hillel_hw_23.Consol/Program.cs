namespace Hillel_hw_23.Consol
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Core.Settings.ConnectionStr = "Server=localhost;Port=3306;Database=contora;Uid=VSuser;Pwd=VisualStudio;";

            while (true)
            {
                Console.WriteLine(CustomMessages.SelectTable);
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine(CustomMessages.NotAnId);
                    continue;
                }
                switch (id)
                {
                    //Agent table.
                    case 1:
                        Agent.ConsoleInterface();
                        break;

                    //Department table.
                    //case 2:
                    //    ConsoleDepartment_Steps()
                    //    break;

                    //Case table.
                    case 3:
                        Case.ConsoleInterface();
                        break;

                    //Target table.
                    case 4:
                        Target.ConsoleInterface();
                        break;

                    //Check BD connection.
                    //case 0:
                    //    break;

                    default:
                        Console.WriteLine(CustomMessages.UnknownId);
                        break;
                }
            }
        }
    }
}