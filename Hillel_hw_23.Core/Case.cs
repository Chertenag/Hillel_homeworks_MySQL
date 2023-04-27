namespace Hillel_hw_23.Core
{
    public class Case
    {
        private int depId;
        private int primId;
        private int? secId;
        private DateOnly dateOpen;
        private DateOnly? dateClosed;


        public int DepartmentID { get => depId; set => depId = value; }
        public int PrimaryAgentID { get => primId; set => primId = value; }
        public int? SecondaryAgentID { get => secId; set => secId = value; }
        public DateOnly DateOpen { get => dateOpen; set => dateOpen = value; }

        /// <summary>
        /// Дата закрытия дела. Возвращает Null, если дело ещё не закрыто.
        /// </summary>
        public DateOnly? DateClosed { get => dateClosed; set => dateClosed = value; }

        private Case(int depId, int primAgentId, int? secAgentId, DateOnly open, DateOnly? close)
        {
            this.depId = depId;
            this.primId = primAgentId;
            this.secId = secAgentId;
            this.dateOpen = open;
            this.dateClosed = close;
        }

        public static async Task AddNew(int depId, int primAgentId, int secAgentId, string dateOpen, string dateClose, CancellationToken token)
        {
            string mappedDate = DateMapper.Convert(dateOpen);
            if (mappedDate == null)
            {
                throw new ArgumentException("DateOpen is required parameter.");
            }

            await Data.Case.AddNew(
                depId,
                primAgentId,
                secAgentId < 0 ? null : secAgentId,
                mappedDate,
                DateMapper.Convert(dateClose),
                token);
        }

        public static async Task<List<Case>> Search_ByDepartmentId(int depId, CancellationToken token)
        {
            var rez = await Data.Case.Search_ByDepartmentId(depId, token);
            return rez.Select(Mapper).ToList();
        }

        private static Case Mapper(Data.Case coreCase)
        {
            return new Case(coreCase.DepartmentID, coreCase.PrimaryAgentID, coreCase.SecondaryAgentID, coreCase.DateOpen, coreCase.DateClosed);
        }
    }
}

