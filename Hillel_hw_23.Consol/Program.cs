namespace Hillel_hw_23.Consol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Core.Settings.ConnectionStr = "Server=localhost;Port=3306;Database=contora;Uid=VSuser;Pwd=VisualStudio;";

            //Core.Agent.AddNew("First_name", "Last_name", "Middle_name", 9, 5, 6, "+380931234567", "Odessa, blabla");
            //Core.Case.AddNew(9, 1, 2, "2020-01-01", "");
            //Core.Target.AddNew("First_name", "Last_name", "Middle_name", 22, "+380679876543", "", "", "Very opasniy guy.");
        }
    }
}