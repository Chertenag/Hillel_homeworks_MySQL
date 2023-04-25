namespace Hillel_hw_23.Core
{
    public static class Settings
    {
        public static string ConnectionStr
        {
            get => Data.Settings.ConnectionStr;
            set => Data.Settings.ConnectionStr = value;
        }
    }

}