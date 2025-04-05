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

    public void listLogEntries() {
        string [] fields = null;

        if (!File.Exists(logFileName)) {
            Console.WriteLine("The Log entries file was not found");
        }
        else {
            //This approach may not work well for millions of entries, should be fine for thousands
            string logData = File.ReadAllText(logFileName);
            if (String.IsNullOrEmpty(logData)) {
                Console.WriteLine("The log file could not be read!");
                return;
            }
            
            string [] logEntries = logData.Split(';');
            foreach (string logEntry in logEntries) {
                if (String.IsNullOrEmpty(logEntry)) {
                    continue;
                }
            
                fields = logEntry.Split(',');
                Console.WriteLine("Log entry added date: " + fields[0]);
                
                //Write each sentence on separate line. Left justified.
                Console.WriteLine("Log entry: ");
                string [] lines = fields[1].Split('.');
                foreach (string line in lines) {
                    if (String.IsNullOrEmpty(line)) {
                        continue;
                    }
                    Console.WriteLine(line.TrimStart() + '.');
                }
            }
        }

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