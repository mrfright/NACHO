namespace NACHO
{
    public class Addenda
    {
        public const uint RECORD_TYPE_LENGTH        = 1;
        public const uint ADDENDA_TYPE_LENGTH       = 2;
        public const uint PAYMENT_INFO_LENGTH       = 80;
        public const uint ADDENDA_SEQUENCE_LENGTH   = 4;
        public const uint ENTRY_SEQUENCE_LENGTH     = 7;

        public const string RECORD_TYPE = "7";

        public string RecordType;
        public string AddendaType;
        public string PaymentInfo;
        public string AddendaSequence;
        public string EntrySequence;

        public Addenda(string recordTypeParam,
            string addendaTypeParam,
            string paymentInfoParam,
            string addendaSequenceParam,
            string entrySequenceParam,
            out string addendaMessage)
        {
            addendaMessage = LengthCheck.CheckLength("Addenda Record Type", recordTypeParam, RECORD_TYPE_LENGTH);
            addendaMessage += ExpectedString.CheckString("Addenda Record Type", recordTypeParam, RECORD_TYPE);
            RecordType = recordTypeParam;

            addendaMessage += LengthCheck.CheckLength("Addenda Type", addendaTypeParam, ADDENDA_TYPE_LENGTH);
            AddendaType = addendaTypeParam;

            addendaMessage += LengthCheck.CheckLength("Payment Info", paymentInfoParam, PAYMENT_INFO_LENGTH);
            PaymentInfo = paymentInfoParam;

            addendaMessage += LengthCheck.CheckLength("Addenda Sequence", addendaSequenceParam, ADDENDA_SEQUENCE_LENGTH);
            AddendaSequence = addendaSequenceParam;

            addendaMessage += LengthCheck.CheckLength("Entry Sequence", entrySequenceParam, ENTRY_SEQUENCE_LENGTH);
            EntrySequence = entrySequenceParam;
        }
    }
}
