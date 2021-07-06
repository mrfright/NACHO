using System;
using System.Text;

namespace NACHO
{
    public class ACHPrinter
    {
        public static string PrintACH(ACH ach)
        {
            var achBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(ach.InternalString))
                PrintInternal(ach, achBuilder);

            PrintHeader(ach, achBuilder);
            PrintBatches(ach, achBuilder);
            PrintControl(ach, achBuilder);
            CompleteLastBlock(ach, achBuilder);

            return achBuilder.ToString();
        }        

        public static void PrintInternal(ACH ach, StringBuilder achBuilder)
        {
            achBuilder.AppendLine(ach.InternalString);
        }

        public static void PrintHeader(ACH ach, StringBuilder achBuilder)
        {
            achBuilder.AppendLine(ach.GetHeader());
        }        

        public static void PrintBatches(ACH ach, StringBuilder achBuilder)
        {
            foreach (Batch batch in ach.Batches)
            {
                achBuilder.AppendLine(BatchPrinter.PrintBatch(batch));
            }
        }

        public static void PrintControl(ACH ach, StringBuilder achBuilder)
        {
            achBuilder.AppendLine(ach.GetControl());
        }

        private static void CompleteLastBlock(ACH ach, StringBuilder achBuilder)
        {
            var totalNumberOfLines = ach.GetTotalLinesCount();
            var printedNumberOfLines = ach.GetDataLinesCount();

            var blockFiller = new string('9', int.Parse(ach.RecordSize));

            for (var i = 0; i < totalNumberOfLines - printedNumberOfLines; i++)
                achBuilder.AppendLine(blockFiller);
        }
    }
}