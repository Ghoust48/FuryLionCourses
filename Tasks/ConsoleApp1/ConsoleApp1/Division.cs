using System;

namespace ConsoleApp1
{
    public class Division : ArithmeticOperation
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="levelDifficulty"></param>
        public Division(int levelDifficulty) : base(levelDifficulty)
        {
            CountDivision++;  
            Result = FirstNumber / SecondNumber;
            FirstNumber = Result * SecondNumber;
        }

        public override string ToString()
        {
            return $"{FirstNumber} / {SecondNumber} = ";
        }

        public override int Checked(bool isCorrect)
        {
            return isCorrect == true ? CorrectDivision++ : CorrectDivision;
        }
    }
}