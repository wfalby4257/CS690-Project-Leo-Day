namespace KoffeeKount;

using System.IO;
class Program
{
    static void Main(string[] args)
    {   var selections = new[] {" 1 Buy a Koffee", " 2 Get Koffee count", " 3 Set a Reminder", " 4 Create a Log entry",
                                " ", " 5 List Reminders", " 6 List Log entries", " 7 Set base Koffee price", " 8 Reset files",
                                " ", " 99 Exit"};
        
        //Get the base price for cup of Koffee
        string koffeePrice = getKoffeePrice();

        //Write main menu to console
        Console.WriteLine("Select one of the following actions (use the number):");
        Console.WriteLine(String.Join(Environment.NewLine, selections));

        string selected = Console.ReadLine();
        switch (selected) {
            case "1":
                Console.WriteLine("Buy a Koffee was selected.");
                buyAKoffee();
                break;

            case "2":
                Console.WriteLine("Get Koffee count was selected.");
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
                break;

            case "99":
                Console.WriteLine("Exit was selected.");
                break;

            default:
                Console.WriteLine("Invalid selection!");
                break;
        }
    }

    public static void buyAKoffee() {
        var types = new [] {" 1 Americano", " 2 Cappuccino", " 3 DoubleDouble", " 4 Expresso", " 5 Latte"};

        //Write main menu
        Console.WriteLine("Make a selection (use the number):");
        Console.WriteLine(String.Join(Environment.NewLine, types));

        string type = Console.ReadLine();
        switch (type) {
            case "1":
                Console.WriteLine("Americano selected");
                break;

            case "2":
                Console.WriteLine("Cappuccino selected");
                break;

            case "3":
                Console.WriteLine("DoubleDouble selected");
                break;

            case "4":
                Console.WriteLine("Expresso selected");
                break;

            case "5":
                Console.WriteLine("Latte selected");
                break;

            default:
                Console.WriteLine("Invalid selection!");
                break;
        }
    }

    public static string getKoffeePrice() {
        try
        {
            // Open the text file using a stream reader.
            using StreamReader reader = new("KoffeePrice.txt");

            // Read the stream as a string.
            return(reader.ReadLine());
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            return("The file could not be read!");
        }
    }
}
