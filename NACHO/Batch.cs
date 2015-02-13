using System.Collections.Generic;
using System.Collections;
using System;

namespace NACHO
{
    /// <summary>
    /// Batch record for ACH files
    /// <para>
    /// A Batch will have a header and control record information, and any number of Entry Detail records
    /// </para>
    /// </summary>
    public class Batch
    {
        public const uint HEADER_RECORD_TYPE_LENGTH         = 1;
        public const uint HEADER_SERVICE_CLASS_LENGTH       = 3;
        public const uint COMPANY_NAME_LENGTH               = 16;
        public const uint COMPANY_DISCRETIONARY_DATA_LENGTH = 20;
        public const uint HCOMPANY_IDENTIFICATION_LENGTH    = 10;
        public const uint STANDARD_ENTRY_LENGTH             = 3;
        public const uint COMPANY_ENTRY_LENGTH              = 10;
        public const uint COMPANY_DESCRIPTION_DATE_LENGTH   = 6;
        public const uint EFFECTIVE_ENTRY_DATE_LENGTH       = 6;
        public const uint SETTLEMENT_DATE_LENGTH            = 3;
        public const uint ORIGINATOR_STATUS_LENGTH          = 1;
        public const uint HEADER_ORIGINATOR_DFI_LENGTH      = 8;
        public const uint HEADER_BATCH_NUMBER_LENGTH        = 7;
        public const uint CONTROL_RECORD_TYPE_LENGTH        = 1;
        public const uint CONTROL_SERVICE_CLASS_LENGTH      = 3;
        public const uint ENTRY_COUNT_LENGTH                = 6;
        public const uint ENTRY_HASH_LENGTH                 = 10;
        public const uint TOTAL_DEBIT_LENGTH                = 12;
        public const uint TOTAL_CREDIT_LENGTH               = 12;
        public const uint CCOMPANY_IDENTIFICATION_LENGTH    = 10;
        public const uint MESSAGE_AUTH_LENGTH               = 19;
        public const uint RESERVED_LENGTH                   = 6;
        public const uint CONTROL_ORIGINATING_DFI_LENGTH    = 8;
        public const uint CONTROL_BATCH_NUMBER_LENGTH       = 7;

        public const string HEADER_RECORD_TYPE  = "5";
        public const string CONTROL_RECORD_TYPE = "8";

        
        public string[] SERVICE_CLASS_CODES = {"200",
                                               "220",
                                               "225"};
        //TODO maybe have new const for each of these, like MIXED_DEBIT_CREDIT = SERVICE_CLASS_CODES[0]

        public string[] STANDARD_ENTRY_CODES = {"PPD",
                                                "CCD",
                                                "CTX",
                                                "TEL",
                                                "WEB"};
        //TODO similar as TODO above

        public string HeaderRecordType;
        public string HeaderServiceClass;
        public string HeaderCompanyName;
        public string CompanyDiscretionary;
        public string HeaderCompanyIdentification;
        public string StandardEntry;
        public string CompanyEntry;
        public string CompanyDescriptionDate;
        public string EffectiveEntryDate;
        public string SettlementDate;
        public string OriginatorStatus;
        public string HeaderOriginatorDFI;
        public string HeaderBatchNumber;
        public string ControlRecordType;
        public string ControlServiceClass;
        public string EntryCount;
        public string EntryHash;
        public string TotalDebit;
        public string TotalCredit;
        public string ControlCompanyIdentification;
        public string MessageAuthentication;
        public string Reserved;
        public string ControlOriginatingDFI;
        public string ControlBatchNumber;

        public List<Entry> Entries = new List<Entry>();

        public void
        SetHeader(string headerRecordTypeParam,
            string headerServiceClassParam,
            string headerCompanyNameParam,
            string companyDiscretionaryPara,
            string headerCompanyIdentificationParam,
            string standardEntryParam,
            string companyEntryParam,
            string companyDescriptionDateParam,
            string effectiveEntryDateParam,
            string settlementDateParam,
            string originatorStatusParam,
            string headerOriginatorDFIParam,
            string headerBatchNumberParam)
        {            
            HeaderRecordType            = headerRecordTypeParam;
            HeaderServiceClass          = headerServiceClassParam;
            HeaderCompanyName           = headerCompanyNameParam;            
            CompanyDiscretionary        = companyDiscretionaryPara;            
            HeaderCompanyIdentification = headerCompanyIdentificationParam;            
            StandardEntry               = standardEntryParam;            
            CompanyEntry                = companyEntryParam;            
            CompanyDescriptionDate      = companyDescriptionDateParam;            
            EffectiveEntryDate          = effectiveEntryDateParam;            
            SettlementDate              = settlementDateParam;            
            OriginatorStatus            = originatorStatusParam;            
            HeaderOriginatorDFI         = headerOriginatorDFIParam;            
            HeaderBatchNumber           = headerBatchNumberParam;
        }

        public void
        SetControl(
            string controlRecordTypeParam,
            string controlServiceClassParam,
            string entryCountParam,
            string entryHashParam,
            string totalDebitParam,
            string totalCreditParam,
            string controlCompanyIdentificationParam,
            string messageAuthenticationParam,
            string reservedParam,
            string controlOriginatorDFIParam,
            string controlBatchNumberParam)
        {            
            ControlRecordType               = controlRecordTypeParam;            
            ControlServiceClass             = controlServiceClassParam;            
            EntryCount                      = entryCountParam;            
            EntryHash                       = entryHashParam;            
            TotalDebit                      = totalDebitParam;            
            TotalCredit                     = totalCreditParam;            
            ControlCompanyIdentification    = controlCompanyIdentificationParam;            
            MessageAuthentication           = messageAuthenticationParam;            
            Reserved                        = reservedParam;            
            ControlOriginatingDFI           = controlOriginatorDFIParam;            
            ControlBatchNumber              = controlBatchNumberParam;
        }

        
        public string Verify()
        {
            string messages = "";

            messages += LengthCheck.CheckLength("Header Record Type", HeaderRecordType, HEADER_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Header Record Type", HeaderRecordType, new string[] { HEADER_RECORD_TYPE });
            messages += LengthCheck.CheckLength("Header Service Class", HeaderServiceClass, HEADER_SERVICE_CLASS_LENGTH);
            messages += ExpectedString.CheckString("Header Service Class", HeaderServiceClass, SERVICE_CLASS_CODES);
            messages += LengthCheck.CheckLength("Header Company Name", HeaderCompanyName, COMPANY_NAME_LENGTH);
            messages += LengthCheck.CheckLength("Company Discretionary Data", CompanyDiscretionary, COMPANY_DISCRETIONARY_DATA_LENGTH);
            messages += LengthCheck.CheckLength("Header Company Identification", HeaderCompanyIdentification, HCOMPANY_IDENTIFICATION_LENGTH);
            messages += LengthCheck.CheckLength("Standard Entry", StandardEntry, STANDARD_ENTRY_LENGTH);
            messages += ExpectedString.CheckString("Standard Entry", StandardEntry, STANDARD_ENTRY_CODES);
            messages += LengthCheck.CheckLength("Company Entry", CompanyEntry, COMPANY_ENTRY_LENGTH);
            messages += LengthCheck.CheckLength("Company Description", CompanyDescriptionDate, COMPANY_DESCRIPTION_DATE_LENGTH);
            messages += LengthCheck.CheckLength("Effective Entry Date", EffectiveEntryDate, EFFECTIVE_ENTRY_DATE_LENGTH);
            messages += LengthCheck.CheckLength("Settlement Date", SettlementDate, SETTLEMENT_DATE_LENGTH);
            messages += LengthCheck.CheckLength("Originator Status", OriginatorStatus, ORIGINATOR_STATUS_LENGTH);
            messages += LengthCheck.CheckLength("Header Originator DFI", HeaderOriginatorDFI, HEADER_ORIGINATOR_DFI_LENGTH);
            messages += LengthCheck.CheckLength("Header Batch Number", HeaderBatchNumber, HEADER_BATCH_NUMBER_LENGTH);
            messages += LengthCheck.CheckLength("Control Record Type", ControlRecordType, CONTROL_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Control Record Type", ControlRecordType, new string[] { CONTROL_RECORD_TYPE });
            messages += LengthCheck.CheckLength("Control Service Class", ControlServiceClass, CONTROL_SERVICE_CLASS_LENGTH);
            messages += ExpectedString.CheckString("Control Service Class", ControlServiceClass, SERVICE_CLASS_CODES);
            messages += LengthCheck.CheckLength("Entry Count", EntryCount, ENTRY_COUNT_LENGTH);
            messages += LengthCheck.CheckLength("Entry Hash", EntryHash, ENTRY_HASH_LENGTH);
            messages += LengthCheck.CheckLength("Total Debit", TotalDebit, TOTAL_DEBIT_LENGTH);
            messages += LengthCheck.CheckLength("Total Credit", TotalCredit, TOTAL_CREDIT_LENGTH);
            messages += LengthCheck.CheckLength("Control Batch Number", ControlBatchNumber, CONTROL_BATCH_NUMBER_LENGTH);
            messages += LengthCheck.CheckLength("Control Company Identification", ControlCompanyIdentification, CCOMPANY_IDENTIFICATION_LENGTH);
            messages += LengthCheck.CheckLength("Message Authentication", MessageAuthentication, MESSAGE_AUTH_LENGTH);
            messages += LengthCheck.CheckLength("Reserved", Reserved, RESERVED_LENGTH);
            messages += LengthCheck.CheckLength("Control Originating DFI", ControlOriginatingDFI, CONTROL_ORIGINATING_DFI_LENGTH);

            if (!HeaderServiceClass.Equals(ControlServiceClass))
            {
                messages += "\nHeader service class of '" + HeaderServiceClass + 
                    "' is different than control service class '" + ControlServiceClass + "'";
            }

            if (!HeaderCompanyIdentification.Equals(ControlCompanyIdentification))
            {
                messages += "\nHeader company ID of '" + HeaderCompanyIdentification + 
                    "' is different than control company ID '" + ControlCompanyIdentification + "'";
            }

            if (!HeaderOriginatorDFI.Equals(ControlOriginatingDFI))
            {
                messages += "\nHeader originating DFI of '" + HeaderOriginatorDFI +
                    "' is different than the control originating DFI '" + ControlOriginatingDFI + "'";
            }

            if (!HeaderBatchNumber.Equals(ControlBatchNumber))
            {
                messages += "\nHeader batch number of '" + HeaderBatchNumber +
                    "' is different than the control batch number '" + ControlBatchNumber + "'";
            }

            int entrySequence = 1;
            foreach (Entry entry in Entries)
            {
                string entryMessage = entry.Verify();
                if (!string.IsNullOrEmpty(entryMessage))
                {
                    messages += "\n" + entryMessage;
                }

                string expectedEntryTrace = GenerateEntryTraceNumber(entrySequence);
                if (!expectedEntryTrace.Equals(entry.TraceNumber))
                {
                    messages += "\nEntry has trace number '" + entry.TraceNumber +
                        "' when '" + expectedEntryTrace + "' was expected: " +
                        EntryPrinter.PrintEntryMembers(entry);
                }

                entrySequence++;
            }

            int entryCount = 0;
            if (int.TryParse(EntryCount, out entryCount))
            {
                int actualEntryCount = EntryAddendaCount();
                if (entryCount != actualEntryCount)
                {
                    messages += "\nEntry/Addenda count was '" + EntryCount + "' when '" + actualEntryCount.ToString() + "' was expected";
                }
            }
            else
            {
                messages += "\nEntry count could not be parsed as a number: '" + EntryCount + "'";
            }

            string expectedHash = GenerateHash();
            if (!expectedHash.Equals(EntryHash))
            {
                messages += "\nEntry hash is '" + EntryHash + "' when '" + expectedHash + "' was expected";
            }

            string expectedTotalDebit = GenerateTotalDebit();
            if (!expectedTotalDebit.Equals(TotalDebit))
            {
                messages += "\nTotal debit is '" + TotalDebit + "' when '" + expectedTotalDebit + "' was expected";
            }

            string expectedTotalCredit = GenerateTotalCredit();
            if (!expectedTotalCredit.Equals(TotalCredit))
            {
                messages += "\nTotal credit is '" + TotalCredit + "' when '" + expectedTotalCredit + "' was expected";
            }

            if (!string.IsNullOrEmpty(messages))
            {
                messages = "Errors in batch: " + BatchPrinter.PrintBatchMembers(this) + ": " + messages;
            }

            return messages;
        }

        public string GenerateEntryTraceNumber(int entrySequence)
        {
            return HeaderOriginatorDFI + entrySequence.ToString().PadLeft(7, '0');
        }

        public int EntryAddendaCount()
        {
            int count = 0;
            foreach (Entry entry in Entries)
            {
                count++;
                count += entry.AddendaList.Count;
            }

            return count;
        }

        public string GenerateHash()
        {
            long batchHashSum = 0;
            foreach (Entry entry in Entries)
            {
                long trySum;
                if (long.TryParse(entry.ReceivingDFI, out trySum))
                {
                    batchHashSum += trySum;
                }
            }
            long batchHash = batchHashSum % 10000000000;
            return batchHash.ToString().PadLeft(10, '0');
        }


        public string GenerateTotalDebit()
        {
            int totalDebit = 0;
            foreach (Entry entry in Entries)
            {
                if (entry.TransactionCode.Equals("27") || entry.TransactionCode.Equals("37"))
                {
                    int tryDebit;
                    if (int.TryParse(entry.Amount, out tryDebit))
                    {
                        totalDebit += tryDebit;
                    }
                }
            }

            return totalDebit.ToString().PadLeft(12, '0');
        }

        public string GenerateTotalCredit()
        {            
            int totalCredit = 0;
            foreach (Entry entry in Entries)
            {
                //TODO not right, but right now only getting debits
                if (!(entry.TransactionCode.Equals("27") || entry.TransactionCode.Equals("37")))
                {
                    int tryCredit;
                    if (int.TryParse(entry.Amount, out tryCredit))
                    {
                        totalCredit += tryCredit;
                    }
                }
            }

            return totalCredit.ToString().PadLeft(12, '0');
        }

        public static Batch CreateBatch(
            string serviceClassCode,
            string companyName,
            string companyDiscretionary,
            string companyID,
            string standardEntry,
            string companyEntry,
            string companyDescriptiveDate,
            string effectiveEntryDate,
            string settlementDate,
            string originatingDFI,
            int batchNumber)
        {
            Batch batch = new Batch();
            batch.SetHeader(
                "5",
                serviceClassCode,
                companyName,
                companyDiscretionary,
                companyID,
                standardEntry,
                companyEntry,
                companyDescriptiveDate,
                effectiveEntryDate,
                settlementDate,
                "1",//TODO may not be one but think this always should be
                originatingDFI,
                batchNumber.ToString().PadLeft(7, '0'));

            batch.SetControl(
                "8",
                serviceClassCode,
                "",
                "",
                "",
                "",
                companyID,
                "".PadLeft(19),
                "".PadLeft(6),
                originatingDFI,
                batchNumber.ToString().PadLeft(7, '0'));
            
            batch.AutoGenValues();

            return batch;
        }

        //TODO enum for service class code

        //TODO autogen that sets all entry values (trace, auto gen entry), entry/addenda count, hash, total debit amount, total credit amount, matching values (record type, service class, company id, originating dfi, batch number)
        public void AutoGenValues()
        {
            int entrySequence = 1;
            foreach (Entry entry in Entries)
            {
                entry.AutoGenValues();
                entry.TraceNumber = GenerateEntryTraceNumber(entrySequence);
                entrySequence++;
            }

            EntryCount = EntryAddendaCount().ToString().PadLeft(6, '0');
            EntryHash = GenerateHash();
            TotalDebit = GenerateTotalDebit();
            TotalCredit = GenerateTotalCredit();

            ControlServiceClass = HeaderServiceClass;
            ControlCompanyIdentification = HeaderCompanyIdentification;
            ControlOriginatingDFI = HeaderOriginatorDFI;
            ControlBatchNumber = HeaderBatchNumber;
        }

        void AddEntry(Entry entry)
        {
            Entries.Add(entry);
            AutoGenValues();
        }
    }
}
