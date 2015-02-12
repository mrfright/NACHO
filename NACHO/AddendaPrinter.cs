namespace NACHO
{
    public class AddendaPrinter
    {
        public static string PrintAddenda(Addenda addenda)
        {
            return addenda.RecordType
                + addenda.AddendaType
                + addenda.PaymentInfo
                + addenda.AddendaSequence
                + addenda.EntrySequence;
        }

        public static string PrintAddendaVerbose(Addenda addenda)
        {
            return  "Record Type='" + addenda.RecordType +
                    "' Addenda Type='" + addenda.AddendaType +
                    "' PaymentInfo='" + addenda.PaymentInfo +
                    "' Addenda Sequence='" + addenda.AddendaSequence +
                    "' Entry Sequence='" + addenda.EntrySequence + "'";
        }
    }
}
