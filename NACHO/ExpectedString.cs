using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NACHO
{
    public class ExpectedString
    {
        public static string CheckString(string paramName, string paramToCheck, string[] expectedValues)
        {
            string message = "";
            bool inList = false;
            foreach(string value in expectedValues)
            {
                if(paramToCheck.Equals(value))
                {
                    inList = true;
                    break;
                }
            }

            if (!inList)
            {
                message += "\n" + paramName + " was not an expected value (";
                string prevString = "";
                foreach (string value in expectedValues)
                {
                    message += prevString + value;
                    prevString = ", ";
                }
                message += "): " + paramToCheck;
            }
            return message;
        }

        public static string CheckNumericNoSpaces(string paramName, string paramToCheck)
        {
            string message = "";
            foreach (char c in paramToCheck)
            {
                if (!Char.IsDigit(c))
                {
                    message += "\n" + paramName + " contains a non-digit when all digits (no spaces) was expected: " + paramToCheck;
                    break;
                }
            }

            return message;
        }

        public static string CheckNumericWithSpaces(string paramName, string paramToCheck)
        {
            string message = "";

            foreach (char c in paramToCheck)
            {
                if (!(Char.IsDigit(c) || Char.IsWhiteSpace(c)))
                {
                    message += "\n" + paramName + " contains non-digit, non-space when only digits or spaces were expected: " + paramToCheck;
                    break;
                }
            }

            return message;
        }

        public static string CheckAlphaNumericNoSpaces(string paramName, string paramToCheck)
        {
            string message = "";
            foreach (char c in paramToCheck)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    message += "\n" + paramName + " contains a non-alpha-numeric when all alpha-numerics (no spaces) was expected: " + paramToCheck;
                    break;
                }
            }

            return message;
        }

        public static string CheckAlphaNumericWithSpaces(string paramName, string paramToCheck)
        {
            string message = "";

            foreach (char c in paramToCheck)
            {
                if (!(Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c)))
                {
                    message += "\n" + paramName + " contains non-alpha-numeric, non-space when only alpha-numerics or spaces were expected: " + paramToCheck;
                    break;
                }
            }

            return message;
        }
    }
}
