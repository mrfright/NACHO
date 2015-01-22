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

            if (entry.Addenda != null)
            {
                entryStr += "\n" + AddendaPrinter.PrintAddenda(entry.Addenda);
            }

            return entryStr;
        }
    }
}
