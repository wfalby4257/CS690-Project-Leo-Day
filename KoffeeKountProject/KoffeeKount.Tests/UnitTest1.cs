namespace KoffeeKount.Tests;

using System.IO;
using KoffeeKount;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        string logFileName = "TestLogEntries.txt";

        //Always start with a new file
        if (File.Exists(logFileName)) {
            File.Delete(logFileName);
        }

        LogFileHandler logFH = new LogFileHandler(logFileName);

        string tmpStr = "This is a test log entry.";
        logFH.writeLogEntry(tmpStr);

        Assert.True(File.Exists(logFileName));

        string logData = File.ReadAllText(logFileName);
        string [] logEntries = logData.Split(';');
        string cmpStr = logEntries[1].Trim(';');
        Assert.Equal(tmpStr, cmpStr);
    }
}