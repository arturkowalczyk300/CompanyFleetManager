using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager.Models
{
    public class PhoneNumber
    {
        public int? CountryCode { get; set; } = 48;
        public int? MainNumber { get; set; }

        public PhoneNumber(int? CountryCode, int? MainNumber)
        {
            this.CountryCode = CountryCode;
            this.MainNumber = MainNumber;
        }

        public override string ToString()
        {
            if (CountryCode == null || MainNumber == null) return "INVALID";

            return $"+{CountryCode}{MainNumber}";
        }

        public static PhoneNumber ParseString(string str)
        {
            PhoneNumber phoneNumber;

            try
            {
                phoneNumber = new PhoneNumber(
                   int.Parse(str.Substring(1, 2)),
                   int.Parse(str.Substring(3)));
            }
            catch (Exception e)
            {
                phoneNumber = new PhoneNumber(null, null); //invalid phone number
            }

            return phoneNumber;
        }
    }
}
