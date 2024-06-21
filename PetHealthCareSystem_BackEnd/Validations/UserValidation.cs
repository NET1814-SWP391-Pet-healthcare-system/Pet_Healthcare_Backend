using System.Text.RegularExpressions;

namespace PetHealthCareSystem_BackEnd.Validations
{
    public static class UserValidation
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0\d{9,10})$";
            Regex regex = new Regex(pattern);

            if(!regex.IsMatch(phoneNumber))
            {
                return false;
            }
            return true;
        }
    }
}
