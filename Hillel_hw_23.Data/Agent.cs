using MySql.Data.MySqlClient;

namespace Hillel_hw_23.Data
{
    public class Agent
    {
        private int id;
        private string fName;
        private string lName;
        private string? mName;
        private int depId;
        private int posId;
        private int rankId;
        private int statusId;
        private string? phone;
        private string? address;

        public int ID { get => id; set => id = value; }
        public string FirstName { get => fName; set => fName = value; }
        public string LastName { get => lName; set => lName = value; }
        public string? MiddleName { get => mName; set => mName = value; }
        public int DepartmentID { get => depId; set => depId = value; }
        public int PositionID { get => posId; set => posId = value; }
        public int RankID { get => rankId; set => rankId = value; }
        public int StatusID { get => statusId; set => statusId = value; }
        public string? Phone { get => phone; set => phone = value; }
        public string? Address { get => address; set => address = value; }

        public Agent(int id, string fName, string lName, string? mName, int depId, int positionId, int rankId, int statusId, string? phone, string? address)
        {
            this.id = id;
            this.fName = fName;
            this.lName = lName;
            this.mName = mName;
            this.depId = depId;
            this.posId = positionId;
            this.rankId = rankId;
            this.statusId = statusId;
            this.phone = phone;
            this.address = address;
        }

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

        //См. Core.Agent.Search_ByField<T>(...).
        ///// <summary>
        ///// Посик в таблице по значению одной колонки.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="columnName">Имя колонки.</param>
        ///// <param name="searchValue">Значения для поиска.</param>
        ///// <param name="token">Токен отмены.</param>
        ///// <returns>Список всех найдёных объектов типа Data.Agent.</returns>
        //public static async Task<List<Agent>> Search_ByField<T>(AgentColumns columnName, T searchValue, CancellationToken token)
        //{
        //    List<Agent> agents = new();

        //    using (MySqlConnection conn = new(Settings.ConnectionStr))
        //    {
        //        conn.Open();
        //        string search = $@"SELECT * FROM contora.agent
        //            WHERE {columnName} = @search";
        //        MySqlCommand command = new(search, conn);
        //        command.Parameters.AddWithValue("@search", searchValue);
        //        MySqlDataReader reader = command.ExecuteReader();

        //        while (await reader.ReadAsync(token))
        //        {
        //            agents.Add(new Agent
        //                (
        //                reader.GetInt32(AgentColumns.id.ToString()),
        //                reader.GetString(AgentColumns.first_name.ToString()),
        //                reader.GetString(AgentColumns.last_name.ToString()),
        //                reader.GetString(AgentColumns.middle_name.ToString()),
        //                reader.GetInt32(AgentColumns.department_id.ToString()),
        //                reader.GetInt32(AgentColumns.position_id.ToString()),
        //                reader.GetInt32(AgentColumns.rank_id.ToString()),
        //                reader.GetInt32(AgentColumns.status_id.ToString()),
        //                reader.GetString(AgentColumns.phone.ToString()),
        //                reader.GetString(AgentColumns.address.ToString())
        //                ));
        //        }
        //    }
        //    return agents;
        //}

        //Знаю, что тут в любом случае будет или 0 или 1, но так проще обрабатывать как с другими запросами.
        public static async Task<List<Agent>> Search_ById(int id, CancellationToken token)
        {
            List<Agent> agents = new();

            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string search = $@"SELECT * FROM contora.agent
                        WHERE {AgentColumns.id} = @searchValue";
                MySqlCommand command = new(search, conn);
                command.Parameters.AddWithValue("@searchValue", id);
                MySqlDataReader reader = command.ExecuteReader();

                while (await reader.ReadAsync(token))
                {
                    agents.Add(await ReadAgent(reader, token));
                }
            }
            return agents;
        }

        public static async Task<List<Agent>> Search_ByFirstName(string fName, CancellationToken token)
        {
            List<Agent> agents = new();

            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string search = $@"SELECT * FROM contora.agent
                        WHERE {AgentColumns.first_name} = @searchValue";
                MySqlCommand command = new(search, conn);
                command.Parameters.AddWithValue("@searchValue", fName);
                MySqlDataReader reader = command.ExecuteReader();

                while (await reader.ReadAsync(token))
                {
                    agents.Add(await ReadAgent(reader, token));
                }
            }
            return agents;
        }

        public static async Task<List<Agent>> Search_ByLastName(string lName, CancellationToken token)
        {
            List<Agent> agents = new();

            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string search = $@"SELECT * FROM contora.agent
                        WHERE {AgentColumns.last_name} = @searchValue";
                MySqlCommand command = new(search, conn);
                command.Parameters.AddWithValue("@searchValue", lName);
                MySqlDataReader reader = command.ExecuteReader();

                while (await reader.ReadAsync(token))
                {
                    agents.Add(await ReadAgent(reader, token));
                }
            }
            return agents;
        }

        public static async Task<List<Agent>> Search_ByDepartmentId(int depId, CancellationToken token)
        {
            List<Agent> agents = new();

            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string search = $@"SELECT * FROM contora.agent
                        WHERE {AgentColumns.department_id} = @searchValue";
                MySqlCommand command = new(search, conn);
                command.Parameters.AddWithValue("@searchValue", depId);
                MySqlDataReader reader = command.ExecuteReader();

                while (await reader.ReadAsync(token))
                {
                    agents.Add(await ReadAgent(reader, token));
                }
            }
            return agents;
        }

        public static async Task<List<Agent>> Search_ByPositionId(int posId, CancellationToken token)
        {
            List<Agent> agents = new();

            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string search = $@"SELECT * FROM contora.agent
                        WHERE {AgentColumns.position_id} = @searchValue";
                MySqlCommand command = new(search, conn);
                command.Parameters.AddWithValue("@searchValue", posId);
                MySqlDataReader reader = command.ExecuteReader();

                while (await reader.ReadAsync(token))
                {
                    agents.Add(await ReadAgent(reader, token));
                }
            }
            return agents;
        }
        
        public static async Task<List<Agent>> Search_ByStatusId(int statusId, CancellationToken token)
        {
            List<Agent> agents = new();

            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string search = $@"SELECT * FROM contora.agent
                        WHERE {AgentColumns.status_id} = @searchValue";
                MySqlCommand command = new(search, conn);
                command.Parameters.AddWithValue("@searchValue", statusId);
                MySqlDataReader reader = command.ExecuteReader();

                while (await reader.ReadAsync(token))
                {
                    agents.Add(await ReadAgent(reader, token));
                }
            }
            return agents;
        }

        private static async Task<Agent> ReadAgent (MySqlDataReader reader, CancellationToken token)
        {
            string? phone = null;
            string? address = null;

            if (!await reader.IsDBNullAsync((int)AgentColumns.phone, token))
            {
                phone = reader.GetString(AgentColumns.phone.ToString());
            }
            if (!await reader.IsDBNullAsync((int)AgentColumns.address, token))
            {
                address = reader.GetString(AgentColumns.phone.ToString());
            }

            return new Agent
                (
                reader.GetInt32(AgentColumns.id.ToString()),
                reader.GetString(AgentColumns.first_name.ToString()),
                reader.GetString(AgentColumns.last_name.ToString()),
                reader.GetString(AgentColumns.middle_name.ToString()),
                reader.GetInt32(AgentColumns.department_id.ToString()),
                reader.GetInt32(AgentColumns.position_id.ToString()),
                reader.GetInt32(AgentColumns.rank_id.ToString()),
                reader.GetInt32(AgentColumns.status_id.ToString()),
                phone,
                address
                );
        }

        public static async Task Update (Agent agent, CancellationToken token)
        {
            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string update = @"UPDATE `contora`.`agent`
                    SET
                    `first_name` = @fName,
                    `last_name` = @lName,
                    `middle_name` = @mName,
                    `department_id` = @depId,
                    `position_id` = @positionId,
                    `rank_id` = @rankId,
                    `status_id` = @statusId,
                    `phone` = @phone,
                    `address` = @address
                    WHERE `id` = @id;";
                MySqlCommand command = new(update, conn);
                command.Parameters.AddWithValue("@id", agent.ID);
                command.Parameters.AddWithValue("@fName", agent.FirstName);
                command.Parameters.AddWithValue("@lName", agent.LastName);
                command.Parameters.AddWithValue("@mName", agent.MiddleName);
                command.Parameters.AddWithValue("@depId", agent.DepartmentID);
                command.Parameters.AddWithValue("@positionId", agent.PositionID);
                command.Parameters.AddWithValue("@rankId", agent.RankID);
                command.Parameters.AddWithValue("@statusId", agent.StatusID);
                command.Parameters.AddWithValue("@phone", agent.Phone);
                command.Parameters.AddWithValue("@address", agent.Address);
                await command.ExecuteNonQueryAsync(token);
            }
        }

        public static async Task Delete_Byid (int id, CancellationToken token)
        {
            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string delete = @"DELETE FROM `contora`.`agent`
                    WHERE `id` = @id;";
                MySqlCommand command = new(delete, conn);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync(token);
            }
        }
    }

    public enum AgentColumns
    {
        id = 0,
        first_name = 1,
        last_name = 2,
        middle_name = 3,
        department_id = 4,
        position_id = 5,
        rank_id = 6,
        status_id = 7,
        phone = 8,
        address = 9
    }
}