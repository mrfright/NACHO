namespace NACHO
{
    public class BatchParser
    {
        //todo handle current line in file passing and updating
        public static Batch ParseStream(System.IO.StreamReader reader, out string messages, out uint linesRead)
        {
            linesRead = 0;
            messages = "";
            Batch batch = new Batch();
            int recordType;

            string devnull;
            
            if ((recordType = reader.Peek()) != -1)
            {
                if (recordType != ACHParser.BATCH_HEADER_RECORD_TYPE_ASCII_VALUE)
                {
                    //consume line, message that not expected
                    //TODO check null?
                    devnull = reader.ReadLine();
                    ++linesRead;
                }
                else
                {
                    //assume next line to consume is a batch header
                    //TODO check null?
                    devnull = reader.ReadLine();
                    ++linesRead;
                    //if not 94 chars then error message

                    //for testing just for the moment
                    //while not a batch control or end of file, consume a line

                    while ((recordType = reader.Peek()) != -1 && recordType != ACHParser.BATCH_CONTROL_RECORD_TYPE_ASCII_VALUE)
                    {
                        //TODO more checks needed, must be an entry type, would be an error to get an ACH header here, or some random character
                        //if not an entry record type then error message
                        devnull = reader.ReadLine();
                        ++linesRead;
                        //if not 94 chars then error message
                    }

                    

                    //TODO shoudl be batch control, check then get that, would be an error if -1
                    //check if peek to next record type is batch control as expected, or something else or end of file which are errors
                    devnull = reader.ReadLine();
                    //if not 94 chars then error message
                    ++linesRead;
                }
            }
            else
            {
                //message that we got an unexpected emtpy file
            }
            return batch;
        }
    }
}
