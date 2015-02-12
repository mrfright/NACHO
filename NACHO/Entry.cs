using System.Collections.Generic;
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
        public string[] TRANSACTION_CODE_TYPES = {"22",
                                                  "23",
                                                  "24",
                                                  "27",
                                                  "28",
                                                  "29",
                                                  "32",
                                                  "33",
                                                  "34",
                                                  "37",
                                                  "38",
                                                  "39"};

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

        public List<Addenda> AddendaList;

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
            string traceNumberParam)
        {
            RecordType                  = recordTypeParam;
            TransactionCode             = transactionCodeParam;
            ReceivingDFI                = receivingDFIParam;
            CheckDigit                  = checkDigitParam;
            DFIAccount                  = DFIAccountParam;
            Amount                      = amountParam;
            IndividualIdentification    = individualIdentificationParam;
            IndividualName              = individualNameParam;
            DiscretionaryData           = discretionaryDataParam;
            AddendaRecord               = addendaRecordParam;
            TraceNumber                 = traceNumberParam;
        }

        public static string GenerateCheckDigit(string RDFI)
        {
            int[] weights = {3, 7, 1};
            int weightIndex = 0;
            int sum = 0;
            foreach (char dfiChar in RDFI.ToCharArray())
            {
                sum += int.Parse(dfiChar.ToString()) * weights[weightIndex];
                weightIndex = (weightIndex + 1) % 3;
            }

            string checkDigit = ((10 - (sum % 10)) % 10).ToString();
            return checkDigit;
        }

        public string Verify()
        {
            string entryMessage = "";


            entryMessage += LengthCheck.CheckLength("Entry Record Type", RecordType, RECORD_TYPE_LENGTH);
            entryMessage += ExpectedString.CheckString("Entry Record Type", RecordType, new string[]{RECORD_TYPE});
            entryMessage += LengthCheck.CheckLength("Transaction Code", TransactionCode, TRANSACTION_CODE_LENGTH);
            entryMessage += ExpectedString.CheckString("Transaction Code", TransactionCode, TRANSACTION_CODE_TYPES);
            entryMessage += LengthCheck.CheckLength("Receiving DFI", ReceivingDFI, RECEIVING_DFI_LENGTH);
            entryMessage += ExpectedString.CheckNumericNoSpaces("Receiving DFI", ReceivingDFI);
            entryMessage += LengthCheck.CheckLength("Check Digit", CheckDigit, CHECK_DIGIT_LENGTH);

            string expectedCheckDigit = GenerateCheckDigit(ReceivingDFI);
            if (!CheckDigit.Equals(expectedCheckDigit))
            {
                entryMessage += "\nCheck Digit is '" + CheckDigit + "' when '" + expectedCheckDigit + "' was expected";
            }

            entryMessage += LengthCheck.CheckLength("DFI Account", DFIAccount, DFI_ACCOUNT_LENGTH);
            entryMessage += ExpectedString.CheckAlphaNumericWithSpaces("DFI Account", DFIAccount);            
            entryMessage += LengthCheck.CheckLength("Amount", Amount, AMOUNT_LENGTH);
            entryMessage += ExpectedString.CheckNumericNoSpaces("Amount", Amount);
            entryMessage += LengthCheck.CheckLength("Individual Identification", IndividualIdentification, INDIVIDUAL_IDENTIFICATION_LENGTH);
            entryMessage += LengthCheck.CheckLength("Individual Name", IndividualName, INDIVIDUAL_NAME_LENGTH);
            entryMessage += LengthCheck.CheckLength("Discretionary Data", DiscretionaryData, DISCRETIONARY_DATA_LENGTH);
            entryMessage += LengthCheck.CheckLength("Addenda Record", AddendaRecord, ADDENDA_RECORD_LENGTH);

            entryMessage += ExpectedString.CheckString("Addenda Record", AddendaRecord, new string[] { "0", "1" });

            if (AddendaRecord.Equals("0") && AddendaList != null && AddendaList.Count > 0)
            {
                entryMessage += "Addenda Record is '0' when there are addenda records when none were expected";
            }
            else if (AddendaRecord.Equals("1") && (AddendaList == null || AddendaList.Count <= 0))
            {
                entryMessage += "Addenda Record is '1' when there are no addedna records when at least one was expected";
            }

            int expectedAddendaSequence = 1;
            foreach (Addenda addenda in AddendaList)
            {
                //check addenda, append if any errors
                entryMessage += "\n" + addenda.Verify();
                int addendaSequence = -1;
                if (int.TryParse(addenda.AddendaSequence, out addendaSequence))
                {
                    if (expectedAddendaSequence != int.Parse(addenda.AddendaSequence))
                    {
                        entryMessage += "Addenda has incorrect addenda sequence when " + expectedAddendaSequence.ToString()
                            + " was expected: " + AddendaPrinter.PrintAddendaVerbose(addenda);
                    }
                }

                if (!TraceNumber.Substring(8, 7).Equals(addenda.EntrySequence))
                {
                    entryMessage += "Addenda did not have expected entry sequence: " + AddendaPrinter.PrintAddendaVerbose(addenda);
                }
            }

            entryMessage += LengthCheck.CheckLength("Trace Number", TraceNumber, TRACE_NUMBER_LENGTH);

            if (!string.IsNullOrEmpty(entryMessage))
            {
                entryMessage = "Errors in Entry with " + EntryPrinter.PrintEntryMembers(this) + ": " + entryMessage;
            }

            return entryMessage;
        }

        void AddNewAddenda(string addendaType, string paymentInfo)
        {
            AddAddendaToList(Addenda.CreateAddenda(addendaType, paymentInfo));
        }

        void AddAddendaToList(Addenda addenda)
        {
            //TODO add to list, update its sequence and entry trace number

        }

        void AddNewPPDAddenda(string paymentInfo)
        {
            AddAddendaToList(Addenda.CreatePPDAddenda(paymentInfo));
        }
    }
}
