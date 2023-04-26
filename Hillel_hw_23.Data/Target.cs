using MySql.Data.MySqlClient;

namespace Hillel_hw_23.Data
{
    public class Target
    {
        public static async Task AddNew(string fName, string lName, string? mName, int caseId, string? phone, string? bday, string? address, string? info, CancellationToken token)
        {
            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string addNew = @"INSERT INTO contora.target
                    (
                    `first_name`, `last_name`, `middle_name`, `case_id`, 
                    `phone`, `birthdate`, `address`, `additional_info`)
                    VALUES 
                    (
                    @fName, @lName, @mName, @caseId, 
                    @phone, @bday, @address, @info
                    )";
                MySqlCommand command = new(addNew, conn);
                command.Parameters.AddWithValue("@fName", fName);
                command.Parameters.AddWithValue("@lName", lName);
                command.Parameters.AddWithValue("@mName", mName);
                command.Parameters.AddWithValue("@caseId", caseId);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@bday", bday);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@info", info);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}