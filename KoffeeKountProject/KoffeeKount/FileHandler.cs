namespace KoffeeKount;
using System.IO;

public class KoffeeFileHandler {
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
        string fileName = "KoffeePurchases.txt";

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
        File.AppendAllText(fileName, cupOfKoffee.ToString());
    }


}