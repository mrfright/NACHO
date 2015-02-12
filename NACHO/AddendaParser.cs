namespace NACHO
{
    public class AddendaParser
    {
        public static Addenda ParseAddenda(string addendaStr)
        {
            string recordType = addendaStr.Substring(0, 1);
            string addendaType = addendaStr.Substring(1, 2);
            string paymentInfo = addendaStr.Substring(3, 80);
            string addendaSequence = addendaStr.Substring(83, 4);
            string entrySequence = addendaStr.Substring(87, 7);

            return new Addenda(recordType,
                addendaType,
                paymentInfo,
                addendaSequence,
                entrySequence);
        }
    }
}
