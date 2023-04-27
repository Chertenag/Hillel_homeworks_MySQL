using MySql.Data.MySqlClient;

namespace Hillel_hw_23.Data
{
    public class Case
    {
        private static readonly string depIdColumn = "department_id";
        private static readonly string primAgentIdColumn = "primary_agent_id";
        private static readonly string secondaryAgentIdColumn = "secondary_agent_id";
        private static readonly string dateOpenColumn = "date_open";
        private static readonly string dateCloseColumn = "date_close";

        private int depId;
        private int primId;
        private int? secId;
        private DateOnly dateOpen;
        private DateOnly? dateClosed;


        public int DepartmentID { get => depId; set => depId = value; }
        public int PrimaryAgentID { get => primId; set => primId = value; }
        public int? SecondaryAgentID { get => secId; set => secId = value; }
        public DateOnly DateOpen { get => dateOpen; set => dateOpen = value; }
        public DateOnly? DateClosed { get => dateClosed; set => dateClosed = value; }

        private Case(int depId, int primAgentId, int? secAgentId, DateOnly open, DateOnly? close) 
        {
            this.depId = depId;
            this.primId = primAgentId;
            this.secId = secAgentId;
            this.dateOpen = open;
            this.dateClosed = close;
        }

        public static async Task AddNew(int depId, int primAgentId, int? secAgentId, string dateOpen, string? dateClose, CancellationToken token)
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
                await command.ExecuteNonQueryAsync();
            }
        }

        public static async Task<List<Case>> Search_ByDepartmentId(int depIdSearch, CancellationToken token)
        {
            List<Case> cases = new();

            using (MySqlConnection conn = new(Settings.ConnectionStr))
            {
                conn.Open();
                string search = @"SELECT * FROM contora.case
                    WHERE department_id = @depId";
                MySqlCommand command = new(search, conn);
                command.Parameters.AddWithValue("@depId", depIdSearch);
                MySqlDataReader reader = command.ExecuteReader();

                while (await reader.ReadAsync(token)) 
                {
                    int? secAgent = null;
                    DateOnly? closed = null;

                    if (!await reader.IsDBNullAsync(3, token))
                    {
                        secAgent = reader.GetInt32(secondaryAgentIdColumn);
                    }
                    if (!await reader.IsDBNullAsync(5, token))
                    {
                        closed = DateOnly.FromDateTime(reader.GetDateTime(dateCloseColumn));
                    }



                    //int? secAgent = reader.GetFieldValue<int?>(3);
                    //var rez = reader.GetFieldValue<DateTime?>(6);
                    //DateOnly? closed = rez.HasValue ? null : DateOnly.FromDateTime(rez.Value);

                    cases.Add(new Case
                        (
                        reader.GetInt32(depIdColumn),
                        reader.GetInt32(primAgentIdColumn),
                        secAgent,
                        DateOnly.FromDateTime(reader.GetDateTime(dateOpenColumn)),
                        closed
                        ));
                }
            }
            return cases;
        }
    }
}