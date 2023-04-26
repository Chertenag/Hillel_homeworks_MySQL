using MySql.Data.MySqlClient;

namespace Hillel_hw_23.Data
{
    public class Agent
    {
        public static async Task AddNew(string fName, string lName, string? mName, int depId, int positionId, int rankId, int statusId, string? phone, string? address, CancellationToken token)
        {
            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string addNew = @"INSERT INTO contora.agent 
                    (
                    `first_name`, `last_name`, `middle_name`, `department_id`, 
                    `position_id`, `rank_id`, `status_id`, `phone`, `address`
                    )
                    VALUES 
                    (
                    @fName, @lName, @mName, @depId, 
                    @positionId, @rankId, @statusId, @phone, @address
                    )";
                MySqlCommand command = new(addNew, conn);
                command.Parameters.AddWithValue("@fName", fName);
                command.Parameters.AddWithValue("@lName", lName);
                command.Parameters.AddWithValue("@mName", mName);
                command.Parameters.AddWithValue("@depId", depId);
                command.Parameters.AddWithValue("@positionId", positionId);
                command.Parameters.AddWithValue("@rankId", rankId);
                command.Parameters.AddWithValue("@statusId", statusId);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@address", address);
                await command.ExecuteNonQueryAsync(token);
            }
        }
    }
}