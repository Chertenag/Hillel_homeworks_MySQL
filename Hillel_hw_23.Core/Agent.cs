namespace Hillel_hw_23.Core
{
    public class Agent
    {
        public static void AddNew(string fName, string lName, string mName, int depId, int positionId, int rankId, string phone, string address)
        {
            Data.Agent.AddNew(
                fName == string.Empty ? throw new ArgumentException("Name can`t be empty.") :
                    fName.Length > 50 ? throw new ArgumentException("Name must be below 50 symbols.") : fName,
                lName == string.Empty ? throw new ArgumentException("Last name can`t be empty.") :
                    lName.Length > 50 ? throw new ArgumentException("Last name must be below 50 symbols.") : lName,
                mName == string.Empty ? null :
                    mName.Length > 50 ? throw new ArgumentException("Middle name must be below 50 symbols.") : mName,
                depId,
                positionId,
                rankId,
                phone == string.Empty ? null : phone,
                address == string.Empty ? null :
                    address.Length > 100 ? throw new ArgumentException("Address must be below 100 symbols.") : address);
        }
    }
}