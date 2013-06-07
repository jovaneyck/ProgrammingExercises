using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace TDDMicroExercises.UnicodeFileToHtmTextConverter
{
    [TestFixture]
    class UnicodeFileToHtmTextConverterTest
    {
        [Test]
        [UseReporter(typeof(NUnitReporter))]
        public void ConvertsAFileToHtml()
        {
            //This test hits the filesystem AND needs a real file for every test run :(
            //Hardcoded dependency on the File API is a violiation of SoliD:
            //Multiple repsonsibilities: reading a file from disk AND converting it to HTML
            //Hardwired dependency on a concrete type, rather than an abstraction: File API.
            const string fullFileName = "./UnicodeFileToHtmTextConverter/testFile.txt";
            UnicodeFileToHtmTextConverter converter = new UnicodeFileToHtmTextConverter(fullFileName);
            string result = converter.ConvertToHtml();
           Approvals.Verify(result);
        }
    }
}
