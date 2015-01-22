namespace NACHO
{
    public class BatchPrinter
    {
        public static string PrintHeader(Batch batch)
        {
            return batch.HeaderRecordType
                + batch.HeaderServiceClass
                + batch.HeaderCompanyName
                + batch.CompanyDiscretionary
                + batch.HeaderCompanyIdentification
                + batch.StandardEntry
                + batch.CompanyEntry
                + batch.CompanyDescriptionDate
                + batch.EffectiveEntryDate
                + batch.SettlementDate
                + batch.OriginatorStatus
                + batch.HeaderOriginatorDFI
                + batch.HeaderBatchNumber;
        }

        public static string PrintControl(Batch batch)
        {
            return batch.ControlRecordType
                + batch.ControlServiceClass
                + batch.EntryCount
                + batch.EntryHash
                + batch.TotalDebit
                + batch.TotalCredit
                + batch.ControlCompanyIdentification
                + batch.MessageAuthentication
                + batch.Reserved
                + batch.ControlOriginatingDFI
                + batch.ControlBatchNumber;
        }

        public static string PrintBatch(Batch batch)
        {
            string batchStr = PrintHeader(batch);

            foreach (Entry entry in batch.Entries)
            {
                batchStr += "\n" + EntryPrinter.PrintEntry(entry);
            }

            return batchStr + "\n" + PrintControl(batch);
        }
    }
}
