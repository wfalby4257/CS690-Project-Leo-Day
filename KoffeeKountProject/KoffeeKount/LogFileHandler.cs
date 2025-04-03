namespace KoffeeKount;
using System.IO;

public class LogFileHandler {
    string logFileName = "LogEntries.txt";

    public void writeLogEntry(string logEntry) {
        //If file not found, create it
        if (!File.Exists(logFileName)) {
            try {
                File.Create(logFileName).Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Failed creating file", ex);
            }
        }

        //Write data to file
        string strData = string.Format("{0},{1};", DateTime.Now, logEntry);
        File.AppendAllText(logFileName, strData);
    }

    public void deleteLogFile() {
        //Is there a file?
        if (!File.Exists(logFileName)) {
            Console.WriteLine("The Log entries file was not found");
        }
        else {
            File.Delete(logFileName);
            Console.WriteLine("The Log entries file was deleted.");
        }
    }
}