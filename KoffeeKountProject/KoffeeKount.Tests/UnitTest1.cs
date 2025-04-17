namespace KoffeeKount.Tests;

using System.IO;
using KoffeeKount;

public class UnitTest1
{
    [Fact]
    public void WriteToLogFile()
    {
        //Always start with a new file
        string logFileName = "TestLogEntries.txt";
        if (File.Exists(logFileName)) {
            File.Delete(logFileName);
        }

        LogFileHandler logFH = new LogFileHandler(logFileName);

        //Add the log string. This will create the file.
        string tmpStr = "This is a test log entry.";
        logFH.writeLogEntry(tmpStr);

        //Get the string from the log and compare it to original
        string logData = File.ReadAllText(logFileName);
        string [] logEntries = logData.Split(',');
        string cmpStr = logEntries[1].Trim(';');
        Assert.Equal(tmpStr, cmpStr);
    }

    [Fact]
    public void DeleteLogFile()
    {
        //Always start with a new file
        string logFileName = "TestLogEntries.txt";
        if (File.Exists(logFileName)) {
            File.Delete(logFileName);
        }

        LogFileHandler logFH = new LogFileHandler(logFileName);

        //Add the log string. This will create the file.
        string tmpStr = "This is a test log entry.";
        logFH.writeLogEntry(tmpStr);

        logFH.deleteLogFile('N');
        Assert.False(File.Exists(logFileName));
    }

    [Fact]
    public void GetKoffeeCount()
    {
        //Always start with new files
        string priceFileName = "TestPriceFile.txt";
        if (File.Exists(priceFileName)) {
            File.Delete(priceFileName);
        }
        File.Create(priceFileName).Close();
        //price for cup of Koffee
        string koffeePrice = "6.75";
        File.AppendAllText(priceFileName, koffeePrice);

        string fileName = "TestKoffeePurchases.txt";
        if (File.Exists(fileName)) {
            File.Delete(fileName);
        }

        KoffeeFileHandler koffeeFH = new KoffeeFileHandler(fileName, priceFileName);

        //Add 1 cup of Koffee
        Koffee cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, "Latte");
        koffeeFH.writeKoffeeInfo(cupOfKoffee);

        Assert.Equal(1, koffeeFH.getKoffeeCount(4));
    }

    [Fact]
    public void DeleteKoffeeFile()
    {
        //Always start with new files
        string priceFileName = "TestPriceFile.txt";
        if (File.Exists(priceFileName)) {
            File.Delete(priceFileName);
        }
        File.Create(priceFileName).Close();
        //price for cup of Koffee
        string koffeePrice = "6.75";
        File.AppendAllText(priceFileName, koffeePrice);

        string fileName = "TestKoffeePurchases.txt";
        if (File.Exists(fileName)) {
            File.Delete(fileName);
        }

        KoffeeFileHandler koffeeFH = new KoffeeFileHandler(fileName, priceFileName);

        //Add 1 cup of Koffee. Purchase file is created.
        Koffee cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, "Latte");
        koffeeFH.writeKoffeeInfo(cupOfKoffee);

        koffeeFH.deleteKoffeeFile('N');
        Assert.False(File.Exists(fileName));
    }
}