namespace ConsoleApp1
{
    /// <summary>
    /// Класс арифметичкеской операции разности
    /// </summary>
    public class Substraction : ArithmeticOperation
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="levelDifficulty">Уровень сложности</param>
        public Substraction(int levelDifficulty) : base(levelDifficulty)
        {
            Result = FirstNumber - SecondNumber;
            CountSubstraction++;
        }

        public override string ToString()
        {
            return $"{FirstNumber} - {SecondNumber} = ";
        }

        /// <summary>
        /// Проверка на корректность
        /// </summary>
        /// <param name="isCorrect"></param>
        /// <returns></returns>
        public override int Checked(bool isCorrect)
        {
            return isCorrect == true ? CorrectSubstraction++ : CorrectSubstraction;
        }
    }
}