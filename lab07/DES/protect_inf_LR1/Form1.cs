using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace protect_inf_LR1
{
    public partial class Form1 : Form
    {
        private const int sizeOfBlock = 128;
        private const int sizeOfChar = 16;

        private const int shiftKey = 2;

        private const int quantityOfRounds = 16;

        private string s = "Трусов В.С hello";

        private string[] keys = new string[] { "afaf", "aajf", "albj" };

        private string result = "";

        string[] Blocks;

        public Form1()
        {
            InitializeComponent();
            result = StringToRightLength(s);
            CutStringIntoBlocks(result);
        }

        //зашифровать
        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < 3; k++)
            {
                if (k == 1)
                {
                    DecodeDES(keys, k);
                }
                else
                {
                    EncodeDES(keys, k);
                }
            }
            MessageBox.Show(result);
        }

        //расшифровать
        private void buttonDecipher_Click(object sender, EventArgs e)
        {
            for (int k = 2; k >= 0; k--)
            {
                if (k == 1)
                {
                    EncodeDES(keys, k);
                }
                else
                {
                    DecodeDES(keys, k);
                }
            }
            MessageBox.Show(result);
        }

        //шифрование DES
        private void EncodeDES(string[] keys, int k)
        {
            keys[k] = CorrectKeyWord(keys[k], result.Length / (2 * Blocks.Length));
            keys[k] = StringToBinaryFormat(keys[k]);
            result = StringToBinaryFormat(result);
            CutBinaryStringIntoBlocks(result);

            for (int j = 0; j < quantityOfRounds; j++)
            {
                for (int i = 0; i < Blocks.Length; i++)
                    Blocks[i] = EncodeDES_One_Round(Blocks[i], keys[k]);

                keys[k] = KeyToNextRound(keys[k]);
            }

            keys[k] = KeyToPrevRound(keys[k]);

            keys[k] = StringFromBinaryToNormalFormat(keys[k]);

            result = "";

            for (int i = 0; i < Blocks.Length; i++)
                result += Blocks[i];

            result = StringFromBinaryToNormalFormat(result);
        }

        //расшифровка DES
        private void DecodeDES(string[] keys, int k)
        {
            keys[k] = StringToBinaryFormat(keys[k]);
            result = StringToBinaryFormat(result);
            CutBinaryStringIntoBlocks(result);
            for (int j = 0; j < quantityOfRounds; j++)
            {
                for (int i = 0; i < Blocks.Length; i++)
                    Blocks[i] = DecodeDES_One_Round(Blocks[i], keys[k]);

                keys[k] = KeyToPrevRound(keys[k]);
            }

            keys[k] = KeyToNextRound(keys[k]);

            keys[k] = StringFromBinaryToNormalFormat(keys[k]);

            result = "";

            for (int i = 0; i < Blocks.Length; i++)
                result += Blocks[i];

            result = StringFromBinaryToNormalFormat(result);
        }

        //доводим строку до размера, чтобы делилась на sizeOfBlock
        private string StringToRightLength(string input)
        {
            while (((input.Length * sizeOfChar) % sizeOfBlock) != 0)
                input += "0";

            return input;
        }

        //разбиение обычной строки на блоки
        private void CutStringIntoBlocks(string input)
        {
            Blocks = new string[(input.Length * sizeOfChar) / sizeOfBlock];

            int lengthOfBlock = input.Length / Blocks.Length;

            for (int i = 0; i < Blocks.Length; i++)
            {
                Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
                Blocks[i] = StringToBinaryFormat(Blocks[i]);
            }
        }

        //разбиение двоичной строки на блоки
        private void CutBinaryStringIntoBlocks(string input)
        {
            Blocks = new string[input.Length / sizeOfBlock];

            int lengthOfBlock = input.Length / Blocks.Length;

            for (int i = 0; i < Blocks.Length; i++)
                Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
        }

        //перевод строки в двоичный формат
        private string StringToBinaryFormat(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                string char_binary = Convert.ToString(input[i], 2);

                while (char_binary.Length < sizeOfChar)
                    char_binary = "0" + char_binary;

                output += char_binary;
            }

            return output;
        }

        //доводим длину ключа до нужной
        private string CorrectKeyWord(string input, int lengthKey)
        {
            if (input.Length > lengthKey)
                input = input.Substring(0, lengthKey);
            else
                while (input.Length < lengthKey)
                    input = "0" + input;

            return input;
        }

        //шифрование DES один раунд
        private string EncodeDES_One_Round(string input, string key)
        {
            string L = input.Substring(0, input.Length / 2);
            string R = input.Substring(input.Length / 2, input.Length / 2);

            return (R + XOR(L, f(R, key)));
        }

        //расшифровка DES один раунд
        private string DecodeDES_One_Round(string input, string key)
        {
            string L = input.Substring(0, input.Length / 2);
            string R = input.Substring(input.Length / 2, input.Length / 2);

            return (XOR(f(L, key), R) + L);
        }

        //XOR двух строк с двоичными данными
        private string XOR(string s1, string s2)
        {
            string result = "";

            for (int i = 0; i < s1.Length; i++)
            {
                bool a = Convert.ToBoolean(Convert.ToInt32(s1[i].ToString()));
                bool b = Convert.ToBoolean(Convert.ToInt32(s2[i].ToString()));

                if (a ^ b)
                    result += "1";
                else
                    result += "0";
            }
            return result;
        }

        //шифрующая функция f. в данном случае это XOR
        private string f(string s1, string s2)
        {
            return XOR(s1, s2);
        }

        //вычисление ключа для следующего раунда шифрования. циклический сдвиг >> 2
        private string KeyToNextRound(string key)
        {
            for (int i = 0; i < shiftKey; i++)
            {
                key = key[key.Length - 1] + key;
                key = key.Remove(key.Length - 1);
            }

            return key;
        }

        //вычисление ключа для следующего раунда расшифровки. циклический сдвиг << 2
        private string KeyToPrevRound(string key)
        {
            for (int i = 0; i < shiftKey; i++)
            {
                key = key + key[0];
                key = key.Remove(0, 1);
            }

            return key;
        }

        //переводим строку с двоичными данными в символьный формат
        private string StringFromBinaryToNormalFormat(string input)
        {
            string output = "";

            while (input.Length > 0)
            {
                string char_binary = input.Substring(0, sizeOfChar);
                input = input.Remove(0, sizeOfChar);

                int a = 0;
                int degree = char_binary.Length - 1;

                foreach (char c in char_binary)
                    a += Convert.ToInt32(c.ToString()) * (int)Math.Pow(2, degree--);

                output += ((char)a).ToString();
            }

            return output;
        }
    }
}
