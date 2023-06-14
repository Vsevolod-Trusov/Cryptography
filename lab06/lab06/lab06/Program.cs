 char[,] rotors =
        {
            { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' },
            { 'j', 'p', 'g', 'v', 'o', 'u', 'm', 'f', 'y', 'q', 'b', 'e', 'n', 'h', 'z', 'r', 'd', 'k', 'a', 's', 'x', 'l', 'i', 'c', 't', 'w' },
            { 'e', 's', 'o', 'v', 'p', 'z', 'j', 'a', 'y', 'q', 'u', 'i', 'r', 'h', 'x', 'l', 'n', 'f', 't', 'g', 'k', 'd', 'c', 'm', 'w', 'b' },
            { 'a', 'j', 'd', 'k', 's', 'i', 'r', 'u', 'x', 'b', 'l', 'h', 'w', 't', 'm', 'c', 'q', 'g', 'z', 'n', 'p', 'y', 'f', 'v', 'o', 'e' }
        };

Dictionary<char, char> reflector = new Dictionary<char, char>
{
    ['a'] = 'y',
    ['b'] = 'r',
    ['c'] = 'u',
    ['d'] = 'h',
    ['e'] = 'q',
    ['f'] = 's',
    ['g'] = 'l',
    ['i'] = 'p',
    ['j'] = 'x',
    ['k'] = 'n',
    ['m'] = 'o',
    ['t'] = 'z',
    ['v'] = 'w'
};

int[] shifts = { 1, 2, 2 };


string res = EncryptEnigma("hello vsevolod how are you");
Console.WriteLine(res);

string EncryptEnigma(string key)
{
    key = key.Replace(" ", "");
    char[] result = new char[key.Length];
    result = key.ToLower().ToCharArray();
    for (int i = 0; i < key.Length; i++)
    {
        for (int j = rotors.GetLength(0) - 1; j > 0; j--)
        {
            result[i] = rotors[j, GetIndexByKey(result[i])];
        }

        foreach (var el in reflector.Keys)
        {
            if (result[i] == reflector[el])
            {
                result[i] = el;
            }
            else if (result[i] == el)
            {
                result[i] = reflector[el];
            }
        }

        for (int j = 0; j < rotors.GetLength(0); j++)
        {
            result[i] = rotors[j, GetIndexByKey(result[i])];
        }

        for (int j = 1; j < rotors.GetLength(0); j++)
        {
            char[] temp = new char[rotors.GetLength(1)];
            int number = shifts[j - 1];
            for (int k = 0; k < rotors.GetLength(1); k++)
            {
                temp[k] = (k - number < 0) ? rotors[j, rotors.GetLength(1) + (k - number)] : rotors[j, k - number];
            }
            for (int k = 0; k < rotors.GetLength(1); k++)
            {
                rotors[j, k] = temp[k];
            }
        }
    }
    return new string(result);
}

int GetIndexByKey(char key)
{
    for (int i = 0; i < rotors.GetLength(1); i++)
    {
        if (rotors[0, i] == key)
            return i;
    }
    return -1;
}

