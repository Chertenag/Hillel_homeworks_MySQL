using MySql.Data.MySqlClient;

namespace Hillel_hw_23.Data
{
    public class Agent
    {
        private static readonly string connStr = "Server=localhost;Port=3306;Database=contora;Uid=VSuser;Pwd=VisualStudio;";

        public static void AddNew(string name, int depId, string position, string rank, int actCases, int cloCases)
        {
            using (MySqlConnection conn = new(connStr))
            {
                conn.Open();
                string addNew = "INSERT INTO contora.agent" +
                    "(name, department_id, position, rank, active_cases, closed_cases)" +
                    "VALUES " +
                    $"('{name}', '{depId}', '{position}', '{rank}', '{actCases}', '{cloCases}')";
                MySqlCommand mySqlCommand = new MySqlCommand(addNew);
                mySqlCommand.ExecuteNonQuery();
            }
        }
    }

    public class Target
    {
        private static readonly string connStr = "Server=localhost;Port=3306;Database=contora;Uid=VSuser;Pwd=VisualStudio;";

        public static void AddNew(string name, string phone, string bday, string address)
        {
            using (MySqlConnection conn = new(connStr))
            {
                conn.Open();
                string addNew = "INSERT INTO contora.target" +
                    "(name, phone, birthdate, address) " +
                    "VALUES " +
                    "(@name, @phone, @bday, @address)";
                MySqlCommand command = new(addNew);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@bday", bday);
                command.Parameters.AddWithValue("@address", address);
                ,,,
                command.ExecuteNonQuery();
            }
        }


    }
}