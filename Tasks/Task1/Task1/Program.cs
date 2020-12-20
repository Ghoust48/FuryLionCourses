// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Text;

namespace Task1
{
    internal class Program
    {
        private static int ReadInt()
        {
            int value;

            while (!int.TryParse(Console.ReadLine(), out value))
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");

            return value;
        }

        private static void SubTask1()
        {
            // С клавиатуры вводятся 3 числа. Вывести их в порядке возрастания

            var arr = new int[3];

            Console.WriteLine("Пожалуйста введите 3 числа(Например: 4, 7, 2): ");

            for (var i = 0; i < arr.Length; i++)
                arr[i] = ReadInt();
            
            Array.Sort(arr);
            ArrayOutput(arr);
        }
        
        private static void SubTask2()
        {
            // С клавиатуры вводится число, вывести его в обратном порядке 0;
            
            Console.WriteLine("Пожалуйста введите число(Например: 358): ");

            var number = ReadInt();

            Console.WriteLine($"Результат: {ReversDigits(number)} ");
        }

        private static int ReversDigits(int value)
        {
            var result = 0;

            while (value > 0)
            {
                result = result * 10 + value % 10;
                value = value / 10;
            }

            return result;
        }

        private static void SubTask3()
        {
            // Реализовать метод вычисляющий факториал из числа введенного с клавиатуры  

            Console.WriteLine("Пожалуйста введите число для вычисления факториала: ");
            var number = ReadInt();

            while (number < 0)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                number = ReadInt();
            }

            Console.Write($"Результат: {Factorial(number)}");
        }

        private static void SubTask4()
        {
            // Получить сумму первых N чисел с шагом M от числа X

            var sum = 0;

            Console.Write("Введите число \nX = ");
            var x = ReadInt();
           
            Console.Write("N = ");
            var n = ReadInt();

            while (n <= 0)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                n = ReadInt();
            }

            Console.Write("M = ");
            var m = ReadInt();

            while (m <= 0)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                m = ReadInt();
            }

            for (var i = x; n > 0; i += m, n--)
                sum += i;

            Console.WriteLine($"Результат: = {sum}");
        }

        private static int SubMenu(int selectMenu)
        {
            // Метод для вызова проверки и ввода числа, используется в SubTask5 - SubTask6

            Console.Write("Введите число \nX = ");
            var x = ReadInt();

            while (x <= 0)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                x = ReadInt();
            }

            if (selectMenu == 5)
            {
                var result = SubTask5(x);
                for (var i = 0; i < result.Length; i++)
                    Console.WriteLine($"{result[i]}");
            }
            else if (selectMenu == 6)
            {
                for (var i = 1; i < x; i++)
                    if (SubTask6(i) && i != 1)
                        Console.WriteLine("Совершенное число: {0} ", i);

            }
            return x;
        }

        private static int[] SubTask5(int value)
        {
            // Реализовать метод, который получает число X и возвращает все четные числа в диапазоне от 0..X

            var count = 0;
            var size = 0;

            for (var i = 2; i <= value; i += 2)
                count++;

            var array = new int[count];

            for (var y = 2; y <= value; y += 2, size++)
                array[size] = y;

            return array;
        }

        private static bool SubTask6(int value)
        {
            // Реализовать метод который получает число X и возвращает все cовершенные числа в диапазоне 0..X

            var sumDividers = 1;

            for (var i = 2; i < value / 2 + 1; i++)
            {
                if (value % i == 0)
                    sumDividers += i;
            }

            return (sumDividers == value);
        }
        
        private static void SubTask7()
        {
            // Дан одномерный массив получить сумму всех элементов расположенных в четных и отдельно в нечетных позициях.

            var arr = InputArray();
            var sumEven = 0;
            var sumOdd = 0;

            for (var i = 1; i < arr.Length; i += 2)
                sumEven += arr[i];

            for (var i = 0; i < arr.Length; i += 2)
                sumOdd += arr[i];
            
            Console.WriteLine($"Сумма в четных = {sumEven} и нечётных = {sumOdd} позициях");
        }

        private static void SubTask8()
        {
            // Дан одномерный массив отсортировать отрицательные элементы

            var arr = InputArray();

            for (var i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                    i++;
                for (var j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] < 0 && arr[j] < 0 && arr[i] > arr[j])
                    {
                        var tmp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }
            ArrayOutput(arr);
        }

        private static void SubTask9()
        {
            // Дан массив преобразовать его, так чтобы последний поменялась с первой предпоследний с вторым и т.д

            var arr = InputArray();

            for (int i = 0, j = arr.Length - 1; i < arr.Length / 2 && j / 2 > 0; i++, j--)
            {
                var tmp = arr[i];
                arr[i] = arr[j];
                arr[j] = tmp;
            }
            ArrayOutput(arr);
        }

        private static void SubTask10()
        {
            // Дан одномерный массив, получить сумму элементов которые: a) больше числа M b) Меньше числа N

            var arr = InputArray();
            var sumM = 0;
            var sumN = 0;

            Console.Write("Введите число\nM = ");
            var m = ReadInt();

            Console.Write("N = ");
            var n = ReadInt();

            foreach (var a in arr)
            {
                if (a < n)
                    sumN += a;
                else if (a > m)
                    sumM += a;
            }

            Console.WriteLine($"Сумма больших числа M {sumM} и меньших числа N {sumN}");
        }

        private static void SubTask11()
        {
            // Дан двумерный массив, поменять местами четные строки с нечетными.

            var arr = InputMatrix();

            for (var i = 0; i < arr.GetLength(0) - 1; i += 2)
                for (var j = 0; j < arr.GetLength(1); j++)
                {
                    var tmp = arr[i, j];
                    arr[i, j] = arr[i + 1, j];
                    arr[i + 1, j] = tmp;
                }
            OutputMatrix(arr);
        }

        private static void SubTask12()
        {
            // Дан двумерный массив получить максимальные элемента каждого столбца

            int max;
            var arr = InputMatrix();

            for (var j = 0; j < arr.GetLength(1); j++)
            {
                max = arr[0, j];
                for (var i = 1; i < arr.GetLength(0); i++)
                {
                    if (max < arr[i, j])
                        max = arr[i, j];
                }

                Console.WriteLine($"Результат: {j} столбец {max} ");
            }
        }

        private static void SubTask13()
        {
            // Дан двумерный массив поменять местами элементы, расположенные на главной диагонали с противоположной диагонально

            var lessSize = 0;
            var arr = InputMatrix();

            if (arr.GetLength(0) < arr.GetLength(1))
                lessSize = arr.GetLength(0);
            else if (arr.GetLength(1) < arr.GetLength(0))
                lessSize = arr.GetLength(1);
            else if (arr.GetLength(1) == arr.GetLength(0))
                lessSize = arr.GetLength(1);

            for (int i = 0, k = arr.GetLength(1) - 1; i < lessSize && k >= 0; i++, k--)
            {
                var tmp = arr[i, i];
                arr[i, i] = arr[i, k];
                arr[i, k] = tmp;
            }

            OutputMatrix(arr);
        }

        private static void SubTask14()
        {
            // Дана строка, посчитать количество символов "A"

            var result = 0;
            var str = InputString();

            foreach (var i in str)
                if (i == 'a' || i == 'A')
                    result++;
            
            Console.Write($"Результат: {result}");
        }

        private static void SubTask15()
        {
            // Дана строка, посчитать количество слов

            var str = InputString();

            var words = str.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            Console.Write($"Результат: {words.Length}");
        }

        private static void SubTask16()
        {
            // Дана строка, перевернуть ее

            var str = InputString();

            Console.Write($"Результат: ");

            for (var i = str.Length - 1; i >= 0; i--)
                Console.Write(str[i]);
        }

        private static void SubTask17()
        {
            // Дана строка, поменять все символы "C" на "E".

            var str = new StringBuilder(InputString());

            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == 'C' || str[i] == 'С')
                    str[i] = 'E';
                else if (str[i] == 'c' || str[i] == 'с')
                    str[i] = 'e';
            }

            Console.Write($"Результат: {str}");
        }

        private static void SubTask18()
        {
            // Дана строка, посчитать количество слов, которые начинаются на букву "K".

            var str = InputString();
            var count = 0;

            var words = str.Split(new[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < words.GetLength(0); i++)
                if (words[i].StartsWith('k') || words[i].StartsWith('K') || words[i].StartsWith('к') || words[i].StartsWith('К'))
                    count++;
            
            Console.Write($"Результат: {count}");
        }

        private static bool PerfectNumbers(int x)
        {
            var sumDividers = 1;

            for (var i = 2; i < x / 2 + 1; i++)
            {
                if (x % i == 0)
                    sumDividers += i;
            }

            return (sumDividers == x);
        }

        private static int[] EvenNumber(int x)
        {
            var countNumber = 0;
            var arraySize = 0;

            for (var i = 2; i <= x; i += 2)
                countNumber++;

            var array = new int[countNumber];

            for (var y = 2; y <= x; y += 2, arraySize++)
                array[arraySize] = y;

            return array;
        }

        private static int Factorial(int value)
        {
            var result = 1;

            for (var i = 1; i <= value; i++)
                result *= i;

            return (value == 0) ? 1 : result;
        }

        private static string InputString()
        {
            Console.Write("Введите строку: ");
            var str = Console.ReadLine();

            return str;
        }

        private static int[,] InputMatrix()
        {
            Console.Write("Введите размерность массива: \nN = ");
            var n = ReadInt();

            while (n <= 0)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                n = ReadInt();
            }

            Console.Write("M = ");
            var m = ReadInt();

            while (m <= 0)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                m = ReadInt();
            }

            var arr = new int[n, m];
            var rand = new Random();

            for (var i = 0; i < arr.GetLength(0); i++)
                for (var j = 0; j < arr.GetLength(1); j++)
                    arr[i, j] = rand.Next(-100, 100);

            OutputMatrix(arr);

            return arr;
        }

        private static void OutputMatrix(int[,] arr)
        {
            Console.WriteLine("Результат: ");

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write($"{arr[i, j]}\t");
                
                Console.WriteLine();
            }
        }

        private static int[] InputArray()
        {
            Console.Write("Введите размерность массива: ");

            var n = ReadInt();

            while (n <= 0)
            {
                Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                n = ReadInt();
            }

            var arr = new int[n];
            var rand = new Random();

            for (var i = 0; i < arr.Length; i++)
                arr[i] = rand.Next(-100, 100);

            ArrayOutput(arr);

            return arr;
        }

        private static void ArrayOutput(int[] arr)
        {
            Console.Write("Результат: ");

            foreach (var i in arr)
                Console.Write($"{i} ");

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int selectMenu;
            var isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine("1. С клавиатуры вводятся 3 числа. Вывести их в порядке возрастания.");
                Console.WriteLine("2. С клавиатуры вводится число, вывести его в обратном порядке.");
                Console.WriteLine("3. Реализовать метод вычисляющий факториал из числа введенного с клавиатуры.");
                Console.WriteLine("4. Получить сумму первых N чисел с шагом M от числа X (X=10 N=5 M=2 > 10+12+14+16+18=70).");
                Console.WriteLine("5. Реализовать метод, который получает число X и возвращает все четные числа в диапазоне от 0..X (X=8 > 2, 4, 6, 8).");
                Console.WriteLine("6. Реализовать метод который получает число X и возвращает все cовершенные числа в диапазоне 0..X (X=10 > 6).");
                Console.WriteLine("7. Дан одномерный массив получить сумму всех элементов расположенных в четных и отдельно в нечетных позициях.");
                Console.WriteLine("8. Дан одномерный массив отсортировать отрицательные элементы. ([4, -1, 1, -2] > [4, -2, 1, -1])");
                Console.WriteLine("9. Дан массив преобразовать его, так чтобы последний поменялась с первой предпоследний с вторым и т.д.");
                Console.WriteLine("10. Дан одномерный массив, получить сумму элементов которые: a) больше числа M b) Меньше числа N");
                Console.WriteLine("11. Дан двумерный массив, поменять местами четные строки с нечетными.");
                Console.WriteLine("12. Дан двумерный массив получить максимальные элемента каждого столбца.");
                Console.WriteLine("13. Дан двумерный массив поменять местами элементы, расположенные на главной диагонали с противоположной диагонально.");
                Console.WriteLine("14. Дана строка, посчитать количество символов A");
                Console.WriteLine("15. Дана строка, посчитать количество слов.");
                Console.WriteLine("16. Дана строка, перевернуть ее (компьютер > ретюьпмок).");
                Console.WriteLine("17. Дана строка, поменять все символы C на E.");
                Console.WriteLine("18. Дана строка, посчитать количество слов, которые начинаются на букву K.");
                Console.WriteLine("\n0. Выход.");

                while(!int.TryParse(Console.ReadLine(), out selectMenu) || selectMenu < 0 || selectMenu > 18)
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");

                switch (selectMenu)
                {
                    case 1:
                        {
                            Console.Clear();
                            SubTask1();
                            Console.Read();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            SubTask2();
                            Console.Read();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            SubTask3();
                            Console.Read();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            SubTask4();
                            Console.Read();
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            SubMenu(selectMenu);
                            //Console.WriteLine($"Результат: {SubTask5()} ");
                            Console.Read();
                            break;
                        }
                    case 6:
                        {
                            Console.Clear();
                            SubMenu(selectMenu);
                            //Console.WriteLine($"Результат: {SubTask6()} ");
                            Console.Read();
                            break;
                        }
                    case 7:
                        {
                            Console.Clear();
                            SubTask7();
                            Console.Read();
                            break;
                        }
                    case 8:
                        {
                            Console.Clear();
                            SubTask8();
                            Console.Read();
                            break;
                        }
                    case 9:
                        {
                            Console.Clear();
                            SubTask9();
                            Console.Read();
                            break;
                        }
                    case 10:
                        {
                            Console.Clear();
                            SubTask10();
                            Console.Read();
                            break;
                        }
                    case 11:
                        {
                            Console.Clear();
                            SubTask11();
                            Console.Read();
                            break;
                        }
                    case 12:
                        {
                            Console.Clear();
                            SubTask12();
                            Console.Read();
                            break;
                        }
                    case 13:
                        {
                            Console.Clear();
                            SubTask13();
                            Console.Read();
                            break;
                        }
                    case 14:
                        {
                            Console.Clear();
                            SubTask14();
                            Console.Read();
                            break;
                        }
                    case 15:
                        {
                            Console.Clear();
                            SubTask15();
                            Console.Read();
                            break;
                        }
                    case 16:
                        {
                            Console.Clear();
                            SubTask16();
                            Console.Read();
                            break;
                        }
                    case 17:
                        {
                            Console.Clear();
                            SubTask17();
                            Console.Read();
                            break;
                        }
                    case 18:
                        {
                            Console.Clear();
                            SubTask18();
                            Console.Read();
                            break;
                        }
                    case 0:
                        {
                            isExit = true;
                            break;
                        }
                }
            } 
        }
    }
}
