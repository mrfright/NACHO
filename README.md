# NACHO

NACHO - National Automated Clearing House Objects, a library for the NACHA ACH input file structure

The Automated Clearing House (ACH) is a network for financial transactions in the US.  The [National ACH Association (NACHA)](https://www.nacha.org/) manages the ACH network.  The NACHA file format is what they use for their financial transactions.

That's how I understand it when I made NACHO to help me with work I was doing for a client that wanted to do payment processing.  There are specifications for the file format (see below), but as is the case in actual implementation of any specification, the implementation is the authority on the matter in that case and not necessarily the specification.  For me, this was the client's payment processor that said if our files were good.  They sent me some example files, but even those had errors.  Ultimately, I generated test files, they sent me back what was wrong, I made changes until the test files I sent were accepted by them.

Should you find that this library doesn't create files that meet your specification, whatever that may be, create an issue and I'll look into it.

* [ACH Rules Online](http://achrulesonline.org/index.aspx) - an official site, you have to register but it's free, helped me a little
* [NACHA Format](http://links.hwtreasurysolution.com/documents/NACHA-FORMAT.pdf) - the most helpful document I had, I think from [Treasury Software](http://treasurysoftware.com/)
  * [ACH File Software](http://www.treasurysoftware.com/ACH/ACH-NACHA-File-Software.aspx)
  * [ACH Universal](http://www.treasurysoftware.com/ACH/ACH.aspx)
* [ACH File Layout Background](http://services.minutemenu.com/centers/staticresources/achinfo.htm)
* [ACH Status Codes](https://www.paypalobjects.com/en_US/vhelp/paypalmanager_help/ach_status_code.htm)
* [ACH/Check](http://help.paytrace.com/create-account-ach-check)

- [ ] format page
- [ ] example page