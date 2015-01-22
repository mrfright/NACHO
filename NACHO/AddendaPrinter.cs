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
    }
}
