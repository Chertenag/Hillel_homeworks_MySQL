using Hillel_hw_25.EFData;

namespace Hillel_hw_25.EFCore
{
    public class Target
    {
        //Никаких проверок логики не добавлял как с Hillel_hw_23.Core.Agent
        public Target(int id, string firstName, string lastName, string? middleName, int caseId, string? phone, DateOnly? birthdate, string? address, string? additionalInfo)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            MiddleName = middleName;
            CaseId = caseId;
            Phone = phone;
            Birthdate = birthdate;
            Address = address;
            AdditionalInfo = additionalInfo;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public int CaseId { get; set; }
        public string? Phone { get; set; }
        public DateOnly? Birthdate { get; set; }
        public string? Address { get; set; }
        public string? AdditionalInfo { get; set; }

        //Не знаю куда вообще промапить EFData.Case.
        //Тут скорее всего надо исходить из того, нужен ли "Case" вообще нам в дальнейшей работе с объектом "Target".
        //public virtual EFData.Case Case { get; set; } = null!;

        public static async Task Create_async(int id, string fName, string lName, string mName,
            int caseId, string phone, string bDay, string address, string addInfo, CancellationToken token)
        {
            //Проверки...
            await EFData.Target.Create_async (id, fName, lName, mName, caseId, phone, DateOnly.Parse(bDay), address, addInfo, token);
        }

        public static async Task<List<EFCore.Target>> Read_ById_async(int id, CancellationToken token)
        {
            var rez = await EFData.Target.Read_ById_async(id, token);
            return rez.Select(MapperFromEFData).ToList();
        }

        public static async Task Update_async (EFCore.Target target, CancellationToken token)
        {
            await EFData.Target.Update_async(MapperToEFData(target), token);
        }

        public static async Task Delete_byId_async (int id, CancellationToken token)
        {
            await EFData.Target.Delete_byId_async(id, token);
        }

        private static EFCore.Target MapperFromEFData (EFData.Target target)
        {
            return new Target(target.Id, target.FirstName, target.LastName, target.MiddleName, 
                target.CaseId, target.Phone, target.Birthdate, target.Address, target.AdditionalInfo);
        }

        //По аналогии с Hillel_hw_23.Core.Agent тут тоже можно накидать проверки.
        private static EFData.Target MapperToEFData (EFCore.Target target)
        {
            return new EFData.Target(target.Id, target.FirstName, target.LastName, target.MiddleName,
                target.CaseId, target.Phone, target.Birthdate, target.Address, target.AdditionalInfo);
        }

    }
}