namespace KoffeeKount;

public class ReminderUI {

    public ReminderFileHandler reminderFH;

    public ReminderUI(ReminderFileHandler reminderFH) {
        this.reminderFH = reminderFH;
    }

    public void writeReminderEntry() {        
        int alertIntrvl = 0;

        Console.WriteLine("Enter reminder title: ");
        string title = Console.ReadLine() ?? string.Empty;        
        if (String.IsNullOrEmpty(title)) {            
            Console.WriteLine("Enter a title.");
            return;
        }

        Console.WriteLine("Enter reminder trigger date (MM/DD/YYYY): ");
        string triggerDate = Console.ReadLine() ?? string.Empty;        
        if (String.IsNullOrEmpty(triggerDate)) {            
            Console.WriteLine("Enter a date value.");
            return;
        }

        Console.WriteLine("Enter reminder trigger time (HH:MM AM/PM): ");
        string triggerTime = Console.ReadLine() ?? string.Empty;
        if (String.IsNullOrEmpty(triggerDate)) {
            Console.WriteLine("Enter a time value.");
            return;
        }

        Console.WriteLine("Enter an alert interval (optional): ");
        string tempVar = Console.ReadLine() ?? string.Empty;
        if (!String.IsNullOrEmpty(tempVar)) {
            alertIntrvl = int.Parse(tempVar);                
        }

        Console.WriteLine("Enter a reminder note (optional): ");
        string note = Console.ReadLine() ?? string.Empty;
        
        Reminder reminder = new Reminder(title, triggerDate, triggerTime, alertIntrvl, note); 
        try {
            reminderFH.writeReminderEntry(reminder);
            Console.WriteLine("Reminder: " + title + " set for " + triggerDate + " at " + triggerTime + ".");
        }
        catch (ArgumentException ex) {
            Console.WriteLine(ex.Message);
        }
    }
}