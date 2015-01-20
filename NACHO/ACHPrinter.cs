namespace NACHO
{
    public class ACHPrinter
    {
        public static string PrintInternal(ACH ach)
        {
            return ach.InternalString;
        }

        public static string PrintHeader(ACH ach)
        {
            return ach.HeaderRecordTypeCode
                 + ach.PriorityCode
                 + ach.ImmediateDestination
                 + ach.ImmediateOrigin
                 + ach.FileCreationDate
                 + ach.FileCreationTime
                 + ach.FileIdModifier
                 + ach.RecordSize
                 + ach.BlockingFactor
                 + ach.FormatCode
                 + ach.ImmediateDestinationName
                 + ach.ImmediateOriginName
                 + ach.ReferenceCode;
        }

        public static string PrintControl(ACH ach)
        {
            return ach.ControlRecordTypeCode
                 + ach.BatchCount
                 + ach.BlockCount
                 + ach.EntryAddendaCount
                 + ach.EntryHash
                 + ach.TotalDebit
                 + ach.TotalCredit
                 + ach.Reserved;
        }

        public static string PrintACH(ACH ach)
        {
            string achString = PrintInternal(ach);
            achString += "\n" + PrintHeader(ach);
            achString += "\n" + PrintBatches(ach);
            achString += "\n"+PrintControl(ach);
            return achString;
        }

        public static string PrintBatches(ACH ach)
        {
            string batchStr = "";
            string prepend = "";
            foreach (Batch batch in ach.Batches)
            {
                batchStr += prepend + BatchPrinter.PrintBatch(batch);
                prepend = "\n";
            }

            return batchStr;
        }
    }
}
