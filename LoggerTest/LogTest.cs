using DSLogger;
using NUnit.Framework;
using System;
using System.IO;

namespace LoggerTest
{
    public class Tests
    {
        private Logger log;
        private string testFilePath;
        [SetUp]
        public void SetupLoggerTests()
        {
            testFilePath = $"{LogFile.GetDateString(DateTime.Now)} - test.txt";
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
            log = new Logger("", "test");
            //Reset the existing file
            File.WriteAllText("ExistingTestFile.txt", "This line should not be overrwritten");
        }
        [Test]
        public void LoggerFilePathCorrect()
        {
            string sut = log.log.FullPath;
            string expected = testFilePath;
            Assert.That(sut, Is.EqualTo(expected));
        }
        [Test]
        public void CheckNewFileCreated()
        {
            log.WriteToLog("Test Text");
            bool sut = File.Exists(testFilePath);
            Assert.That(sut, Is.True);
        }

        [Test]
        public void ContentOfNewFileCorrect()
        {
            log.WriteToLog("Test Text");

            string sut = File.ReadAllText(testFilePath);

            string expected = "Test Text";

            var contains = sut.Contains(expected);

            Assert.That(contains, Is.True);
        }

        [Test]
        [TestCase("This line should not be overrwritten", 0, TestName = "Original line wasnt overrwritten")]
        [TestCase("This should be the second line!", 1, TestName = "New line was added")]
        public void FileIsntOverrwritten(string expectedLine, int lineNumber)
        {
            //overrule the full path to get our existing file for test
            log.log.FullPath = "ExistingTestFile.txt";

            log.WriteToLog("This should be the second line!");
            log.WriteToLog("This should be the third line!");
            string[] fileLines = File.ReadAllLines("ExistingTestFile.txt");
            if (lineNumber == 0)
            {
                var sut = fileLines[lineNumber];

                Assert.That(sut, Is.EqualTo(expectedLine));
            }
            else
            {
                var sut = fileLines[lineNumber].Contains(expectedLine);
                Assert.That(sut, Is.True);
            }
        }

    }
}