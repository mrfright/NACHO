using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NACHO
{
    public class EntryParser
    {
        public static Entry ParseEntry(string entryString)
        {
            string recordType               = entryString.Substring(0, 1);
            string transactionCode          = entryString.Substring(1, 2);
            string receivingDFI             = entryString.Substring(3, 8);
            string checkDigit               = entryString.Substring(11, 1);
            string DFIAccount               = entryString.Substring(12, 17);
            string amount                   = entryString.Substring(29, 10);
            string individualIdentification = entryString.Substring(39, 15);
            string individualName           = entryString.Substring(54, 22);
            string discretionary            = entryString.Substring(76, 2);
            string addenda                  = entryString.Substring(78, 1);
            string trace                    = entryString.Substring(79, 15);

            return new Entry(recordType,
                transactionCode,
                receivingDFI,
                checkDigit,
                DFIAccount,
                amount,
                individualIdentification,
                individualName,
                discretionary,
                addenda,
                trace);
        }
    }
}
