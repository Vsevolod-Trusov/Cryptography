using lab07;
using System.Diagnostics;
using System.Numerics;
using System.Text;

 char[] characters = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
int p = 19;
int q = 23;
int n = p * q;
int f = (p - 1) * (q - 1);
int e = GetE();
int d = ReverseModule(e, f);
string str = "Привет как дела";
List<string> enctypt = new List<string>();
List<char> decrypt = new List<char>();

Console.WriteLine("ПСП на основе алгоритма RSA:");
PspRSA();
Console.WriteLine("\n");

str = str.ToUpper();
Stopwatch sWatch1 = new Stopwatch();
Console.WriteLine("Зашифрованное сообщение:");
EnctyptRSA();
sWatch1.Stop();
Console.WriteLine("\nВремя выполнения");
Console.WriteLine(sWatch1.ElapsedMilliseconds.ToString() + "мс");

Stopwatch sWatch2 = new Stopwatch();
Console.WriteLine("\n\nРасшифрованное сообщение:");
DectyptRSA();
sWatch2.Stop();
Console.WriteLine("\nВремя выполнения");
Console.WriteLine(sWatch2.ElapsedMilliseconds.ToString() + "мс");

Console.WriteLine();

Stopwatch sWatch3 = new Stopwatch();
sWatch3.Start();
byte[] keys = ASCIIEncoding.ASCII.GetBytes("61 60 23 22 21 20");
RC4 encoder = new RC4(keys);
string testString = "Hello how are you";
byte[] testBytes = ASCIIEncoding.ASCII.GetBytes(testString);
byte[] result = encoder.Encode(testBytes, testBytes.Length);
string encryptedString = ASCIIEncoding.ASCII.GetString(result);
Console.WriteLine(encryptedString);
RC4 decoder = new RC4(keys);
byte[] decryptedBytes = decoder.Decode(result, result.Length);
string decryptedString = ASCIIEncoding.ASCII.GetString(decryptedBytes);
Console.WriteLine(decryptedString);
sWatch3.Stop();
Console.WriteLine("Время выполнения");
Console.WriteLine(sWatch3.ElapsedMilliseconds.ToString() + "мс");


void PspRSA()
{
    BigInteger x = 10;
    for (int i = 0; i < 10; i++)
    {
        x = BigInteger.Pow(x, e) % new BigInteger(n);
        Console.Write(x + " ");
    }
}

 void EnctyptRSA()
{
    for (int i = 0; i < str.Length; i++)
    {
        int m = Array.IndexOf(characters, str[i]);
        BigInteger c = BigInteger.Pow(m, e) % new BigInteger(n);
        enctypt.Add((c).ToString());
    }
    foreach (string data in enctypt)
        Console.Write(data + " ");
}

void DectyptRSA()
{
    foreach (string data in enctypt)
    {
        int c = Convert.ToInt32(data);
        BigInteger m = BigInteger.Pow(c, d) % new BigInteger(n);
        decrypt.Add(characters[Convert.ToInt32(m.ToString())]);
    }
    foreach (char data in decrypt)
        Console.Write(data);
}

int GetE()
{
    int e = 2;
    while (true)
    {
        if (NOD(e, f) == 1) break;
        e++;
    }
    return e;
}

int NOD(int a, int b)
{
    while (a != 0 && b != 0)
    {
        if (a > b)
        {
            a = a % b;
        }
        else
        {
            b = b % a;
        }
    }
    return a + b;
}

int ReverseModule(int a, int m)
{
    a = a % m;
    for (int x = 1; x < m; x++)
    {
        if ((a * x) % m == 1)
        {
            a = x;
            break;
        }
    }
    return a;
}
 