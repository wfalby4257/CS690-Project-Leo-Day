namespace KoffeeKount;
using System.Text.RegularExpressions;

public class KoffeeUI {

    public KoffeeFileHandler koffeeFH;
    string koffeePrice;

    public KoffeeUI (KoffeeFileHandler koffeeFH) {
        //Get the base price for cup of Koffee
    this.koffeeFH = koffeeFH;
    this.koffeePrice = koffeeFH.getKoffeePrice();
    }

    public void buyAKoffee(string koffeePrice) {
        var types = new [] {" 1 Americano", " 2 Cappuccino", " 3 DoubleDouble", " 4 Expresso", " 5 Latte"};
        string type = "";
        string koffeeType = "";
        char typeSelected = 'N';

        do {
            //Write menu
            Console.WriteLine("Make a selection (use the number). Enter Exit to terminate.");
            Console.WriteLine(String.Join(Environment.NewLine, types));

            type = Console.ReadLine() ?? string.Empty;        
            if (String.IsNullOrEmpty(type)) {
                Console.WriteLine("The input could not be read!");
                //Prepare to exit the method
                type = "Exit";
            }

            switch (type) {
                case "1":
                    koffeeType = "Americano";                
                    typeSelected = 'Y';
                    break;

                case "2":
                    koffeeType = "Cappuccino";                
                    typeSelected = 'Y';
                    break;

                case "3":
                    koffeeType = "DoubleDouble";                
                    typeSelected = 'Y';
                    break;

                case "4":                
                    koffeeType = "Expresso";
                    typeSelected = 'Y';
                    break;

                case "5":
                    koffeeType = "Latte";            
                    typeSelected = 'Y';    
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
            //cupOfKoffee.showKoffee();
            try {
                koffeeFH.writeKoffeeInfo(cupOfKoffee);
                Console.WriteLine("You've purchased a cup of " + koffeeType + " for " + koffeePrice + ".");
            }
            catch (ArgumentException ex) {
                Console.WriteLine("Failed writing Koffee data!");
                Console.WriteLine(ex.Message);
            
            }
        }            
    }

    public void getKoffeeCount() {
        int totalKoffeeCount = 0;
        int days = 0;

        Console.WriteLine("Enter range in number of days: ");
        try {
            days = int.Parse(Console.ReadLine() ?? string.Empty);            
            totalKoffeeCount = koffeeFH.getKoffeeCount(days);
        }
        catch (FileNotFoundException ex) {
            Console.WriteLine("You have not made any Koffee purchases in the last " + days + " days.");
            return;
        }
        catch (FormatException ex) {
            Console.WriteLine("Enter a valid number of days.");
            //Console.WriteLine(ex.Message);
            return;
        }

        if (totalKoffeeCount > 0) {
            Console.WriteLine("Number of Koffees purchased in the last " + days + " days: " + totalKoffeeCount); 
        }
        else {
            Console.WriteLine("You have not made any Koffee purchases in the last " + days + " days.");
        }
    }

    public string setBaseKoffeePrice() {
        char validPrice = 'N';
        string price = "";
        string pattern = @"^[0-9]*\.?[0-9]+$";

        do {
            Console.WriteLine("Enter base Koffee price. Format is 9.99 (Dollars and cents) ");
            price = Console.ReadLine() ?? string.Empty;
            if (String.IsNullOrEmpty(price)) {
                Console.WriteLine("The price could not be read! Try again.");       
                continue;         
            }

            //Make sure only numbers and period
            if (Regex.IsMatch(price, pattern)) {
                validPrice = 'Y';
            }            
            else {
                Console.WriteLine("Invalid input! Try again.");
            }
        } while (validPrice == 'N');

        //Valid price entered, save it.
        koffeeFH.setBaseKoffeePrice(price);
        Console.WriteLine("Base price " + price + " saved.");
        return price;
    }
}