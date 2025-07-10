namespace GospelReachCapstone.Services
{
    public class GeneralFunctions
    {
        // General Functions
        public bool CheckValidString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return true;
        }
    }
}
