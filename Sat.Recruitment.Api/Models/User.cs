using System;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

        public static User Map(UserRequest source)
        {
            return new User()
            {
                Name = source.Name,
                Email = GetNormalizedEmail(source.Email),
                Address = source.Address,
                Phone = source.Phone,
                UserType = source.UserType,
                Money = GetUpdatedMoney(source.UserType, decimal.Parse(source.Money))
            };
        }

        private static decimal GetUpdatedMoney(string userType, decimal money)
        {
            var percentage = userType == "Normal" ? money > 100 ? Convert.ToDecimal(0.12) : money < 100 && money > 10 ? Convert.ToDecimal(0.8) : 0
                : userType == "SuperUser" ? money > 100 ? Convert.ToDecimal(0.2) : 0
                : userType == "Premium" ? money > 100 ? Convert.ToDecimal(2) : 0 : 0;

            return money * (1 + percentage);
        }

        private static string GetNormalizedEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return email;

            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
