namespace KoffeeKount;

using System.IO;
class Program
{
    static void Main(string[] args)
    {   var selections = new[] {" 1 Buy a Koffee", " 2 Get Koffee count", " 3 Set a Reminder", " 4 Create a Log entry",
                                " ", " 5 List Reminders", " 6 List Log entries", " 7 Set base Koffee price", " 8 Reset files",
                                " ", " 99 Exit"};
        
        //Get the base price for cup of Koffee
        KoffeeFileHandler koffeeFH = new KoffeeFileHandler();
        string koffeePrice = koffeeFH.getKoffeePrice();

        //Write main menu to console
        Console.WriteLine("Select one of the following actions (use the number):");
        Console.WriteLine(String.Join(Environment.NewLine, selections));

        string selection = Console.ReadLine();
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
                break;    

            case "4":
                Console.WriteLine("Create a Log entry was selected.");
                break;

            case "5":
                Console.WriteLine("List Reminders was selected.");
                break;

            case "6":
                Console.WriteLine("List Log entries was selected.");
                break;

            case "7":
                Console.WriteLine("Set base Koffee price was selected.");
                break;

            case "8":
                Console.WriteLine("Reset files was selected.");
                resetFiles(koffeeFH);
                break;

            case "99":
                Console.WriteLine("Leo has left the building.");
                break;

            default:
                Console.WriteLine("Invalid selection!");
                break;
        }
    }

    public static void buyAKoffee(string koffeePrice, KoffeeFileHandler koffeeFH) {
        var types = new [] {" 1 Americano", " 2 Cappuccino", " 3 DoubleDouble", " 4 Expresso", " 5 Latte"};

        //Write main menu
        Console.WriteLine("Make a selection (use the number):");
        Console.WriteLine(String.Join(Environment.NewLine, types));

        string type = Console.ReadLine();
        Koffee cupOfKoffee = null;
        switch (type) {
            case "1":
                Console.WriteLine("Americano selected");
                cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, "Americano");
                break;

            case "2":
                cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, "Cappuccino");    
                Console.WriteLine("Cappuccino selected");
                break;

            case "3":
                cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, "DoubleDouble");
                Console.WriteLine("DoubleDouble selected");
                break;

            case "4":
                cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, "Expresso");
                Console.WriteLine("Expresso selected");
                break;

            case "5":
                cupOfKoffee = new Koffee(double.Parse(koffeePrice), 1, "Latte");
                Console.WriteLine("Latte selected");
                break;

            default:
                Console.WriteLine("Invalid selection!");
                break;
        }

        cupOfKoffee.showKoffee();
        try {
            koffeeFH.writeKoffeeInfo(cupOfKoffee);
        }
        catch (ArgumentException ex) {
            Console.WriteLine("Failed writing Koffee data!");
            Console.WriteLine(ex.Message);
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

    public static void resetFiles(KoffeeFileHandler koffeeFH) {
        var options = new [] {" 1 All", " 2 Log entries", " 3 Reminders", " 4 Koffee"};

        //Write menu
        Console.WriteLine("Make a selection (use the number):");
        Console.WriteLine(String.Join(Environment.NewLine, options));

        string option = Console.ReadLine();
        switch(option) {
            case "1":
                Console.WriteLine("All files to be deleted");
                break;

            case "2":
                Console.WriteLine("Log entries file to be deleted");
                break;

            case "3":
                Console.WriteLine("Reminders file to be deleted");
                break;

            case "4":
                Console.WriteLine("Koffee purchases file to be deleted");
                koffeeFH.deleteKoffeeFile();
                break;

            default:
                Console.WriteLine("Invalid selection! Try Again.");
                break;
        }

    }
}
