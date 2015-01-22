namespace NACHO
{
    public class Entry
    {
        public const uint RECORD_TYPE_LENGTH                = 1;
        public const uint TRANSACTION_CODE_LENGTH           = 2;
        public const uint RECEIVING_DFI_LENGTH              = 8;
        public const uint CHECK_DIGIT_LENGTH                = 1;
        public const uint DFI_ACCOUNT_LENGTH                = 17;
        public const uint AMOUNT_LENGTH                     = 10;
        public const uint INDIVIDUAL_IDENTIFICATION_LENGTH  = 15;
        public const uint INDIVIDUAL_NAME_LENGTH            = 22;
        public const uint DISCRETIONARY_DATA_LENGTH         = 2;
        public const uint ADDENDA_RECORD_LENGTH             = 1;
        public const uint TRACE_NUMBER_LENGTH               = 15;

        public const string RECORD_TYPE = "6";

        public string RecordType;
        public string TransactionCode;
        public string ReceivingDFI;
        public string CheckDigit;
        public string DFIAccount;
        public string Amount;
        public string IndividualIdentification;
        public string IndividualName;
        public string DiscretionaryData;
        public string AddendaRecord;
        public string TraceNumber;

        public Addenda Addenda;

        public Entry(string recordTypeParam,
            string transactionCodeParam,
            string receivingDFIParam,
            string checkDigitParam,
            string DFIAccountParam,
            string amountParam,
            string individualIdentificationParam,
            string individualNameParam,
            string discretionaryDataParam,
            string addendaRecordParam,
            string traceNumberParam,
            out string entryMessage)
        {
            entryMessage = "";

            entryMessage += LengthCheck.CheckLength("Entry Record Type", recordTypeParam, RECORD_TYPE_LENGTH);
            entryMessage += ExpectedString.CheckString("Entry Record Type", recordTypeParam, RECORD_TYPE);
            RecordType = recordTypeParam;

            entryMessage += LengthCheck.CheckLength("Transaction Code", transactionCodeParam, TRANSACTION_CODE_LENGTH);
            TransactionCode = transactionCodeParam;

            entryMessage += LengthCheck.CheckLength("Receiving DFI", receivingDFIParam, RECEIVING_DFI_LENGTH);
            ReceivingDFI = receivingDFIParam;

            entryMessage += LengthCheck.CheckLength("Check Digit", checkDigitParam, CHECK_DIGIT_LENGTH);
            CheckDigit = checkDigitParam;

            entryMessage += LengthCheck.CheckLength("DFI Account", DFIAccountParam, DFI_ACCOUNT_LENGTH);
            DFIAccount = DFIAccountParam;

            entryMessage += LengthCheck.CheckLength("Amount", amountParam, AMOUNT_LENGTH);
            Amount = amountParam;

            entryMessage += LengthCheck.CheckLength("Individual Identification", individualIdentificationParam, INDIVIDUAL_IDENTIFICATION_LENGTH);
            IndividualIdentification = individualIdentificationParam;

            entryMessage += LengthCheck.CheckLength("Individual Name", individualNameParam, INDIVIDUAL_NAME_LENGTH);
            IndividualName = individualNameParam;

            entryMessage += LengthCheck.CheckLength("Discretionary Data", discretionaryDataParam, DISCRETIONARY_DATA_LENGTH);
            DiscretionaryData = discretionaryDataParam;

            entryMessage += LengthCheck.CheckLength("Addenda Record", addendaRecordParam, ADDENDA_RECORD_LENGTH);
            AddendaRecord = addendaRecordParam;

            entryMessage += LengthCheck.CheckLength("Trace Number", traceNumberParam, TRACE_NUMBER_LENGTH);
            TraceNumber = traceNumberParam;
        }
    }
}
