// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

 double englishEntropy = 0;
 double russianEntropy = 0;
 double binaryEntropy = 0;


string englishAbc = "abcdefghijklmnopqrstuvwxyz";
string russianAbc = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
string binary = "01";

Console.WriteLine("Task 1");
Console.WriteLine("Russian alphabet:\n");
russianEntropy = EntropyAlphabet(russianAbc, "russianText.txt", russianAbc.Count());

Console.WriteLine("English alphabet:\n");
englishEntropy = EntropyAlphabet(englishAbc, "englishText.txt", englishAbc.Count());

Console.WriteLine("Task 2");
binaryEntropy = EntropyAlphabet(binary, "binary.txt", binary.Count());

Console.WriteLine("Task 3");
CountAmountOfInformation();

Console.WriteLine("Task 4");
ErroneousTransmissionMethod();

double EntropyAlphabet(string letters, string fileName, int count)
{
    double entropy = 0;
    int countLetter = 0;
    int countLettersInFile = 0;
    double letterChance = 0;
    using (StreamReader streamReader = new StreamReader($@"D:\Univer\3курс\2сем\Криптография\Labs\Lab02\{fileName}"))
    {
        string file = streamReader.ReadToEnd().ToLower().Replace("\r", "").Replace("\n", "").Replace(" ", "");
        file = Regex.Replace(file, @"\W+", "");
        countLettersInFile = file.Count();

        Console.WriteLine($"Symbols count in file: {countLettersInFile}");
        Console.WriteLine();
        Console.WriteLine("Number of occurrences of each letter:");
        foreach (char letter in letters)
        {
            countLetter = file.Count(x => x == letter);
            Console.WriteLine($"{letter}: {countLetter}");

            letterChance = (double)countLetter / countLettersInFile;
            Console.WriteLine($"P({letter}): {letterChance}");
            Console.WriteLine();

            if (letterChance > 0)
            {
                entropy += letterChance * (Math.Log(letterChance, 2)) * (-1);
            }
        }
        Console.WriteLine("Entropy of the Shannon alphabet:");
        Console.WriteLine(entropy);
    }
    Console.WriteLine();
    return entropy;
}

void CountAmountOfInformation()
{
    string name = "Трусов Всеволод Сергеевич";
    double countInformation = russianEntropy * name.Replace(" ", "").ToLower().Count();
    Console.WriteLine("Information amount with entropy usage of russian alphabet:");
    Console.WriteLine(countInformation);
    double ascii = name.ToLower().Count() * 8;
    Console.WriteLine();
    Console.WriteLine("Information amount with ASCII usage for russian alphabet:");
    Console.WriteLine(ascii);
    Console.WriteLine();
}

void ErroneousTransmissionMethod()
{
    double[] errorChanceValues = { 0.1, 0.5, 1 };
    foreach (double chanceValue in errorChanceValues)
    {
        double invertedChance = 1 - chanceValue;
        Console.WriteLine($"Chance of erroneous transmission : {chanceValue}");
        double conditionalEntropy = -chanceValue * Math.Log(chanceValue, 2) - invertedChance * Math.Log(invertedChance, 2);
        if (double.IsNaN(conditionalEntropy))
        {
            conditionalEntropy = 0;
        }
        Console.WriteLine($"Conditional Entropy : {conditionalEntropy}");

        string name = "Трусов Всеволод Сергеевич";
        double ascii = (1 - conditionalEntropy) * name.ToLower().Count() * 8;
        Console.WriteLine();
        Console.WriteLine("Information amount with ASCII usage for russian alphabet:");
        Console.WriteLine(ascii);
        Console.WriteLine();
    }
}
