using System;

namespace ConsoleApp1
{
    /// <summary>
    /// Класс арифметических операций
    /// </summary>
    public class ArithmeticOperation
    {
        /// <summary>
        /// Счётчик всех правильных/неправильных ответов
        /// </summary>
        public static int Count;
        /// <summary>
        /// Счётчик всех сумм
        /// </summary>
        public static int CountSummation;
        /// <summary>
        /// Счётчик всех разностей
        /// </summary>
        public static int CountSubstraction;
        /// <summary>
        /// Счётчик всех операций умножения
        /// </summary>
        public static int CountMultiplication;
        /// <summary>
        /// Счётчки всех операций деления
        /// </summary>
        public static int CountDivision;
        /// <summary>
        /// Счётчик всех корректных операций
        /// </summary>
        public static int CorrectAnswers { get; set; }
        /// <summary>
        /// Счётчик корректных операций суммы
        /// </summary>
        public static int CorrectSummation;
        /// <summary>
        /// Счётчик корректных операций умножения
        /// </summary>
        public static int CorrectMultiplication;
        /// <summary>
        /// Счётчик корректных операций деления
        /// </summary>
        public static int CorrectDivision;
        /// <summary>
        /// Счётчик корректных операций разницы
        /// </summary>
        public static int CorrectSubstraction;
        /// <summary>
        /// Счётчик процента ответов
        /// </summary>
        public static int Percent;
        /// <summary>
        /// РЕзультат операций
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// Первое число
        /// </summary>
        public int FirstNumber { get; set; }
        /// <summary>
        /// Второе число
        /// </summary>
        public int SecondNumber { get; set; }

        public ArithmeticOperation()
        {
            
        }
        
        /// <summary>
        /// Коструктор класса
        /// </summary>
        /// <param name="levelDifficulty">Сложность</param>
        public ArithmeticOperation(int levelDifficulty)
        {
            FirstNumber = new Random().Next(1, levelDifficulty);
            SecondNumber = new Random().Next(1, levelDifficulty);
            Count++;
        }

        /// <summary>
        /// Метод для проверки корректности значения
        /// </summary>
        /// <param name="isCorrect"></param>
        /// <returns></returns>
        public virtual int Checked(bool isCorrect)
        {
            return 0;
        }

    }
}