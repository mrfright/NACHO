using System.Collections.Generic;
namespace NACHO
{
    public class Entry
    {
        public const int RECORD_TYPE_LENGTH                = 1;
        public const int TRANSACTION_CODE_LENGTH           = 2;
        public const int RECEIVING_DFI_LENGTH              = 8;
        public const int CHECK_DIGIT_LENGTH                = 1;
        public const int DFI_ACCOUNT_LENGTH                = 17;
        public const int AMOUNT_LENGTH                     = 10;
        public const int INDIVIDUAL_IDENTIFICATION_LENGTH  = 15;
        public const int INDIVIDUAL_NAME_LENGTH            = 22;
        public const int DISCRETIONARY_DATA_LENGTH         = 2;
        public const int ADDENDA_RECORD_LENGTH             = 1;
        public const int TRACE_NUMBER_LENGTH               = 15;

        public const string RECORD_TYPE = "6";

        public const string DEBIT_FOR_CHECKING = "27";
        public const string DEBIT_FOR_SAVINGS = "37";
        public string[] TRANSACTION_CODE_TYPES = {"22",
                                                  "23",
                                                  "24",
                                                  DEBIT_FOR_CHECKING,
                                                  "28",
                                                  "29",
                                                  "32",
                                                  "33",
                                                  "34",
                                                  DEBIT_FOR_SAVINGS,
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

        public List<Addenda> AddendaList = new List<Addenda>();

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
            foreach (char dfiChar in RDFI.Trim().ToCharArray())
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

            /*
            string expectedCheckDigit = GenerateCheckDigit(ReceivingDFI);
            if (!CheckDigit.Equals(expectedCheckDigit))
            {
                entryMessage += "\nCheck Digit is '" + CheckDigit + "' when '" + expectedCheckDigit + "' was expected";
            }*/

            entryMessage += LengthCheck.CheckLength("DFI Account", DFIAccount, DFI_ACCOUNT_LENGTH);
            entryMessage += ExpectedString.CheckAlphaNumericWithSpaces("DFI Account", DFIAccount);            
            entryMessage += LengthCheck.CheckLength("Amount", Amount, AMOUNT_LENGTH);
            entryMessage += ExpectedString.CheckNumericNoSpaces("Amount", Amount);
            entryMessage += LengthCheck.CheckLength("Individual Identification", IndividualIdentification, INDIVIDUAL_IDENTIFICATION_LENGTH);
            entryMessage += LengthCheck.CheckLength("Individual Name", IndividualName, INDIVIDUAL_NAME_LENGTH);
            entryMessage += LengthCheck.CheckLength("Discretionary Data", DiscretionaryData, DISCRETIONARY_DATA_LENGTH);
            entryMessage += LengthCheck.CheckLength("Addenda Record", AddendaRecord, ADDENDA_RECORD_LENGTH);

            entryMessage += ExpectedString.CheckString("Addenda Record", AddendaRecord, new string[] { "0", "1" });

            if (EntryPrinter.PrintEntry(this).Length != 94)
            {
                entryMessage += "\nEntry does not contain 94 characters: '" + EntryPrinter.PrintEntry(this) + "'";
            }

            if (AddendaRecord.Equals("0") && AddendaList != null && AddendaList.Count > 0)
            {
                entryMessage += "\nAddenda Record is '0' when there are addenda records when none were expected";
            }
            else if (AddendaRecord.Equals("1") && (AddendaList == null || AddendaList.Count <= 0))
            {
                entryMessage += "\nAddenda Record is '1' when there are no addedna records when at least one was expected";
            }

            int expectedAddendaSequence = 1;

            if (AddendaList != null)
            {
                foreach (Addenda addenda in AddendaList)
                {
                    string addendaVerify = addenda.Verify();
                    if (!string.IsNullOrEmpty(addendaVerify))
                    {
                        entryMessage += "\n" + addendaVerify;
                    }

                    int addendaSequence = -1;
                    if (int.TryParse(addenda.AddendaSequence, out addendaSequence))
                    {
                        if (expectedAddendaSequence != int.Parse(addenda.AddendaSequence))
                        {
                            entryMessage += "\nAddenda has incorrect addenda sequence when " + expectedAddendaSequence.ToString()
                                + " was expected: " + AddendaPrinter.PrintAddendaVerbose(addenda);
                        }
                    }

                    if (!TraceNumber.Substring(8, 7).Equals(addenda.EntrySequence))
                    {
                        entryMessage += "\nAddenda did not have expected entry sequence: " + AddendaPrinter.PrintAddendaVerbose(addenda);
                    }
                }
            }

            entryMessage += LengthCheck.CheckLength("Trace Number", TraceNumber, TRACE_NUMBER_LENGTH);

            if (!string.IsNullOrEmpty(entryMessage))
            {
                entryMessage = "Errors in Entry with " + EntryPrinter.PrintEntryMembers(this) + ": " + entryMessage;
            }

            return entryMessage;
        }

        public void AddNewAddenda(string addendaType, string paymentInfo)
        {
            AddAddendaToList(Addenda.CreateAddenda(addendaType, paymentInfo));
        }

        public void AddAddendaToList(Addenda addenda)
        {
            AddendaList.Add(addenda);
            AutoGenValues();
        }

        public void AddNewPPDAddenda(string paymentInfo)
        {
            AddAddendaToList(Addenda.CreatePPDAddenda(paymentInfo));
        }

        public void AutoGenValues()
        {
            if (AddendaList != null && AddendaList.Count > 0)
            {
                AddendaRecord = "1";
            }
            else
            {
                AddendaRecord = "0";
            }

            int addendaSequence = 1;
            if (AddendaList != null)
            {
                foreach (Addenda addenda in AddendaList)
                {
                    addenda.AddendaSequence = addendaSequence++.ToString().PadLeft(4, '0');
                    addenda.EntrySequence = TraceNumber.Substring(8, 7);
                }
            }

            //this is now the 9th digit of the callers passed in recieving dfi
            //CheckDigit = Entry.GenerateCheckDigit(ReceivingDFI);
        }

        public decimal GetAmountDecimal()
        {
            return ((decimal)long.Parse(Amount)) / 100;
        }

        public static Entry CreateEntry(
            string transactionCode, 
            string receivingDFI, 
            string dfiAccount, 
            string amount, 
            string individualID, 
            string individualName,
            string discretionaryData)
        {

            if(individualID.Length > INDIVIDUAL_IDENTIFICATION_LENGTH) 
            {
                individualID = individualID.Substring(0, INDIVIDUAL_IDENTIFICATION_LENGTH);
            }

            if (individualName.Length > INDIVIDUAL_NAME_LENGTH)
            {
                individualName = individualName.Substring(0, INDIVIDUAL_NAME_LENGTH);
            }

            if (discretionaryData.Length > DISCRETIONARY_DATA_LENGTH)
            {
                discretionaryData = discretionaryData.Substring(0, DISCRETIONARY_DATA_LENGTH);
            }

            string checkDigit = "";
            if (receivingDFI.Length >= 9)
            {
                checkDigit = receivingDFI.Trim().ToCharArray()[8].ToString();
                receivingDFI = receivingDFI.Substring(0, 8);
            }

            Entry entry = new Entry(
                "6",
                transactionCode,
                receivingDFI,
                checkDigit,
                dfiAccount,
                amount.PadLeft(AMOUNT_LENGTH, '0'),
                individualID.PadLeft(INDIVIDUAL_IDENTIFICATION_LENGTH),
                individualName.PadLeft(INDIVIDUAL_NAME_LENGTH),
                discretionaryData.PadLeft(DISCRETIONARY_DATA_LENGTH),
                "",
                "");

            entry.AutoGenValues();
            return entry;
        }

        public static Entry CreateEntryDecimal(
            string transactionCode,
            string receivingDFI,
            string dfiAccount,
            decimal amount,
            string individualID,
            string individualName,
            string discretionaryData)
        {
            return CreateEntry(
                    transactionCode,
                    receivingDFI,
                    dfiAccount,
                    ((long)(amount*100)).ToString(),
                    individualID,
                    individualName,
                    discretionaryData
                );
        }     
    }
}
