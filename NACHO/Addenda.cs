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
        public string[] ADDENDA_TYPES = { "02", "05", "98", "99" };

        public string RecordType;
        public string AddendaType;
        public string PaymentInfo;
        public string AddendaSequence;
        public string EntrySequence;

        public Addenda(string recordTypeParam,
            string addendaTypeParam,
            string paymentInfoParam,
            string addendaSequenceParam,
            string entrySequenceParam)
        {            
            RecordType      = recordTypeParam;
            AddendaType     = addendaTypeParam;
            PaymentInfo     = paymentInfoParam;
            AddendaSequence = addendaSequenceParam;
            EntrySequence   = entrySequenceParam;
        }

        public string Verify()
        {
            string addendaMessage = "";

            addendaMessage = LengthCheck.CheckLength("Addenda Record Type", RecordType, RECORD_TYPE_LENGTH);
            addendaMessage += ExpectedString.CheckString("Addenda Record Type", RecordType, new string[]{RECORD_TYPE});
            addendaMessage += LengthCheck.CheckLength("Addenda Type", AddendaType, ADDENDA_TYPE_LENGTH);

            addendaMessage += ExpectedString.CheckString("Addenda Type", AddendaType, ADDENDA_TYPES);

            addendaMessage += LengthCheck.CheckLength("Payment Info", PaymentInfo, PAYMENT_INFO_LENGTH);
            addendaMessage += LengthCheck.CheckLength("Addenda Sequence", AddendaSequence, ADDENDA_SEQUENCE_LENGTH);
            addendaMessage += LengthCheck.CheckLength("Entry Sequence", EntrySequence, ENTRY_SEQUENCE_LENGTH);

            addendaMessage += ExpectedString.CheckNumericWithSpaces("Addenda Sequence", AddendaSequence);
            addendaMessage += ExpectedString.CheckNumericWithSpaces("Entry Detail Sequence", EntrySequence);

            if (AddendaPrinter.PrintAddenda(this).Length != 94)
            {
                addendaMessage += "\nAddenda is not 94 characters long: '" + AddendaPrinter.PrintAddenda(this) + "'";
            }

            if (!string.IsNullOrEmpty(addendaMessage))
            {
                addendaMessage = "Errors in Addenda with " + AddendaPrinter.PrintAddendaVerbose(this) + ": " + addendaMessage;
            }
            return addendaMessage;
        }

        public static Addenda CreateAddenda(string addendaType, string paymentInfo)
        {
            return new Addenda("7", addendaType, paymentInfo, "", "");
        }

        public static Addenda CreatePPDAddenda(string paymentInfo)
        {
            return CreateAddenda("05", paymentInfo);
        }
    }
}
