namespace ConsoleApp1
{
    public class Multiplication : ArithmeticOperation
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="levelDifficulty"></param>
        public Multiplication(int levelDifficulty) : base(levelDifficulty)
        {
            Result = FirstNumber * SecondNumber;
            CountMultiplication++;
        }

        public override string ToString()
        {
            return $"{FirstNumber} * {SecondNumber} = ";
        }

        public override int Checked(bool isCorrect)
        {
            return isCorrect == true ? CorrectMultiplication++ : CorrectMultiplication;
        }
    }
}