using MySql.Data.MySqlClient;

namespace Hillel_hw_23.Data
{
    public class Department
    {
        //Не обновлён под новую таблицу.

        //public static void AddNew(string name, int agentsCount, int actCases, int cloCases)
        //{
        //    using (MySqlConnection conn = new(Settings.ConnectionStr))
        //    {
        //        conn.Open();
        //        string addNew = "INSERT INTO contora.department " +
        //            "(name, agents_count, open_cases, closed_cases) " +
        //            "VALUES " +
        //            "(@name, @agentsCount, @actCases, @cloCases)";
        //        MySqlCommand command = new(addNew, conn);
        //        command.Parameters.AddWithValue("@name", name);
        //        command.Parameters.AddWithValue("@agentsCount", agentsCount);
        //        command.Parameters.AddWithValue("@actCases", actCases);
        //        command.Parameters.AddWithValue("@cloCases", cloCases);
        //        command.ExecuteNonQuery();
        //    }
        //}
    }
}