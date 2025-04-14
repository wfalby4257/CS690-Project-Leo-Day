namespace KoffeeKount;
using System.IO;

public class KoffeeFileHandler {
    string fileName;
    string priceFileName;

    public KoffeeFileHandler(string koffeePurchaseFile, string koffeePriceFile) {
        this.fileName = koffeePurchaseFile;
        this.priceFileName = koffeePriceFile;
    }

    public string getKoffeePrice() {
        string koffeePrice = "";
        try {
            // Open the text file using a stream reader.
            using StreamReader reader = new(priceFileName);

            // Read the stream as a string.
            koffeePrice = reader.ReadLine() ?? string.Empty;
            if (String.IsNullOrEmpty(koffeePrice)) {
                throw new ArgumentException("The Koffee price file could not be read!");
            }
            return(koffeePrice);
        }
        catch (FileNotFoundException ex) {
            Console.WriteLine(ex.Message);
            return("The Koffee price file was not found");
        }
        catch (IOException ex) {
            Console.WriteLine(ex.Message);
            return("The Koffee price file could not be read!");
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
                throw new ArgumentException("Failed creating Koffee base price file", ex);
            }
        }

        //Write data to file
        string strData = string.Format("{0},{1},{2},{3},{4};", cupOfKoffee.dateAdded, cupOfKoffee.price, cupOfKoffee.count, cupOfKoffee.type, cupOfKoffee.dayNumber);
        File.AppendAllText(fileName, strData);
    }

    public int getKoffeeCount (int days) {
        int totalKoffeeCount = 0;
        int dayNumber = 0;
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
            
                fields = purchase.Split(',');
                
                lastDayNumber = DateTime.Now.DayOfYear - days;
                dayNumber = int.Parse(fields[4]);
                if (dayNumber >= lastDayNumber) {
                    totalKoffeeCount += int.Parse(fields[2]);
                }
            }
        }

        return totalKoffeeCount;
    }

    public void setBaseKoffeePrice(string koffeePrice) {
        //If file not found, create it
        if (!File.Exists(priceFileName)) {
            try {
                File.Create(priceFileName).Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Failed creating Koffee base price file", ex);
            }
        }

        //Write data to file        
        File.WriteAllText(priceFileName, koffeePrice);
    }

    public void deleteKoffeeFile(char msgFlag) {
        //Default is do not delete the file
        string deleteFlag = "Y";

        //Is there a file?
        if (!File.Exists(fileName)) {
            Console.WriteLine("The Koffee purchases file was not found");
        }
        else {
            if (msgFlag == 'Y') {
                Console.WriteLine("Do you want to delete the Koffee purchases file? Reply Y or N.");
                deleteFlag = Console.ReadLine() ?? string.Empty;                
            }

            if (deleteFlag == "Y") {
                File.Delete(fileName);
                Console.WriteLine("The Koffee purchases file was deleted.");
            }
            else {
                Console.WriteLine("Koffee purchases file was not deleted.");
            }            
        }
    }}