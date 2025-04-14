namespace KoffeeKount;

using System.IO;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {   var selections = new[] {" 1 Buy a Koffee", " 2 Get Koffee count", " 3 Set a Reminder", " 4 Create a Log entry",
                                " ", " 5 List Reminders", " 6 List Log entries", " 7 Set base Koffee price", " 8 Reset files",
                                " ", " 99 Exit"};
        string selection = "";
        
        //Get the base price for cup of Koffee
        KoffeeFileHandler koffeeFH = new KoffeeFileHandler();
        string koffeePrice = koffeeFH.getKoffeePrice();

        KoffeeUI koffeeUI = new KoffeeUI();

        LogFileHandler logFH = new LogFileHandler();

        ReminderFileHandler reminderFH = new ReminderFileHandler();

        do {
            //Write main menu to console
            Console.WriteLine("Select one of the following actions (use the number). Enter 99 to terminate.");
            Console.WriteLine(String.Join(Environment.NewLine, selections));

            selection = Console.ReadLine() ?? string.Empty;
            Console.Clear();
            if (selection == "1") {
                koffeeUI.buyAKoffee(koffeePrice, koffeeFH);
            }
            else if (selection == "2") {
                koffeeUI.getKoffeeCount(koffeeFH);
            }
            else if (selection == "3") {
                writeReminderEntry(reminderFH);
            }
            else if (selection == "4") {
                writeLogEntry(logFH);
            }
            else if (selection == "5") {
                listReminderEntries(reminderFH);
            }
            else if (selection == "6") {
                listLogEntries(logFH);
            }
            else if (selection == "7") {
                koffeeUI.setBaseKoffeePrice(koffeeFH);
            }
            else if (selection == "8") {
                resetFiles(koffeeFH, logFH, reminderFH);
            }
            else if (selection == "99") {
                Console.WriteLine("Leo has left the building.");
            }
            
        } while (selection != "99");
    }


    public static void writeReminderEntry(ReminderFileHandler reminderFH) {        
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

    public static void writeLogEntry(LogFileHandler logFH) {
        Console.WriteLine("Enter Log entry.");

        string logData = Console.ReadLine() ?? string.Empty;
        if (String.IsNullOrEmpty(logData)) {
                return;
        }

        try {
            logFH.writeLogEntry(logData);
            Console.WriteLine("Log entry saved.");
        }
        catch (ArgumentException ex) {
            Console.WriteLine(ex.Message);
        }
    }

    public static void listReminderEntries(ReminderFileHandler reminderFH) {
        try {
            reminderFH.listReminderEntries();
        }
        catch (ArgumentException ex) {
            Console.WriteLine(ex.Message);
        }    
    }

    public static void listLogEntries(LogFileHandler logFH) {
        try {
            logFH.listLogEntries();
        }
        catch (ArgumentException ex) {
            Console.WriteLine(ex.Message);
        }    
    }

    public static void resetFiles(KoffeeFileHandler koffeeFH, LogFileHandler logFH, ReminderFileHandler reminderFH) {
        var options = new [] {" 1 All", " 2 Log entries", " 3 Reminders", " 4 Koffee"};
        string option = "";
        char noMsgFlag = 'N';
        char showMsgFlag = 'Y';
        do {
            //Write menu
            Console.WriteLine("Make a selection (use the number). Enter Exit to terminate.");
            Console.WriteLine(String.Join(Environment.NewLine, options));

            option = Console.ReadLine() ?? string.Empty;
            if (String.IsNullOrEmpty(option)) {
                Console.WriteLine("The input could not be read!");
                return;
            }

            switch(option) {
                case "1":                    
                    Console.WriteLine("All files to be deleted");
                    logFH.deleteLogFile(noMsgFlag);
                    reminderFH.deleteReminderFile(noMsgFlag);
                    koffeeFH.deleteKoffeeFile(noMsgFlag);
                    option = "Exit";  //Nothing more to be done
                    break;

                case "2":
                    logFH.deleteLogFile(showMsgFlag);
                    break;

                case "3":
                    reminderFH.deleteReminderFile(showMsgFlag);
                    break;

                case "4":
                    koffeeFH.deleteKoffeeFile(showMsgFlag);
                    break;

                case "Exit":
                    break;

                default:
                    Console.WriteLine("Invalid selection! Try Again.");
                    break;
            }
        } while (option != "Exit");
    }
}
