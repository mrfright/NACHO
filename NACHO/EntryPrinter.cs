namespace NACHO
{
    public class EntryPrinter
    {
        public static string PrintEntry(Entry entry)
        {
            string entryStr = entry.RecordType
                + entry.TransactionCode
                + entry.ReceivingDFI
                + entry.CheckDigit
                + entry.DFIAccount
                + entry.Amount
                + entry.IndividualIdentification
                + entry.IndividualName
                + entry.DiscretionaryData
                + entry.AddendaRecord
                + entry.TraceNumber;

            if (entry.AddendaList != null && entry.AddendaList.Count > 0)
            {
                foreach (Addenda addendum in entry.AddendaList)
                {
                    entryStr += "\n" + AddendaPrinter.PrintAddenda(addendum);
                }
            }

            return entryStr;
        }

        public static string PrintEntryMembers(Entry entry)
        {
            return "Record Type='" + entry.RecordType +
                "' Transaction Code='" + entry.TransactionCode +
                "' Receieving DFI='" + entry.ReceivingDFI +
                "' Check Digit='" + entry.CheckDigit +
                "' DFI Account='" + entry.DFIAccount +
                "' Amount='" + entry.Amount +
                "' Individual Identification='" + entry.IndividualIdentification +
                "' Individual Name='" + entry.IndividualName +
                "' Discretionary Data='" + entry.DiscretionaryData +
                "' Addenda Record='" + entry.AddendaRecord +
                "' Trace Number='" + entry.TraceNumber + "'";
        }
    }
}
