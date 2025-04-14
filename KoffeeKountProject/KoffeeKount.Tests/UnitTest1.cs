namespace KoffeeKount.Tests;

using KoffeeKount;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        string logFileName = "LogEntries.txt";
        LogFileHandler logFH = new LogFileHandler(logFileName);

        char msgFlag = 'N';
        logFH.deleteLogFile(msgFlag);

    }
}