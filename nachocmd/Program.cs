using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NACHO;

namespace nachodump
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO test that create with auto gen of everything also passes all verifies

            //this is what knows about files (as should any user of the NACHO library)
            if (args.Length <= 0)
            {
                //TODO print usage

            }
            else
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(args[0]);
                string messages;
                ACH ach = ACHParser.ParseStream(reader, out messages);
                System.Console.WriteLine(messages);
                System.Console.WriteLine("\n"+ACHPrinter.PrintACH(ach));

                Entry entry = new Entry(
                    "6",
                    "27",
                    "07640125",
                    "0",
                    "".PadLeft(17),
                    "".PadLeft(10),
                    "".PadLeft(15),
                    "".PadLeft(22),
                    "  ",
                    "0",
                    "".PadLeft(15));

                entry.CheckDigit = Entry.GenerateCheckDigit(entry.ReceivingDFI);
            }
        }
    }
}
