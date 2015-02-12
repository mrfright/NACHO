using System.Collections.Generic;
using System.Collections;

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

        public string
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
            string messages = "";

            messages += LengthCheck.CheckLength("Header Record Type", headerRecordTypeParam, HEADER_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Header Record Type", headerRecordTypeParam, new string[]{HEADER_RECORD_TYPE});
            HeaderRecordType = headerRecordTypeParam;

            messages += LengthCheck.CheckLength("Header Service Class", headerServiceClassParam, HEADER_SERVICE_CLASS_LENGTH);
            HeaderServiceClass = headerServiceClassParam;

            messages += LengthCheck.CheckLength("Header Company Name", headerCompanyNameParam, COMPANY_NAME_LENGTH);
            HeaderCompanyName = headerCompanyNameParam;

            messages += LengthCheck.CheckLength("Company Discretionary Data", companyDiscretionaryPara, COMPANY_DISCRETIONARY_DATA_LENGTH);
            CompanyDiscretionary = companyDiscretionaryPara;

            messages += LengthCheck.CheckLength("Header Company Identification", headerCompanyIdentificationParam, HCOMPANY_IDENTIFICATION_LENGTH);
            HeaderCompanyIdentification = headerCompanyIdentificationParam;

            messages += LengthCheck.CheckLength("Standard Entry", standardEntryParam, STANDARD_ENTRY_LENGTH);
            StandardEntry = standardEntryParam;

            messages += LengthCheck.CheckLength("Company Entry", companyEntryParam, COMPANY_ENTRY_LENGTH);
            CompanyEntry = companyEntryParam;

            messages += LengthCheck.CheckLength("Company Description", companyDescriptionDateParam, COMPANY_DESCRIPTION_DATE_LENGTH);
            CompanyDescriptionDate = companyDescriptionDateParam;

            messages += LengthCheck.CheckLength("Effective Entry Date", effectiveEntryDateParam, EFFECTIVE_ENTRY_DATE_LENGTH);
            EffectiveEntryDate = effectiveEntryDateParam;

            messages += LengthCheck.CheckLength("Settlement Date", settlementDateParam, SETTLEMENT_DATE_LENGTH);
            SettlementDate = settlementDateParam;

            messages += LengthCheck.CheckLength("Originator Status", originatorStatusParam, ORIGINATOR_STATUS_LENGTH);
            OriginatorStatus = originatorStatusParam;

            messages += LengthCheck.CheckLength("Header Originator DFI", headerOriginatorDFIParam, HEADER_ORIGINATOR_DFI_LENGTH);
            HeaderOriginatorDFI = headerOriginatorDFIParam;

            messages += LengthCheck.CheckLength("Header Batch Number", headerBatchNumberParam, HEADER_BATCH_NUMBER_LENGTH);
            HeaderBatchNumber = headerBatchNumberParam;

            return messages;
        }

        public string
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
            string messages = "";
            messages += LengthCheck.CheckLength("Control Record Type", controlRecordTypeParam, CONTROL_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Control Record Type", controlRecordTypeParam, new string[]{CONTROL_RECORD_TYPE});
            ControlRecordType = controlRecordTypeParam;

            messages += LengthCheck.CheckLength("Control Service Class", controlServiceClassParam, CONTROL_SERVICE_CLASS_LENGTH);
            ControlServiceClass = controlServiceClassParam;

            messages += LengthCheck.CheckLength("Entry Count", entryCountParam, ENTRY_COUNT_LENGTH);
            EntryCount = entryCountParam;

            messages += LengthCheck.CheckLength("Entry Hash", entryHashParam, ENTRY_HASH_LENGTH);
            EntryHash = entryHashParam;

            messages += LengthCheck.CheckLength("Total Debit", totalDebitParam, TOTAL_DEBIT_LENGTH);
            TotalDebit = totalDebitParam;

            messages += LengthCheck.CheckLength("Total Credit", totalCreditParam, TOTAL_CREDIT_LENGTH);
            TotalCredit = totalCreditParam;

            messages += LengthCheck.CheckLength("Control Company Identification", controlCompanyIdentificationParam, CCOMPANY_IDENTIFICATION_LENGTH);
            ControlCompanyIdentification = controlCompanyIdentificationParam;

            messages += LengthCheck.CheckLength("Message Authentication", messageAuthenticationParam, MESSAGE_AUTH_LENGTH);
            MessageAuthentication = messageAuthenticationParam;

            messages += LengthCheck.CheckLength("Reserved", reservedParam, RESERVED_LENGTH);
            Reserved = reservedParam;

            messages += LengthCheck.CheckLength("Control Originating DFI", controlOriginatorDFIParam, CONTROL_ORIGINATING_DFI_LENGTH);
            ControlOriginatingDFI = controlOriginatorDFIParam;

            messages += LengthCheck.CheckLength("Control Batch Number", controlBatchNumberParam, CONTROL_BATCH_NUMBER_LENGTH);
            ControlBatchNumber = controlBatchNumberParam;

            return messages;
        }
    }
}
