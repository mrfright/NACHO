

//this is what knows about whole ach text streams and lines, doesn't know about files

namespace NACHO
{
    public class ACHParser
    {
        public const uint ACH_HEADER_RECORD_TYPE_ASCII_VALUE    = 49;
        public const uint BATCH_HEADER_RECORD_TYPE_ASCII_VALUE  = 53;
        public const uint ENTRY_RECORD_TYPE_ASCII_VALUE         = 54;
        public const uint ADDENDA_RECORD_TYPE_ASCII_VALUE       = 55;
        public const uint BATCH_CONTROL_RECORD_TYPE_ASCII_VALUE = 56;
        public const uint ACH_CONTROL_RECORD_TYPE_ASCII_VALUE   = 57;

        //parse whole file, calls batch parser, returns new ach
        //takes stream reader
        public static ACH ParseStream(System.IO.StreamReader reader, out string messages)
        {
            //or leave null, return null on (initial, not all) error?
            ACH ach = new ACH();


            string internalString = "";

            uint currentLineNumber = 1;
            messages = "";

            //set to false once we see a valid entry record type code
            //used to determine when we should stop seeing internal lines that 
            //should only be at the beginning of the file
            bool didNotRecieveEntry = true;

            bool recievedAchControlEntry = false;
            int recordType;
            
            while ((recordType = reader.Peek()) != -1)
            {
                //if we recieved the ach control entry then add message that another line not expected (except empty line)
                //(but that should be handled by didNotRecieveEntry else case as error)

                //make sure all paths consume at least one line or infinite loop will result

                if (didNotRecieveEntry)
                {
                    //check if first char is a valid record type
                    if (recordType != BATCH_HEADER_RECORD_TYPE_ASCII_VALUE &&
                        recordType != ENTRY_RECORD_TYPE_ASCII_VALUE &&
                        recordType != ADDENDA_RECORD_TYPE_ASCII_VALUE &&
                        recordType != BATCH_CONTROL_RECORD_TYPE_ASCII_VALUE &&
                        recordType != ACH_CONTROL_RECORD_TYPE_ASCII_VALUE)
                    {

                        //this is basically the only valid path, all others should be error that appends message
                        if (recordType != ACH_HEADER_RECORD_TYPE_ASCII_VALUE)
                        {
                            //still an internal message, just append to internal message (with newlines between)
                            internalString += reader.ReadLine();
                            if (internalString != null)//shouldn't get null if recordType peek was successful, but just in case
                            {
                                internalString += "\n";
                                ++currentLineNumber;
                            }
                            else
                            {
                                //TODO report error it was null
                            }
                        }
                        else
                        {
                            //got the ach header entry, set values, go into batch parse call
                            string achHeader = reader.ReadLine();
                            if (achHeader != null)//shouldn't be null if recordType was successful, but just in case
                            {

                                //if not 94 chars, error message
                                string headerRecordTypeCode = achHeader.Substring(0, 1);
                                string priorityCode = achHeader.Substring(1, 2);
                                string immediateDestination = achHeader.Substring(3, 10);
                                string immediateOrigin = achHeader.Substring(13, 10);
                                string fileCreationDate = achHeader.Substring(23, 6);
                                string fileCreationTime = achHeader.Substring(29, 4);
                                string fileIdModifier = achHeader.Substring(33, 1);
                                string recordSize = achHeader.Substring(34, 3);
                                string blockingFactor = achHeader.Substring(37, 2);
                                string formatCode = achHeader.Substring(39, 1);
                                string immediateDestinationName = achHeader.Substring(40, 23);
                                string immediateOriginName = achHeader.Substring(63, 23);
                                string referenceCode = achHeader.Substring(86, 8);

                                string setHeaderMessage = ach.SetHeader(internalString,
                                                          headerRecordTypeCode,
                                                          priorityCode,
                                                          immediateDestination,
                                                          immediateOrigin,
                                                          fileCreationDate,
                                                          fileCreationTime,
                                                          fileIdModifier,
                                                          recordSize,
                                                          blockingFactor,
                                                          formatCode,
                                                          immediateDestinationName,
                                                          immediateOriginName,
                                                          referenceCode);

                                //TODO handle if there there's a message from setheader
                                //if setheadermessage not null or ws then say so and add to messages

                                ++currentLineNumber;//update line number after possible use in error message
                            }
                            else
                            {
                                //TODO report unexpected null
                            }

                            //while next char is a new batch header (message if not a batch header or ach control record type, also error if end of stream -1)
                            //for each batch
                            while ((recordType = reader.Peek()) == BATCH_HEADER_RECORD_TYPE_ASCII_VALUE)
                            {
                                string batchMessages;

                                //TODO pass in a new line count int, add to total current line number count
                                uint linesRead = 0;
                                Batch batch = BatchParser.ParseStream(reader, out batchMessages, out linesRead);
                                messages += batchMessages;

                                if (batch != null)
                                {
                                    ach.Batches.Add(batch);
                                }
                            }
                            //for now test batch parse that just consumes non-ach control entries

                            //when out of batch parse call, reader probably pointing at ach control entry
                            //set more values with that

                            if (recordType != ACH_CONTROL_RECORD_TYPE_ASCII_VALUE)
                            {
                                //error message
                            }
                            else
                            {
                                //get ach control data
                                string achControl = reader.ReadLine();
                                if (achControl != null)
                                {
                                    
                                    //if not 94 chars then error message
                                    string controlRecordType = achControl.Substring(0, 1);
                                    string controlBatchCount = achControl.Substring(1, 6);
                                    string controlBlockCount = achControl.Substring(7, 6);
                                    string controlEntryAddendaCount = achControl.Substring(13, 8);
                                    string controlEntryHash = achControl.Substring(21, 10);
                                    string controlTotalDebit = achControl.Substring(31, 12);
                                    string controlTotalCredit = achControl.Substring(43, 12);
                                    string controlReserved = achControl.Substring(55, 39);

                                    string controlMessage;
                                    controlMessage = ach.SetControl(controlRecordType,
                                                               controlBatchCount,
                                                               controlBlockCount,
                                                               controlEntryAddendaCount,
                                                               controlEntryHash,
                                                               controlTotalDebit,
                                                               controlTotalCredit);

                                    //TODO if controlmessage is not null/ws then add error to message

                                    //TODO check control values here or in SetControl?  Probably SetControl

                                    ++currentLineNumber;
                                }
                            }

                            didNotRecieveEntry = false;
                        }
                    }
                    else
                    {
                        //if a non-ach header record type, then error
                        messages += "\nReceived what looks like a batch or entry unexpectedly before an ACH on line "+currentLineNumber.ToString()+": '" + reader.ReadLine() + "'";
                        ++currentLineNumber;
                        //didNotRecieveEntry = false;
                    }

                    

                    //if it is, make sure it is an ach header
                    //(may be handled earlier right after when we handle ach header)
                   
                }
                else
                {
                    //we got 

                    //if not a record type, or is an ach header then error

                    //when the end of batches is reached, should get the ach control entry, seek 
                    //back to the beginning of that so when exit out of batch then ach next pulls out its control

                    //actually, getting here is an error regardless
                    messages += "\nError on line " + currentLineNumber.ToString() + ": '" + reader.ReadLine() + "'";
                    ++currentLineNumber;
                }
                //needed?
                //++currentLineNumber;
            }

            //if didn't get the ach control, or header for that matter, entry then add message

            //NOTE ignore emtpy line at end

            return ach;
        }

        //parse ach header line
        public static ACH ParseHeader(string headerLine, ACH ach)
        {
            //ach.SetHeader();
            return ach;
        }

        //parse ach footer line
        public static ACH ParseControl(string controlLine, ACH ach)
        {
            //ach.SetControl();
            return ach;
        }
    }
}
