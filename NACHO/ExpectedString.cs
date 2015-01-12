using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NACHO
{
    public class ExpectedString
    {
        public static string CheckString(string paramName, string paramToCheck, string expectedValue)
        {
            string message = "";
            if (!paramToCheck.Equals(expectedValue))
            {
                message += "\n" + paramName + " value is '" + paramToCheck + "' when '" + expectedValue + "' was expected";
            }
            return message;
        }
    }
}
