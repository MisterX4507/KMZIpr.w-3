using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MathNet.Numerics;
using System.Numerics;

namespace шифр_Вижинера
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Шифр Виженера: 1-шифрование");
            Console.WriteLine("Шифр Виженера: 2-расшифрование");
            Console.Write("Введите номер операции: ");
            string S; int n = Convert.ToInt32(Console.ReadLine());
            int i; int m = 32; 
            if (n == 1)
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\User\Desktop\open.txt", Encoding.UTF8))
                {
                    S = sr.ReadToEnd();
                }
                int[] gamma = new int[S.Length]; int[] buf1 = new int[S.Length];
                char[] buf2 = new char[S.Length];
                Console.WriteLine("1-Многократное пвторение ключа");
                Console.WriteLine("2-Самоключ Виженера на основе открытого текста");
                Console.WriteLine("3-Самоключ Виженера на основе шифртекста");
                Console.Write("Выберите вариант формирования гаммы: "); int n1 = Convert.ToInt32(Console.ReadLine());
                if (n1 == 1)
                {
                    mnogokrat(ref gamma, S);
                }
                if (n1 == 2)
                {
                    smesh(ref gamma, S);
                }
                if (n1 == 3)
                {
                    loading(ref gamma, S);
                }
                for (i = 0; i < S.Length; i++)
                {
                    if (((int)S[i] < 1072) || ((int)S[i] > 1104))
                    {
                        buf2[i] = '-'; continue;
                    }
                    buf1[i] = (int)S[i] - 1072;
                    buf2[i] = (char)((Euclid.Modulus((buf1[i] + gamma[i]),m))+1072);
                    if ((n1 == 3) && (i < (S.Length-1)))
                    {
                        gamma[i+1] = Euclid.Modulus((buf1[i] + gamma[i]),m);
                    }
                }
                using (StreamWriter sw = new StreamWriter(@"C:\Users\User\Desktop\close.txt", false))
                {
                    for (i = 0; i < buf2.Length; i++)
                        sw.Write(buf2[i]);
                }
            }
            if (n == 2)
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\User\Desktop\close.txt", Encoding.UTF8))
                {
                    S = sr.ReadToEnd();
                }
                int[] gamma = new int[S.Length]; int[] buf1 = new int[S.Length];
                char[] buf2 = new char[S.Length];
                Console.WriteLine("1-Многократное пвторение ключа");
                Console.WriteLine("2-Самоключ Виженера основе открытого текста");
                Console.WriteLine("3-Самоключ Виженера на основе шифртекста");
                Console.Write("Выберите вариант формирования гаммы: "); int n1 = Convert.ToInt32(Console.ReadLine());
                if (n1 == 1)
                {
                    mnogokrat(ref gamma, S);
                }
                if (n1 == 2)
                {
                    loading(ref gamma, S);
                    
                }
                if (n1 == 3)
                {
                    smesh(ref gamma, S);
                }
                for (i = 0; i < S.Length; i++)
                {
                    if (((int)S[i] < 1072) || ((int)S[i] > 1104))
                    {
                        buf2[i] = '-'; continue;
                    }
                    buf1[i] = (int)S[i] - 1072;
                    buf2[i] = (char)((Euclid.Modulus((buf1[i] - gamma[i]), m)) + 1072);
                    if ((n1 == 2) && (i < (S.Length - 1)))
                    {
                        gamma[i + 1] = Euclid.Modulus((buf1[i] - gamma[i]), m);
                    }
                }
                using (StreamWriter sw = new StreamWriter(@"C:\Users\User\Desktop\open.txt", false))
                {
                    for (i = 0; i < buf2.Length; i++)
                        sw.Write(buf2[i]);
                }
            }
        }
        static void mnogokrat(ref int[] gamma, string S)
        {
            string G; bool q = false; int i, j;
            do
            {
                Console.WriteLine("Введите ключ: ");
                G = Console.ReadLine();
                if (G.Length > S.Length)
                {
                    Console.WriteLine("Некорректный ключ: его длина больше длины текста");
                    q = false; continue;
                }
                for (i = 0; i < G.Length; i++)
                {
                    for (j = i; j < gamma.Length; j = j + G.Length)
                    {
                        if (((int)G[i] >= 1072) && ((int)G[i] <= 1104))
                        {
                            gamma[j] = (int)G[i] - 1072;
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ключ: формируемая гамма содержит некорректные символы");
                            q = false; continue;
                        }
                    }
                }
                q = true;
            }
            while (q == false);
        }
        static void smesh(ref int[] gamma, string S)
        {
            string G; int i; bool q = false;
            do
            {
                Console.Write("Введите начальный символ гаммы: "); G = Console.ReadLine();
                if ((G.Length != 1) || (G[0]<1072) || (G[0]>1104))
                {
                    Console.WriteLine("Некорректный ключ: неверный начальный символ гаммы");
                    q = false; continue;
                }
                else
                {
                    for (i=0; i<gamma.Length; i++)
                    {
                        if (i == 0) {gamma[i] = (int)G[i]-1072;}
                        else {gamma[i] = (int)S[i-1]-1072;}
                    }
                    q = true;
                }
            }
            while (q == false);
        }
        static void loading(ref int[] gamma, string S)
        {
            string G; bool q = false;
            do
            {
                Console.Write("Введите начальный символ гаммы: "); G = Console.ReadLine();
                if ((G.Length != 1) || (G[0] < 1072) || (G[0] > 1104))
                {
                    Console.WriteLine("Некорректный ключ: неверный начальный символ гаммы");
                    q = false; continue;
                }
                else
                {
                    gamma[0] = (int)G[0] - 1072;
                    q = true;
                }
            }
            while (q == false);
        }
    }
}
