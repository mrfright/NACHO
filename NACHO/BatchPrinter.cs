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

        public static string PrintBatchMembers(Batch batch)
        {
            return "HeaderRecordType='" + batch.HeaderRecordType
                + "' HeaderServiceClass='" + batch.HeaderServiceClass
                + "' HeaderCompanyName='" + batch.HeaderCompanyName
                + "' CompanyDiscretionary='" + batch.CompanyDiscretionary
                + "' HeaderCompanyIdentification='" + batch.HeaderCompanyIdentification
                + "' StandardEntry='" + batch.StandardEntry
                + "' CompanyEntry='" + batch.CompanyEntry
                + "' CompanyDescriptionDate='" + batch.CompanyDescriptionDate
                + "' EffectiveEntryDate='" + batch.EffectiveEntryDate
                + "' SettlementDate='" + batch.SettlementDate
                + "' OriginatorStatus='" + batch.OriginatorStatus
                + "' HeaderOriginatorDFI='" + batch.HeaderOriginatorDFI
                + "' HeaderBatchNumber='" + batch.HeaderBatchNumber
                + "' ControlRecordType='" + batch.ControlRecordType
                + "' ControlServiceClass='" + batch.ControlServiceClass
                + "' EntryCount='" + batch.EntryCount
                + "' EntryHash='" + batch.EntryHash
                + "' TotalDebit='" + batch.TotalDebit
                + "' TotalCredit='" + batch.TotalCredit
                + "' ControlCompanyIdentification='" + batch.ControlCompanyIdentification
                + "' MessageAuthentication='" + batch.MessageAuthentication
                + "' Reserved='" + batch.Reserved
                + "' ControlOriginatorDFI='" + batch.ControlOriginatingDFI
                + "' ControlBatchNumber='" + batch.ControlBatchNumber + "'";
        }
    }
}
