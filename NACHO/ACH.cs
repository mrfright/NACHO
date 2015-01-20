using System.Collections.Generic;
using System.Collections;

//doesn't know about files or formatted lines, that's the ACHParser

namespace NACHO
{
    /// <summary>
    /// ACH input file structure for NACHO - National Automated Clearing House Objects, a library for NACHA files
    /// <para>
    /// An ACH class is meant to map one-to-one to ACH files.  Each ACH will have header and control record information
    /// and any number of <see cref="Batch"/> records, and possibly arbitrary text data at the beginning of the file that can be for internal use.
    /// </para>
    /// </summary>
    public class ACH
    {
        public const uint HEADER_RECORD_TYPE_LENGTH         = 1;
        public const uint PRIORITY_CODE_LENGTH              = 2;
        public const uint IMMEDIATE_DESTINATION_LENGTH      = 10;
        public const uint IMMEDIATE_ORIGIN_LENGTH           = 10;
        public const uint FILE_CREATION_DATE_LENGTH         = 6;
        public const uint FILE_CREATION_TIME_LENGTH         = 4;
        public const uint FILE_ID_MODIFIER_LENGTH           = 1;
        public const uint RECORD_SIZE_LENGTH                = 3;
        public const uint BLOCKING_FACTOR_LENGTH            = 2;
        public const uint FORMAT_CODE_LENGTH                = 1;
        public const uint IMMEDIATE_DESTINATION_NAME_LENGTH = 23;
        public const uint IMMEDIATE_ORIGIN_NAME_LENGTH      = 23;
        public const uint REFERENCE_CODE_LENGTH             = 8;
        public const uint CONTROL_RECORD_TYPE_LENGTH        = 1;
        public const uint BATCH_COUNT_LEGTH                 = 6;
        public const uint BLOCK_COUNT_LENGTH                = 6;
        public const uint ENTRY_ADDENDA_COUNT_LENGTH        = 8;
        public const uint ENTRY_HASH_LENGTH                 = 10;
        public const uint TOTAL_DEBIT_LENGTH                = 12;
        public const uint TOTAL_CREDIT_LENGTH               = 12;
        public const uint RESERVED_LENGTH                   = 39;

        public const string HEADER_RECORD_TYPE             = "1";
        public const string PRIORITY_CODE_EXPECTED_VALUE   = "01";
        public const string RECORD_SIZE_EXPECTED_VALUE     = "094";
        public const string BLOCKING_FACTOR_EXPECTED_VALUE = "10";
        public const string FORMAT_CODE_EXPECTED_VALUE     = "1";
        public const string CONTROL_RECORD_TYPE            = "9";

        public string InternalString;
        public string HeaderRecordTypeCode;
        public string PriorityCode;
        public string ImmediateDestination;
        public string ImmediateOrigin;
        public string FileCreationDate;
        public string FileCreationTime;
        public string FileIdModifier;
        public string RecordSize;
        public string BlockingFactor;
        public string FormatCode;
        public string ImmediateDestinationName;
        public string ImmediateOriginName;
        public string ReferenceCode;
        public string ControlRecordTypeCode;
        public string BatchCount;
        public string BlockCount;
        public string EntryAddendaCount;
        public string EntryHash;
        public string TotalDebit;
        public string TotalCredit;
        public string Reserved;

        public ArrayList Batches = new ArrayList();

        public string 
        SetHeader(string internalStringParam,
            string headerRecordTypeCodeParam,
            string priorityCodeParam,
            string immediateDestinationParam,
            string immediateOriginParam,
            string fileCreationDateParam,
            string fileCreationTimeParam,
            string fileIdModifierParam,
            string recordSizeParam,
            string blockingFactorParam,
            string formatCodeParam,
            string immediateDestinationNameParam,
            string immediateOriginNameParam,
            string referenceCodeParam)//set just header values
        {
            string messages = "";

            InternalString = internalStringParam;

            messages += LengthCheck.CheckLength("Header Record Type", headerRecordTypeCodeParam, HEADER_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Record Type", headerRecordTypeCodeParam, HEADER_RECORD_TYPE);
            HeaderRecordTypeCode = headerRecordTypeCodeParam;

            messages += LengthCheck.CheckLength("Priority Code", priorityCodeParam, PRIORITY_CODE_LENGTH);
            messages += ExpectedString.CheckString("Priority Code", priorityCodeParam, PRIORITY_CODE_EXPECTED_VALUE);
            PriorityCode = priorityCodeParam;

            messages += LengthCheck.CheckLength("Immediate Destination", immediateDestinationParam, IMMEDIATE_DESTINATION_LENGTH);
            ImmediateDestination = immediateDestinationParam;

            messages += LengthCheck.CheckLength("Immediate Origin", immediateOriginParam, IMMEDIATE_ORIGIN_LENGTH);
            ImmediateOrigin = immediateOriginParam;

            messages += LengthCheck.CheckLength("File Creation Date", fileCreationDateParam, FILE_CREATION_DATE_LENGTH);
            FileCreationDate = fileCreationDateParam;

            messages += LengthCheck.CheckLength("File Creation Time", fileCreationTimeParam, FILE_CREATION_TIME_LENGTH);
            FileCreationTime = fileCreationTimeParam;

            messages += LengthCheck.CheckLength("File ID Modifier", fileIdModifierParam, FILE_ID_MODIFIER_LENGTH);
            FileIdModifier = fileIdModifierParam;

            messages += LengthCheck.CheckLength("Record Size", recordSizeParam, RECORD_SIZE_LENGTH);
            messages += ExpectedString.CheckString("Record Size", recordSizeParam, RECORD_SIZE_EXPECTED_VALUE);
            RecordSize = recordSizeParam;

            messages += LengthCheck.CheckLength("Blocking Factor", blockingFactorParam, BLOCKING_FACTOR_LENGTH);
            messages += ExpectedString.CheckString("Blocking Factor", blockingFactorParam, BLOCKING_FACTOR_EXPECTED_VALUE);
            BlockingFactor = blockingFactorParam;

            messages += LengthCheck.CheckLength("Format Code", formatCodeParam, FORMAT_CODE_LENGTH);
            messages += ExpectedString.CheckString("Format Code", formatCodeParam, FORMAT_CODE_EXPECTED_VALUE);
            FormatCode = formatCodeParam;

            messages += LengthCheck.CheckLength("Immediate Destination Name", immediateDestinationNameParam, IMMEDIATE_DESTINATION_NAME_LENGTH);
            ImmediateDestinationName = immediateDestinationNameParam;

            messages += LengthCheck.CheckLength("Immediate Origin Name", immediateOriginNameParam, IMMEDIATE_ORIGIN_NAME_LENGTH);
            ImmediateOriginName = immediateOriginNameParam;

            messages += LengthCheck.CheckLength("Reference Code", referenceCodeParam, REFERENCE_CODE_LENGTH);
            ReferenceCode = referenceCodeParam;

            return messages;
        }

        public string 
        SetControl(string controlRecordTypeCodeParam,
                   string batchCountParam, //if calculated entries like this are null, then making a new ach
                   string blockCountParam,
                   string entryAddendaCountParam,
                   string entryHashParam,
                   string totalDebitParam,
                   string totalCreditParam,
                   string reservedParam)//set just control values
        {
            string messages = "";

            messages += LengthCheck.CheckLength("Control Record Type Code", controlRecordTypeCodeParam, CONTROL_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Control Record Type Code", controlRecordTypeCodeParam, CONTROL_RECORD_TYPE);
            ControlRecordTypeCode = controlRecordTypeCodeParam;

            //pass in null if making new ach, else if reading in existing then pass 
            //in and this will do integrity checks
            messages += LengthCheck.CheckLength("Batch Count", batchCountParam, BATCH_COUNT_LEGTH);
            BatchCount = batchCountParam;

            messages += LengthCheck.CheckLength("Block Count", blockCountParam, BLOCK_COUNT_LENGTH);
            BlockCount = blockCountParam;

            messages += LengthCheck.CheckLength("Entry Addenda Count", entryAddendaCountParam, ENTRY_ADDENDA_COUNT_LENGTH);
            EntryAddendaCount = entryAddendaCountParam;

            messages += LengthCheck.CheckLength("Entry Hash", entryHashParam, ENTRY_HASH_LENGTH);
            EntryHash = entryHashParam;

            messages += LengthCheck.CheckLength("Total Debit", totalDebitParam, TOTAL_DEBIT_LENGTH);
            TotalDebit = totalDebitParam;

            messages += LengthCheck.CheckLength("Total Credit", totalCreditParam, TOTAL_CREDIT_LENGTH);
            TotalCredit = totalCreditParam;

            messages += LengthCheck.CheckLength("Reserved", reservedParam, RESERVED_LENGTH);
            Reserved = reservedParam;

            return messages;
        }

        //set values that depend on batch entry values
        void SetAutoValues()
        {
        }
    }
}
