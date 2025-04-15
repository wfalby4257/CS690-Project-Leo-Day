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
        
        /*
        Put these lines here rather than in method later in the code so next developer sees them first.
        The file names are here so they are easily known. Passing the names to the file handler
        constructor makes for easier automated testing. Objects are created here to ensure they are created 
        only once.
        */
        string fileName = "KoffeePurchases.txt";
        string priceFileName = "KoffeePrice.txt";
        KoffeeFileHandler koffeeFH = new KoffeeFileHandler(fileName, priceFileName);
        //Get the base price for cup of Koffee
        string koffeePrice = koffeeFH.getKoffeePrice();
        KoffeeUI koffeeUI = new KoffeeUI(koffeeFH);

        string logFileName = "LogEntries.txt";
        LogFileHandler logFH = new LogFileHandler(logFileName);

        string reminderFileName = "ReminderEntries.txt";
        ReminderFileHandler reminderFH = new ReminderFileHandler(reminderFileName);
        ReminderUI reminderUI = new ReminderUI(reminderFH);

        do {
            //Write main menu to console
            Console.WriteLine("Select one of the following actions (use the number). Enter 99 to terminate.");
            Console.WriteLine(String.Join(Environment.NewLine, selections));

            selection = Console.ReadLine() ?? string.Empty;
            Console.Clear();
            if (selection == "1") {
                koffeeUI.buyAKoffee(koffeePrice);
            }
            else if (selection == "2") {
                koffeeUI.getKoffeeCount();
            }
            else if (selection == "3") {
                reminderUI.writeReminderEntry();
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
                koffeePrice = koffeeUI.setBaseKoffeePrice();
            }
            else if (selection == "8") {
                resetFiles(koffeeFH, logFH, reminderFH);
            }
            else if (selection == "99") {
                Console.WriteLine("Leo has left the building.");
            }
            
        } while (selection != "99");
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
