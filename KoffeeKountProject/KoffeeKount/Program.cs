namespace KoffeeKount;

using System.IO;
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

        LogFileHandler logFH = new LogFileHandler();

        ReminderFileHandler reminderFH = new ReminderFileHandler();

        do {
            //Write main menu to console
            Console.WriteLine("Select one of the following actions (use the number). Enter 99 to terminate.");
            Console.WriteLine(String.Join(Environment.NewLine, selections));

            selection = Console.ReadLine();
            switch (selection) {
                case "1":
                    Console.WriteLine("Buy a Koffee was selected.");
                    buyAKoffee(koffeePrice, koffeeFH);
                    break;

                case "2":
                    Console.WriteLine("Get Koffee count was selected.");
                    getKoffeeCount(koffeeFH);
                    break;

                case "3":
                    Console.WriteLine("Set a Reminder was selected.");
                    writeReminderEntry(reminderFH);
                    break;    

                case "4":
                    Console.WriteLine("Create a Log entry was selected.");
                    writeLogEntry(logFH);
                    break;

                case "5":
                    Console.WriteLine("List Reminders was selected.");                
                    break;

                case "6":
                    Console.WriteLine("List Log entries was selected.");
                    listLogEntries(logFH);
                    break;

                case "7":
                    Console.WriteLine("Set base Koffee price was selected.");
                    break;

                case "8":
                    Console.WriteLine("Reset files was selected.");
                    resetFiles(koffeeFH, logFH, reminderFH);
                    break;

                case "99":
                    Console.WriteLine("Leo has left the building.");
                    break;

                default:
                    Console.WriteLine("Invalid selection! Try again.");
                    break;
            }
        } while (selection != "99");
    }

    public static void buyAKoffee(string koffeePrice, KoffeeFileHandler koffeeFH) {
        var types = new [] {" 1 Americano", " 2 Cappuccino", " 3 DoubleDouble", " 4 Expresso", " 5 Latte"};
        string type = "";
        string koffeeType = "";
        char typeSelected = 'N';

        do {
            //Write menu
            Console.WriteLine("Make a selection (use the number). Enter Exit to terminate.");
            Console.WriteLine(String.Join(Environment.NewLine, types));

            type = Console.ReadLine();        
            if (String.IsNullOrEmpty(type)) {
                Console.WriteLine("The input could not be read!");
                //Prepare to exit the method
                type = "Exit";
            }

            switch (type) {
                case "1":
                    Console.WriteLine("Americano selected");
                    koffeeType = "Americano";                
                    typeSelected = 'Y';
                    break;

                case "2":
                    koffeeType = "Cappuccino";                
                    typeSelected = 'Y';
                    Console.WriteLine("Cappuccino selected");
                    break;

                case "3":
                    koffeeType = "DoubleDouble";                
                    typeSelected = 'Y';
                    Console.WriteLine("DoubleDouble selected");
                    break;

                case "4":                
                    koffeeType = "Expresso";
                    typeSelected = 'Y';
                    Console.WriteLine("Expresso selected");
                    break;

                case "5":
                    koffeeType = "Latte";            
                    typeSelected = 'Y';    
                    Console.WriteLine("Latte selected");
                    break;

                case "Exit":
                    return;

                default:
                    Console.WriteLine("Invalid selection! Try again.");
                    break;
            }
        } while (typeSelected == 'N');

        if (typeSelected == 'Y') {
            //Reset flag for next selection
            typeSelected = 'N';

            Koffee cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, koffeeType);
            cupOfKoffee.showKoffee();
            try {
                koffeeFH.writeKoffeeInfo(cupOfKoffee);
            }
            catch (ArgumentException ex) {
                Console.WriteLine("Failed writing Koffee data!");
                Console.WriteLine(ex.Message);
            
            }
        }            
    }

    public static void getKoffeeCount(KoffeeFileHandler koffeeFH) {
        int totalKoffeeCount = 0;

        Console.WriteLine("Enter range in number of days: ");
        int days = int.Parse(Console.ReadLine());
        try {
            totalKoffeeCount = koffeeFH.getKoffeeCount(days);
        }
        catch (FileNotFoundException ex) {
            Console.WriteLine(" You have not made any Koffee purchases in the last " + days + " days.");
        }

        if (totalKoffeeCount > 0) {
            Console.WriteLine("Number of Koffees purchased in the last " + days + " days: " + totalKoffeeCount); 
        }
        else {
            Console.WriteLine(" You have not made any Koffee purchases in the last " + days + " days.");
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

        //Write menu
        Console.WriteLine("Make a selection (use the number). Enter Exit to terminate.");
        Console.WriteLine(String.Join(Environment.NewLine, options));

        do {
            option = Console.ReadLine();
            if (String.IsNullOrEmpty(option)) {
                Console.WriteLine("The input could not be read!");
                return;
            }

            switch(option) {
                case "1":
                    Console.WriteLine("All files to be deleted");
                    logFH.deleteLogFile();
                    reminderFH.deleteReminderFile();
                    koffeeFH.deleteKoffeeFile();
                    break;

                case "2":
                    Console.WriteLine("Log entries file to be deleted");
                    logFH.deleteLogFile();
                    break;

                case "3":
                    Console.WriteLine("Reminders file to be deleted");
                    reminderFH.deleteReminderFile();
                    break;

                case "4":
                    Console.WriteLine("Koffee purchases file to be deleted");
                    koffeeFH.deleteKoffeeFile();
                    break;

                case "Exit":
                    break;

                default:
                    Console.WriteLine("Invalid selection! Try Again.");
                    break;
            }
        } while (option != "Exit");
    }

    public static void writeLogEntry(LogFileHandler logFH) {
        Console.WriteLine("Enter Log entry.");

        string logData = Console.ReadLine();

        try {
            logFH.writeLogEntry(logData);
            Console.WriteLine("Log entry saved.");
        }
        catch (ArgumentException ex) {
            Console.WriteLine(ex.Message);
        }
    }
}
