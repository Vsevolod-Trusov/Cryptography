using System.Diagnostics;
using System.Text.RegularExpressions;

    const string English = "abcdefghijklmnopqrstuvwxyz";
     int s = 18;
     int k = 0;
     char[,] table;
     string EncryptRoutePermutation = "";
     string DecryptRoutePermutation = "";
     string EncryptMultiplePermutation = "";
     string DecryptMultiplePermutation = "";
     int[] key1 =  { 0, 2, 1, 4, 3, 5 }; // vsevol
     int[] key2 = { 4, 5, 3, 0, 1, 2 };  // trusov
        //---RoutePermutation---//
        MakeTableEncrypt();
        RoutePermutationEncrypt();
        MakeTableDecrypt();
        RoutePermutationDecrypt();
        //----------------------//

        //---MultiplePermutation---//
        MultiplePermutationEncrypt();
        MultiplePermutationDecrypt();
        //-------------------------//

    void MakeTableEncrypt()
    {
        using (StreamReader streamReader = new StreamReader(@"file.txt"))
        {
            string str = streamReader.ReadToEnd().ToLower().Replace("\r", "").Replace("\n", "").Replace(" ", "");
            str = Regex.Replace(str, @"\W+", "");
            k = (str.Length - 1) / s + 1;
            for (int i = str.Length; i <= k * s; i++)
            {
                str += "-";
            }
            table = new char[s, k];
            int kk = 0;
            int length = 0;
            while (kk < k)
            {
                for (int i = 0; i < s; i++)
                {
                    table[i, kk] = str[length];
                    length++;
                }
                kk++;
                if (kk == k) break;
                for (int i = s - 1; i >= 0; i--)
                {
                    table[i, kk] = str[length];
                    length++;
                }
                kk++;
            }

            Console.WriteLine($"\n---Вероятность каждый буквы в исходном тексте--- \n");
            for (int i = 0; i < English.Length; i++)
            {
                int countLetter = str.Count(x => x == English[i]);
                double probabilityLetters = (double)countLetter / str.Length;
                Console.WriteLine($"P({English[i]}): {probabilityLetters}");
            }
            Console.WriteLine();
        }
    }

      void MakeTableDecrypt()
    {
        table = new char[s, k];
        int ss = 0;
        int length = 0;
        while (ss < s)
        {
            for (int i = 0; i < k; i++)
            {
                table[ss, i] = EncryptRoutePermutation[length];
                length++;
            }
            ss++;
            if (ss == s) break;
            for (int i = k - 1; i >= 0; i--)
            {
                table[ss, i] = EncryptRoutePermutation[length];
                length++;
            }
            ss++;
        }
    }

     void RoutePermutationEncrypt()
    {
        var timer = new Stopwatch();
        timer.Start();
        int ss = 0;
        int length = 0;
        while (ss < s)
        {
            for (int i = 0; i < k; i++)
            {
                EncryptRoutePermutation += table[ss, i];
                length++;
            }
            ss++;
            if (ss == s) break;
            for (int i = k - 1; i >= 0; i--)
            {
                EncryptRoutePermutation += table[ss, i];
                length++;
            }
            ss++;
        }
        for (int i = 0; i < s; i++)
        {
            for (int j = 0; j < k; j++)
            {
                Console.Write(table[i, j] + " ");
            }
            Console.WriteLine();
        }
        timer.Stop();
        var ts = timer.Elapsed;
        Console.WriteLine(string.Format("\n---Результат шифрования маршрутной перестановкой: {0:00}:{1:00}.{2:00}---\n", ts.Minutes, ts.Seconds, ts.Milliseconds));
        Console.WriteLine(EncryptRoutePermutation);

        Console.WriteLine($"\n---Вероятность каждый буквы в зашифрованном тексте--- \n");
        for (int i = 0; i < English.Length; i++)
        {
            int countLetter = EncryptRoutePermutation.Count(x => x == English[i]);
            double probabilityLetters = (double)countLetter / EncryptRoutePermutation.Length;
            Console.WriteLine($"P({English[i]}): {probabilityLetters}");
        }
        Console.WriteLine();
    }

    void RoutePermutationDecrypt()
    {
        var timer = new Stopwatch();
        timer.Start();
        int kk = 0;
        int length = 0;
        while (kk < k)
        {
            for (int i = 0; i < s; i++)
            {
                DecryptRoutePermutation += table[i, kk];
                length++;
            }
            kk++;
            if (kk == k) break;
            for (int i = s - 1; i >= 0; i--)
            {
                DecryptRoutePermutation += table[i, kk];
                length++;
            }
            kk++;
        }
        for (int i = 0; i < s; i++)
        {
            for (int j = 0; j < k; j++)
            {
                Console.Write(table[i, j] + " ");
            }
            Console.WriteLine();
        }
        timer.Stop();
        var ts = timer.Elapsed;
        Console.WriteLine(string.Format("\n---Результат расшифрования маршрутной перестановкой: {0:00}:{1:00}.{2:00}---\n", ts.Minutes, ts.Seconds, ts.Milliseconds));
        Console.WriteLine(DecryptRoutePermutation);
        Console.WriteLine();
    }

    void MultiplePermutationEncrypt()
    {
        var timer = new Stopwatch();
        timer.Start();
        string msg = "vsevolod hello how are u man---tstrh";
        string[] msgInArray = new string[msg.Length / key1.Length];
        char[,] res = new char[key1.Length, key2.Length];
        for (int i = 0; i < (msg.Length / key1.Length) + 1; i++)
        {
            if (msg.Length - i * key1.Length <= key1.Length)
            {
                msgInArray[i] = msg.Substring(i * key1.Length);
                Console.WriteLine(msgInArray[i]);
                break;
            }
            else
            {
                msgInArray[i] = msg.Substring(i * key1.Length, key1.Length);
                Console.WriteLine(msgInArray[i]);
            }
        }
        for (int i = 0; i < key1.Length; i++)
        {
            for (int j = 0; j < key2.Length; j++)
            {
                res[key1[i], key2[j]] = msgInArray[j][i];
            }
        }
        for (int i = 0; i < key1.Length; i++)
        {
            for (int j = 0; j < key2.Length; j++)
            {
                EncryptMultiplePermutation += res[i, j];
            }
        }
        timer.Stop();
        var ts = timer.Elapsed;
        Console.WriteLine(string.Format("\n---Результат шифрования множественной перестановкой: {0:00}:{1:00}.{2:00}---\n", ts.Minutes, ts.Seconds, ts.Milliseconds));
        Console.WriteLine(EncryptMultiplePermutation);
        Console.WriteLine();
    }

     void MultiplePermutationDecrypt()
    {
        var timer = new Stopwatch();
        timer.Start();
        int length = 0;
        char[,] msgInArray = new char[key2.Length, key1.Length];
        char[,] res = new char[key2.Length, key1.Length];
        for (int i = 0; i < key1.Length; i++)
        {
            for (int j = 0; j < key2.Length; j++)
            {
                msgInArray[j, i] = EncryptMultiplePermutation[length];
                length++;
                Console.Write(msgInArray[j, i]);
            }
            Console.WriteLine();
        }
        for (int i = 0; i < key2.Length; i++)
        {
            for (int j = 0; j < key1.Length; j++)
            {
                res[i, j] = msgInArray[key2[i], key1[j]];
            }
        }
        for (int i = 0; i < key2.Length; i++)
        {
            for (int j = 0; j < key1.Length; j++)
            {
                DecryptMultiplePermutation += res[i, j];
            }
        }
        timer.Stop();
        var ts = timer.Elapsed;
        Console.WriteLine(string.Format("\n---Результат расшифрования множественной перестановкой: {0:00}:{1:00}.{2:00}---\n", ts.Minutes, ts.Seconds, ts.Milliseconds));
        Console.WriteLine(DecryptMultiplePermutation);
    }
