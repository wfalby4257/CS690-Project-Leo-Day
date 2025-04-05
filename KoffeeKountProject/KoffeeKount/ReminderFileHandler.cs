namespace KoffeeKount;
using System.IO;

public class ReminderFileHandler {
    string reminderFileName = "ReminderEntries.txt";

    public void writeReminderEntry(Reminder reminderEntry) {
        //If file not found, create it
        if (!File.Exists(reminderFileName)) {
            try {
                File.Create(reminderFileName).Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Failed creating Reminder file", ex);
            }
        }

        //Write data to file
        string strData = string.Format("{0},{1},{2},{3},{4}, {5};", reminderEntry.dateAdded, reminderEntry.triggerDate, reminderEntry.triggerTime, reminderEntry.recurCount, reminderEntry.alertIntrvl, reminderEntry.note);
        File.AppendAllText(reminderFileName, strData);
    }

    

    public void listReminderEntries() {
        string [] fields = null;

        if (!File.Exists(reminderFileName)) {
            Console.WriteLine("The Reminder entries file was not found");
        }
        else {
            //This approach may not work well for millions of entries, should be fine for thousands
            string reminderData = File.ReadAllText(reminderFileName);
            if (String.IsNullOrEmpty(reminderData)) {
                Console.WriteLine("The Reminder file could not be read!");
                return;
            }
            
            string [] reminderEntries = reminderData.Split(';');
            foreach (string reminderEntry in reminderEntries) {
                if (String.IsNullOrEmpty(reminderEntry)) {
                    continue;
                }
            
                fields = reminderEntry.Split(',');
                Console.WriteLine("Reminder entry: ");
                Console.WriteLine("Reminder entry added date: " + fields[0]);            
                Console.WriteLine("trigger date = " + fields[1]);
                Console.WriteLine("trigger time = " + fields[2]);
                Console.WriteLine("recurrence count = " + fields[3]);
                Console.WriteLine("alert interval = " + fields[4]);
                if (!String.IsNullOrEmpty(fields[5])) {
                    Console.WriteLine("reminder note = " + fields[5]);
                }                
            }
        }

    }

    public void deleteReminderFile() {
        //Is there a file?
        if (!File.Exists(reminderFileName)) {
            Console.WriteLine("The Reminder entries file was not found");
        }
        else {
            File.Delete(reminderFileName);
            Console.WriteLine("The Reminder entries file was deleted.");
        }
    }
}