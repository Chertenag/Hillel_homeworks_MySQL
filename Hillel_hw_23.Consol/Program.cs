using System.Runtime.InteropServices;

namespace Hillel_hw_23.Consol
{
    internal class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // State of the application once loaded
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static async Task Main(string[] args)
        {
            //Надоело каждый раз при тестах разворачивать консоль на весь экран...
            ShowWindow(GetConsoleWindow(), MAXIMIZE);
            Core.Settings.ConnectionStr = "Server=localhost;Port=3306;Database=contora;Uid=VSuser;Pwd=VisualStudio;";

            while (true)
            {
                Console.WriteLine("====== Основное меню ======");
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
                        await Agent.ConsoleMainInterface();
                        break;

                    //Department table.
                    //case 2:
                    //    ConsoleDepartment_Steps()
                    //    break;

                    //Case table.
                    case 3:
                        await Case.ConsoleMainInterface();
                        break;

                    //Target table.
                    case 4:
                        await Target.ConsoleInterface();
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