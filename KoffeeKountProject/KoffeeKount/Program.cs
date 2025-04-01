namespace KoffeeKount;

using System.IO;
class Program
{
    static void Main(string[] args)
    {
        string koffeePrice = getKoffeePrice();
        // Write the text to the console.
        Console.WriteLine(koffeePrice);

        //Write main menu to console
        
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
