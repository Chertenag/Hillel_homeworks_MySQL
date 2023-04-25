using MySql.Data.MySqlClient;

namespace Hillel_hw_23.Data
{
    public class Case
    {
        public static void AddNew(int depId, int primAgentId, int? secAgentId, string dateOpen, string? dateClose)
        {
            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string addNew = @"INSERT INTO contora.case 
                    (
                    department_id, primary_agent_id, secondary_agent_id, 
                    date_open, date_close
                    ) 
                    VALUES 
                    (
                    @depId, @primAgentId, @secAgentId, 
                    @dateOpen, @dateClose
                    )";
                MySqlCommand command = new(addNew, conn);
                command.Parameters.AddWithValue("@depId", depId);
                command.Parameters.AddWithValue("@primAgentId", primAgentId);
                command.Parameters.AddWithValue("@secAgentId", secAgentId);
                command.Parameters.AddWithValue("@dateOpen", dateOpen);
                command.Parameters.AddWithValue("@dateClose", dateClose);
                command.ExecuteNonQuery();
            }
        }
    }
}