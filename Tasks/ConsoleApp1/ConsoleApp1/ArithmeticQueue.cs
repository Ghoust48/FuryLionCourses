using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    /// <summary>
    /// Класс собственной очереди
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArithmeticQueue<T> : Queue<T>
    {
        /// <summary>
        /// Метод начала работы очереди
        /// </summary>
        /// <returns></returns>
        public int Start()
        {
            int result;
            Console.Write(base.Peek());
            while (!ReadInt(out result))
                Console.Write("Ошибка! Введите число: ");
            return result;
        }
        
        /// <summary>
        /// Проверка числа
        /// </summary>
        /// <param name="value">Число</param>
        /// <returns></returns>
        private static bool ReadInt(out int value)
        {
            return int.TryParse(Console.ReadLine(), out value);
        }
    }
}