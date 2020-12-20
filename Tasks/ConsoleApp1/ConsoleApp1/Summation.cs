using System;

namespace ConsoleApp1
{
    public class Summation : ArithmeticOperation
    {
        
        public Summation(int levelDifficulty) : base(levelDifficulty)
        {
            Result = FirstNumber + SecondNumber;
            CountSummation++;
        }

        public override int Checked(bool isCorrect)
        {
            return isCorrect == true ? CorrectSummation++ : CorrectSummation;
        }
        
        public override string ToString()
        {
            return $"{FirstNumber} + {SecondNumber} = ";
        }
    }
}