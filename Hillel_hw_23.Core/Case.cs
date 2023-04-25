namespace Hillel_hw_23.Core
{
    public class Case
    {
        public static void AddNew(int depId, int primAgentId, int secAgentId, string dateOpen, string dateClose)
        {
            string mappedDate = DateMapper.Convert(dateOpen);
            if (mappedDate == null)
            {
                throw new ArgumentException("DateOpen is required parameter.");
            }

            Data.Case.AddNew(
                depId,
                primAgentId,
                secAgentId < 0 ? null : secAgentId,
                mappedDate,
                DateMapper.Convert(dateClose));
        }
    }
}