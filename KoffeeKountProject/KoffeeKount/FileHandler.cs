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

    public int getKoffeeCount (int days) {
        int totalKoffeeCount = 0;
        int dayNumber = 0;
        int currentDay = 0;
        int lastDayNumber = 0;
        string [] fields = null;

        //If file does not exist, throw not found exception
        if (!File.Exists(fileName)) {
            throw new FileNotFoundException("Koffee purchases file not found");
        }
        else {
            //This approach may not work well for millions of entries, should be fine for thousands
            string koffeeData = File.ReadAllText(fileName);
            string [] purchases = koffeeData.Split(';');
            foreach (string purchase in purchases) {
                if (String.IsNullOrEmpty(purchase)) {
                    continue;
                }
                Console.WriteLine(purchase);
                fields = purchase.Split(',');
                Console.WriteLine(fields[0]);
                Console.WriteLine(fields[1]);
                Console.WriteLine(fields[2]);
                Console.WriteLine(fields[3]);
                Console.WriteLine(fields[4]);
                lastDayNumber = DateTime.Now.DayOfYear - days;
                dayNumber = int.Parse(fields[4]);
                if (dayNumber >= lastDayNumber) {
                    totalKoffeeCount += int.Parse(fields[2]);
                }
            }
        }

        return totalKoffeeCount;
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