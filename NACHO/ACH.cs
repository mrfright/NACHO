using System.Collections.Generic;
using System.Collections;

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
        public const uint RECORD_TYPE_LENGTH                = 1;
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
        public const uint REFERENCE_CODE_LENGTH = 8;

        public const string HEADER_RECORD_TYPE             = "1";
        public const string PRIORITY_CODE_EXPECTED_VALUE   = "01";
        public const string RECORD_SIZE_EXPECTED_VALUE     = "094";
        public const string BLOCKING_FACTOR_EXPECTED_VALUE = "10";
        public const string FORMAT_CODE_EXPECTED_VALUE     = "1";

        public string InternalString;
        public string RecordTypeCode;
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

        public ArrayList Batches;

        ACH(string internalStringParam, 
            string recordTypeCodeParam, 
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
            string referenceCodeParam,
            out string messages)
        {
            Batches = new ArrayList();
            messages = "";

            InternalString = internalStringParam;

            messages += LengthCheck.CheckLength("Record Type", recordTypeCodeParam, RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Record Type", recordTypeCodeParam, HEADER_RECORD_TYPE);
            RecordTypeCode = recordTypeCodeParam;

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
        }

        public string 
        AddBatchRecord(Batch batch)
        {
            string messages = "";

            Batches.Add(batch);

            return messages;
        }        
    }
}
