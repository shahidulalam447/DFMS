using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firm.Utility.Miscellaneous
{
    public class Enum
    {
        public enum Gender
        {
            Male = 1,
            Female = 2
        }
        public enum LivestockType
        {
            Ox = 1,
            Cow= 2,
            Heifer = 3,
            Calf = 4
        }
        public enum Shift
        {
            Morning = 1,
            Day,
            Evening
        }

        public enum PaymentMode
        {
            Bank = 1,
            Cash
        }

        public enum ExpenseType
        {
            LaberPayment = 1,
            Electricity = 2,
            UtilityBills = 3,
            AnnualFunction = 4,
            Feeding = 5,
            ShedCleaning = 6,
            Salary = 7,
            Bonus = 8,
            Others
        }
        public enum GradeEnum
        {
            Grade01 = 1,
            Grade02,
            Grade03,
            Grade04,
            Grade05,
            Grade06
        }
    }
}
