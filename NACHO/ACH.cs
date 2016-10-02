using System.Collections.Generic;
using System.Collections;
using System;

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
        public const int HEADER_RECORD_TYPE_LENGTH         = 1;
        public const int PRIORITY_CODE_LENGTH              = 2;
        public const int IMMEDIATE_DESTINATION_LENGTH      = 10;
        public const int IMMEDIATE_ORIGIN_LENGTH           = 10;
        public const int FILE_CREATION_DATE_LENGTH         = 6;
        public const int FILE_CREATION_TIME_LENGTH         = 4;
        public const int FILE_ID_MODIFIER_LENGTH           = 1;
        public const int RECORD_SIZE_LENGTH                = 3;
        public const int BLOCKING_FACTOR_LENGTH            = 2;
        public const int FORMAT_CODE_LENGTH                = 1;
        public const int IMMEDIATE_DESTINATION_NAME_LENGTH = 23;
        public const int IMMEDIATE_ORIGIN_NAME_LENGTH      = 23;
        public const int REFERENCE_CODE_LENGTH             = 8;
        public const int CONTROL_RECORD_TYPE_LENGTH        = 1;
        public const int BATCH_COUNT_LEGTH                 = 6;
        public const int BLOCK_COUNT_LENGTH                = 6;
        public const int ENTRY_ADDENDA_COUNT_LENGTH        = 8;
        public const int ENTRY_HASH_LENGTH                 = 10;
        public const int TOTAL_DEBIT_LENGTH                = 12;
        public const int TOTAL_CREDIT_LENGTH               = 12;
        public const int RESERVED_LENGTH                   = 39;

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

        public List<Batch> Batches = new List<Batch>();

        public void 
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
            string referenceCodeParam)
        {
            InternalString = internalStringParam;            
            HeaderRecordTypeCode = headerRecordTypeCodeParam;            
            PriorityCode = priorityCodeParam;            
            ImmediateDestination = immediateDestinationParam;            
            ImmediateOrigin = immediateOriginParam;            
            FileCreationDate = fileCreationDateParam;            
            FileCreationTime = fileCreationTimeParam;            
            FileIdModifier = fileIdModifierParam;            
            RecordSize = recordSizeParam;            
            BlockingFactor = blockingFactorParam;            
            FormatCode = formatCodeParam;            
            ImmediateDestinationName = immediateDestinationNameParam;            
            ImmediateOriginName = immediateOriginNameParam;            
            ReferenceCode = referenceCodeParam;
        }

        public void 
        SetControl(string controlRecordTypeCodeParam,
                   string batchCountParam,
                   string blockCountParam,
                   string entryAddendaCountParam,
                   string entryHashParam,
                   string totalDebitParam,
                   string totalCreditParam,
                   string reservedParam)
        {
            ControlRecordTypeCode = controlRecordTypeCodeParam;
            BatchCount = batchCountParam;            
            BlockCount = blockCountParam;            
            EntryAddendaCount = entryAddendaCountParam;            
            EntryHash = entryHashParam;            
            TotalDebit = totalDebitParam;            
            TotalCredit = totalCreditParam;            
            Reserved = reservedParam;
        }

        public void SetAutoValues()
        {
            int batchNumber = 1;
            foreach (Batch batch in Batches)
            {
                batch.HeaderCompanyIdentification = ImmediateOrigin;
                batch.HeaderOriginatorDFI = ImmediateDestination.Substring(1, 8);
                batch.EffectiveEntryDate = FileCreationDate;

                string tempOriginName = ImmediateOriginName.Trim();
                if (tempOriginName.Length > Batch.COMPANY_NAME_LENGTH)
                {
                    tempOriginName = tempOriginName.Substring(0, Batch.COMPANY_NAME_LENGTH);
                }
                batch.HeaderCompanyName = tempOriginName.PadLeft(Batch.COMPANY_NAME_LENGTH, ' ');

                batch.AutoGenValues(batchNumber++);
            }

            BatchCount = Batches.Count.ToString().PadLeft(6, '0');
            BlockCount = GenerateBlockCount().ToString().PadLeft(6, '0');
            EntryAddendaCount = GenerateEntryAddendaCount().ToString().PadLeft(8, '0');
            EntryHash = GenerateEntryHash();
            TotalDebit = GenerateTotalDebit().ToString().PadLeft(12, '0');
            TotalCredit = GenerateTotalCredit().ToString().PadLeft(12, '0');
        }

        public string Verify()
        {
            string messages = "";

            messages += LengthCheck.CheckLength("Header Record Type", HeaderRecordTypeCode, HEADER_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Record Type", HeaderRecordTypeCode, new string[] { HEADER_RECORD_TYPE });
            messages += LengthCheck.CheckLength("Priority Code", PriorityCode, PRIORITY_CODE_LENGTH);
            messages += ExpectedString.CheckString("Priority Code", PriorityCode, new string[] { PRIORITY_CODE_EXPECTED_VALUE });
            messages += LengthCheck.CheckLength("Immediate Destination", ImmediateDestination, IMMEDIATE_DESTINATION_LENGTH);
            messages += LengthCheck.CheckLength("Immediate Origin", ImmediateOrigin, IMMEDIATE_ORIGIN_LENGTH);
            messages += LengthCheck.CheckLength("File Creation Date", FileCreationDate, FILE_CREATION_DATE_LENGTH);
            messages += LengthCheck.CheckLength("File Creation Time", FileCreationTime, FILE_CREATION_TIME_LENGTH);
            messages += LengthCheck.CheckLength("File ID Modifier", FileIdModifier, FILE_ID_MODIFIER_LENGTH);
            messages += LengthCheck.CheckLength("Record Size", RecordSize, RECORD_SIZE_LENGTH);
            messages += ExpectedString.CheckString("Record Size", RecordSize, new string[] { RECORD_SIZE_EXPECTED_VALUE });
            messages += LengthCheck.CheckLength("Control Record Type Code", ControlRecordTypeCode, CONTROL_RECORD_TYPE_LENGTH);
            messages += ExpectedString.CheckString("Control Record Type Code", ControlRecordTypeCode, new string[] { CONTROL_RECORD_TYPE });
            messages += LengthCheck.CheckLength("Blocking Factor", BlockingFactor, BLOCKING_FACTOR_LENGTH);
            messages += ExpectedString.CheckString("Blocking Factor", BlockingFactor, new string[] { BLOCKING_FACTOR_EXPECTED_VALUE });
            messages += LengthCheck.CheckLength("Format Code", FormatCode, FORMAT_CODE_LENGTH);
            messages += ExpectedString.CheckString("Format Code", FormatCode, new string[] { FORMAT_CODE_EXPECTED_VALUE });
            messages += LengthCheck.CheckLength("Immediate Destination Name", ImmediateDestinationName, IMMEDIATE_DESTINATION_NAME_LENGTH);
            messages += LengthCheck.CheckLength("Immediate Origin Name", ImmediateOriginName, IMMEDIATE_ORIGIN_NAME_LENGTH);
            messages += LengthCheck.CheckLength("Reference Code", ReferenceCode, REFERENCE_CODE_LENGTH);
            messages += LengthCheck.CheckLength("Batch Count", BatchCount, BATCH_COUNT_LEGTH);
            messages += LengthCheck.CheckLength("Block Count", BlockCount, BLOCK_COUNT_LENGTH);
            messages += LengthCheck.CheckLength("Entry Addenda Count", EntryAddendaCount, ENTRY_ADDENDA_COUNT_LENGTH);
            messages += LengthCheck.CheckLength("Entry Hash", EntryHash, ENTRY_HASH_LENGTH);
            messages += LengthCheck.CheckLength("Total Debit", TotalDebit, TOTAL_DEBIT_LENGTH);
            messages += LengthCheck.CheckLength("Total Credit", TotalCredit, TOTAL_CREDIT_LENGTH);
            messages += LengthCheck.CheckLength("Reserved", Reserved, RESERVED_LENGTH);

            if (ACHPrinter.PrintHeader(this).Length != 94)
            {
                messages += "\nACH header is not 94 characters long: '" + ACHPrinter.PrintHeader(this) + "'";
            }

            if (ACHPrinter.PrintControl(this).Length != 94)
            {
                messages += "\nACH control footer is not 94 characters long: '" + ACHPrinter.PrintControl(this) + "'";
            }

            foreach (Batch batch in Batches)
            {
                string batchMessage = batch.Verify();

                if (!ImmediateOrigin.Equals(batch.HeaderCompanyIdentification))
                {
                    batchMessage += "\nBatch Company ID '" + batch.HeaderCompanyIdentification +
                        "' doesn't match ACH Immediate Origin '" + ImmediateOrigin + "'";
                }

                if (!ImmediateDestination.Substring(1, 8).Equals(batch.HeaderOriginatorDFI))
                {
                    batchMessage += "\nBatch Originator DFI '" + batch.HeaderOriginatorDFI +
                        "' doesn't match ACH Immediate Destination '" + ImmediateDestination + "'";
                }

                if (!string.IsNullOrEmpty(batchMessage))
                {
                    messages += "\nErrors in Batch: " + BatchPrinter.PrintBatchMembers(batch) + ": " + batchMessage;
                }
            }

            int batchCountNum = -1;
            if (int.TryParse(BatchCount, out batchCountNum))
            {
                if (batchCountNum != Batches.Count)
                {
                    messages += "\nACH value for Batch Count of " + batchCountNum.ToString() +
                        " does not match the actual number of batches of " + Batches.Count.ToString();
                }
            }
            else
            {
                messages += "\nACH Batch Count could not be parsed as an integer";
            }

            int blockCountNum = 0;
            if (int.TryParse(BlockCount, out blockCountNum))
            {
                int expectedBlockCount = GenerateBlockCount();
                if (blockCountNum != expectedBlockCount)
                {
                    messages += "\nACH block count of " + blockCountNum.ToString() +
                        " did not match expected block count of " + expectedBlockCount.ToString();
                }
            }
            else
            {
                messages += "\nACH block count could not be parsed as an integer";
            }

            int entryCountNum = 0;
            if (int.TryParse(EntryAddendaCount, out entryCountNum))
            {
                int expectedEntryCount = GenerateEntryAddendaCount();
                if (entryCountNum != expectedEntryCount)
                {
                    messages += "\nACH entry and addenda count of " + entryCountNum.ToString() +
                        " did not match expected count of " + expectedEntryCount.ToString();
                }
            }
            else
            {
                messages += "\nACH entry and addenda count could not be parsed as an integer";
            }

            string expectedEntryHash = GenerateEntryHash();
            if (!EntryHash.Equals(expectedEntryHash))
            {
                messages += "\nACH entry hash of '" + EntryHash + "' does not match expected value of '" + expectedEntryHash + "'";
            }

            int totalDebitNum = 0;
            if (int.TryParse(TotalDebit, out totalDebitNum))
            {
                int expectedTotalDebit = GenerateTotalDebit();
                if (totalDebitNum != expectedTotalDebit)
                {
                    messages += "\nACH total debit was " + totalDebitNum.ToString() +
                        " when the expected value was " + expectedTotalDebit.ToString();
                }
            }
            else
            {
                messages += "\nACH total debit could not be parsed as an integer";
            }

            int totalCreditNum = 0;
            if (int.TryParse(TotalCredit, out totalCreditNum))
            {
                int expectedTotalCredit = GenerateTotalCredit();
                if (totalCreditNum != expectedTotalCredit)
                {
                    messages += "\nACH total credit was " + totalCreditNum.ToString() +
                        " when the expected value was " + expectedTotalCredit.ToString();
                }
            }
            else
            {
                messages += "\nACH total credit could not be parsed as an integer";
            }

            return messages;
        }

        /// <summary>
        /// Blocks are groups of 10 lines, which includes any header or control lines in the file.
        /// The count is how many blocks are needed to hold that many lines.  
        /// For example, 10 lines is one block. For 13 lines, two blocks are needed.
        /// </summary>
        /// <returns>Block count</returns>
        public int GenerateBlockCount()
        {
            int count = 2;//ACH file header and control

            foreach (Batch batch in Batches)
            {
                count += batch.EntryAddendaCount() + 2;//+2 for batch header and control
            }

            int blockCount = count / 10;//'Blocking Factor' from ACH header, but always 10 any way
            if (count % 10 > 0)
            {
                blockCount++;
            }

            return count;
        }

        public int GenerateEntryAddendaCount()
        {
            int count = 0;

            foreach (Batch batch in Batches)
            {
                count += batch.EntryAddendaCount();
            }

            return count;
        }

        public string GenerateEntryHash()
        {
            long hashAccum = 0;
            foreach (Batch batch in Batches)
            {
                hashAccum += batch.GenerateHashNumber();
            }

            return (hashAccum % 10000000000).ToString().PadLeft(10, '0');
        }

        public int GenerateTotalDebit()
        {
            int totalDebit = 0;
            foreach (Batch batch in Batches)
            {
                totalDebit += batch.GenerateTotalDebitNumber();
            }

            return totalDebit;
        }

        public int GenerateTotalCredit()
        {
            int totalCredit = 0;
            foreach (Batch batch in Batches)
            {
                totalCredit += batch.GenerateTotalCreditNumber();
            }

            return totalCredit;
        }

        public static ACH CreateACH(
            string internalString,
            string immediateDestination,
            string immediateOrigin,
            string fileIDModifier,
            string immediateDestinationName,
            string immediateOriginName,
            string referenceCode
            )
        {
            string date = DateTime.Now.ToString("yyMMdd");
            string time = DateTime.Now.ToString("HHmm");

            if (immediateDestinationName.Length > IMMEDIATE_DESTINATION_NAME_LENGTH)
            {
                immediateDestinationName = immediateDestinationName.Substring(0, (int)IMMEDIATE_DESTINATION_NAME_LENGTH);
            }

            if (immediateOriginName.Length > IMMEDIATE_ORIGIN_NAME_LENGTH)
            {
                immediateOriginName = immediateOriginName.Substring(0, (int)IMMEDIATE_ORIGIN_NAME_LENGTH);
            }

            if (referenceCode.Length > REFERENCE_CODE_LENGTH)
            {
                referenceCode = referenceCode.Substring(0, (int)REFERENCE_CODE_LENGTH);
            }

            ACH ach = new ACH();
            ach.SetHeader(
                internalString,
                "1",
                "01",
                immediateDestination,
                immediateOrigin,
                date,
                time,
                fileIDModifier,
                "094",
                "10",
                "1",
                immediateDestinationName.PadLeft((int)IMMEDIATE_DESTINATION_NAME_LENGTH, ' '),
                immediateOriginName.PadLeft((int)IMMEDIATE_ORIGIN_NAME_LENGTH, ' '),
                referenceCode.PadLeft((int)REFERENCE_CODE_LENGTH, ' '));

            ach.SetControl(
                "9",
                "",
                "",
                "",
                "",
                "",
                "",
                "".PadLeft((int)RESERVED_LENGTH));

            ach.SetAutoValues();

            return ach;
        }

        public void AddBatch(Batch batch)
        {
            Batches.Add(batch);
            SetAutoValues();
        }
    }
}
