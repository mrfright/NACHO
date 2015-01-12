using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NACHO
{
    /// <summary>
    /// Check the length of a parameter and return a message if not the correct length
    /// </summary>
    public class LengthCheck
    {
        /// <summary>
        /// Check the length of a parameter and return a message if not the correct length
        /// </summary>
        /// <param name="paramName">text of the parameter name to print in the message</param>
        /// <param name="paramToCheck">value of the parameter to check</param>
        /// <param name="expectedLength">expected length to check the parameter against</param>
        /// <returns>Returns an empty string if the expected length, returns a message if not</returns>
        public static string CheckLength(string paramName, string paramToCheck, uint expectedLength)
        {
            string message = "";

            if (paramToCheck.Length != expectedLength)
            {
                message = "\n" + paramName + " length was " + paramToCheck.Length.ToString() + " when it was expected to be " + expectedLength.ToString();
            }

            return message;
        }
    }
}
