using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Dictionary<string, ArithmeticOperation> _operations;
        private static ArithmeticOperation _arithmeticOperation;
        static void Main(string[] args)
        {
            Storage.Load<ArithmeticOperation>("data.bin");
            int selectedMenu;
            
            var rand = new Random();
            var queue = new ArithmeticQueue<ArithmeticOperation>();

            do
            {
                Console.Clear();
                Console.WriteLine("1. Выбрать уровень сложности\n" +
                                  "2. Выбрать уровень сложности рандомно\n" +
                                  "0. Выход\n");
                
                while (!ReadInt(out selectedMenu) || selectedMenu < 0 || selectedMenu > 2)
                    Console.Write("Ошибка ввода. Попробуйте еще раз: ");
                
                switch (selectedMenu)
                {
                    case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("1. Низкий\n" +
                                          "2. Средний\n" +
                                          "3. Высокий\n");
                
                        while (!ReadInt(out selectedMenu) || selectedMenu < 0 || selectedMenu > 3)
                            Console.Write("Ошибка ввода. Попробуйте еще раз: ");

                        switch (selectedMenu)
                        {
                            case 1:
                            {
                                Console.Clear();
                                ReadQueue(queue, 10);
                                PrintState(10);
                                break;
                            }
                            case 2:
                            {
                                Console.Clear();
                                ReadQueue(queue, 100);
                                PrintState(100);
                                break;
                            }
                            case 3:
                            {
                                Console.Clear();
                                ReadQueue(queue, 10000);
                                PrintState(10000);
                                break;
                            }
                        }

                        break;
                    }
                    case 2:
                    {
                        var result = rand.Next(1, 3);
                        
                        switch (result)
                        {
                            case 1:
                            {
                                Console.Clear();
                                ReadQueue(queue, 10);
                                PrintState(10);
                                break;
                            }
                            case 2:
                            {
                                Console.Clear();
                                ReadQueue(queue, 100);
                                PrintState(100);
                                break;
                            }
                            case 3:
                            {
                                Console.Clear();
                                ReadQueue(queue, 10000);
                                PrintState(10000);
                                break;
                            }
                        }
                        
                        break;
                    }
                }
                
            } while (selectedMenu != 0);
        }

        private static void ReadQueue(ArithmeticQueue<ArithmeticOperation> queue, int levelDifficulty)
        {
            var rand = new Random();
            int count = rand.Next(4, 8);
                                
            for (int i = 0; i < count; i++) 
            { 
                switch (rand.Next(4)) 
                { 
                    case 0: 
                    { 
                        queue.Enqueue(new Summation(levelDifficulty));
                        break; 
                    } 
                    case 1: 
                    { 
                        queue.Enqueue(new Substraction(levelDifficulty)); 
                        break; 
                    } 
                    case 2: 
                    { 
                        queue.Enqueue(new Multiplication(levelDifficulty)); 
                        break; 
                    } 
                    case 3: 
                    { 
                        queue.Enqueue(new Division(levelDifficulty)); 
                        break; 
                    } 
                }
            }
            
            for (int i = 0; i < count; i++)
            {
                if (queue.Start() == queue.Peek().Result)
                {
                    ArithmeticOperation.CorrectAnswers++;
                    queue.Peek().Checked(true);
                    queue.Dequeue();
                }
                else
                {
                    queue.Dequeue();
                }
            }
        }

        private static void PrintState(int levelDifficulty)
        {
            Console.Clear();
            
            if(levelDifficulty == 10)
                Console.WriteLine("Низкий уровень");
            else if (levelDifficulty == 100)
                Console.WriteLine("Средний уровень");
            else
                Console.WriteLine("Высокий уровень");
            
            Console.WriteLine($"Всего ответов: {ArithmeticOperation.Count}\n" +
                              $"    правельных: {ArithmeticOperation.CorrectAnswers}\n" +
                              $"    неправленых: {(ArithmeticOperation.Count - ArithmeticOperation.CorrectAnswers)}");
            
            Console.WriteLine($"Всего по операции сумма: {ArithmeticOperation.CountSummation}\n" +
                              $"    правельных: {ArithmeticOperation.CorrectSummation}\n" +
                              $"    неправленых: {(ArithmeticOperation.CountSummation - ArithmeticOperation.CorrectSummation)}");
            
            Console.WriteLine($"Всего по операции разность: {ArithmeticOperation.CountSubstraction}\n" +
                              $"    правельных: {ArithmeticOperation.CorrectSubstraction}\n" +
                              $"    неправленых: {(ArithmeticOperation.CountSubstraction - ArithmeticOperation.CorrectSubstraction)}");
            
            Console.WriteLine($"Всего по операции умножение: {ArithmeticOperation.CountMultiplication}\n" +
                              $"    правельных: {ArithmeticOperation.CorrectMultiplication}\n" +
                              $"    неправленых: {(ArithmeticOperation.CountMultiplication - ArithmeticOperation.CorrectMultiplication)}");
            
            Console.WriteLine($"Всего по операции деление: {ArithmeticOperation.CountDivision}\n" +
                              $"    правельных: {ArithmeticOperation.CorrectDivision}\n" +
                              $"    неправленых: {(ArithmeticOperation.CountDivision - ArithmeticOperation.CorrectDivision)}");
            
            if (ArithmeticOperation.CorrectAnswers != 0 && (ArithmeticOperation.Count - ArithmeticOperation.CorrectAnswers) != 0)
                ArithmeticOperation.Percent = ArithmeticOperation.CorrectAnswers * 100 / (ArithmeticOperation.CorrectAnswers + 
                                                                      (ArithmeticOperation.Count - ArithmeticOperation.CorrectAnswers));
            Console.WriteLine($"Общая корректность: {ArithmeticOperation.Percent}");
            
            

            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
        
        private static bool ReadInt(out int value)
        {
            return int.TryParse(Console.ReadLine(), out value);
        }
    }
    
}