using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFleetManager.Models
{
    public class PhoneNumber
    {
        public int CountryCode = 48;
        public int MainNumber;

        public PhoneNumber(int CountryCode, int MainNumber)
        {
            this.CountryCode = CountryCode;
            this.MainNumber = MainNumber;
        }

        public override string ToString()
        {
            return $"+{CountryCode}{MainNumber}";
        }
    }
}
