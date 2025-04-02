namespace KoffeeKount;
using System.IO;

public class KoffeeFileHandler {
    string fileName = "KoffeePurchases.txt";
    public string getKoffeePrice() {
        string koffeePrice = "";
        try {
            // Open the text file using a stream reader.
            using StreamReader reader = new("KoffeePrice.txt");

            // Read the stream as a string.
            koffeePrice = reader.ReadLine();
            return(koffeePrice);
        }
        catch (FileNotFoundException ex) {
            Console.WriteLine(ex.Message);
            return("The file was not found");
        }
        catch (IOException ex) {
            Console.WriteLine(ex.Message);
            return("The file could not be read!");
        }
    }

    public void writeKoffeeInfo(Koffee cupOfKoffee) {
        //If file not found, create it
        if (!File.Exists(fileName)) {
            try {
                File.Create(fileName).Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Failed creating file", ex);
            }
        }

        //Write data to file
        string strData = string.Format("{0},{1},{2},{3},{4};", cupOfKoffee.dateAdded, cupOfKoffee.price, cupOfKoffee.count, cupOfKoffee.type, cupOfKoffee.dayNumber);
        File.AppendAllText(fileName, strData);
    }

    public void getKoffeeCount () {
        //If file does not exist, throw not found exception
        if (!File.Exists(fileName)) {
            throw new FileNotFoundException("Koffee purchases file not found");
        }
        else {
            string koffeeData = File.ReadAllText(fileName);
            string [] purchases = koffeeData.Split(';');
            foreach (string purchase in purchases) {
                Console.WriteLine(purchase);
                string [] fields = purchase.Split(',');
                DateTime dateAdded = DateTime.Parse(fields[0]);
                double price = double.Parse(fields[1]);
                int count = int.Parse(fields[2]);
                string type = fields[3];
                int dayNumber = int.Parse(fields[4]);
            }
        }
    }
    public void deleteKoffeeFile() {
        //Is there a file?
        if (!File.Exists(fileName)) {
            Console.WriteLine("The Koffee purchases file was not found");
        }
        else {
            File.Delete(fileName);
            Console.WriteLine("The Koffee purchases file was deleted.");
        }
    }
}