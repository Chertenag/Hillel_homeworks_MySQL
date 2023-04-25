namespace Hillel_hw_23.Core
{
    public class Target
    {
        public static void AddNew(string fName, string lName, string mName, int caseId, string phone, string bday, string address, string info)
        {
            Data.Target.AddNew(
                fName == string.Empty ? throw new ArgumentException("Name can`t be empty.") : 
                    fName.Length > 50 ? throw new ArgumentException("Name must be below 50 symbols.") : fName,
                lName == string.Empty ? throw new ArgumentException("Last name can`t be empty.") :
                    lName.Length > 50 ? throw new ArgumentException("Last name must be below 50 symbols.") : lName,
                mName == string.Empty ? null : 
                    mName.Length > 50 ? throw new ArgumentException("Middle name must be below 50 symbols.") : mName,
                caseId,
                phone == string.Empty ? null : phone, 
                DateMapper.Convert(bday),
                address == string.Empty ? null : 
                    address.Length > 100 ? throw new ArgumentException("Address must be below 100 symbols.") : address,
                info == string.Empty ? null : 
                    info.Length > 250 ? throw new ArgumentException("Additional info must be below 250 symbols.") : info
                );
        }
    }
}