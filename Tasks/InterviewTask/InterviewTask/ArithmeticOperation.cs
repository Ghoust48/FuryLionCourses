// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;

public abstract class ArithmeticOperation
{
    public static event Action<ArithmeticOperation> Succeeded;
    public static event Action<ArithmeticOperation> Failed;
    
    protected static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());
    
    protected int A;
    protected int B;

    public Difficulty Difficulty { get; }

    protected abstract int Result { get; }
    protected abstract char SignRepresentation { get; }
    public abstract OperationType OperationType { get; }

    protected ArithmeticOperation(Difficulty difficulty)
    {
        Difficulty = difficulty;
    }

    public void Run(byte currentNumber, byte totalCount)
    {
        while (true)
        {
            Console.Clear();
        
            Console.WriteLine($"Задание {currentNumber + 1} из {totalCount}.");
            Console.WriteLine($"Какой результат выражения {A} {SignRepresentation} {B}?\n");

            if (int.TryParse(Console.ReadLine(), out var result))
            {
                ProceedResult(result);
                break;
            }

            Console.WriteLine("Некорректный ввод.\n" +
                              "На вход принимаются только целые числа в диапазоне -2,147,483,648..2,147,483,647\n" +
                              "Нажмите любую кнопку, чтобы попробовать еще раз..");
            Console.ReadKey();
        }
    }

    private void ProceedResult(int result)
    {
        if (result.Equals(Result))
            OnSucceeded();
        else
            OnFailed();
        
        Console.WriteLine("Нажмите любую кнопку для продолжения...");
        Console.ReadKey();
    }

    private void OnSucceeded()
    {
        Console.WriteLine(Messages.GetRandomCorrectMessage());
        Succeeded?.Invoke(this);
    }

    private void OnFailed()
    {
        Console.WriteLine(Messages.GetRandomIncorrectMessage());
        Failed?.Invoke(this);
    }

    protected virtual void GenerateInputData()
    {
        GenerateValueBorders(out var lowerBorder, out var higherBorder);
        A = Random.Next(lowerBorder, higherBorder);
        B = Random.Next(lowerBorder, higherBorder);
    }

    private void GenerateValueBorders(out int lowerBorder, out int higherBorder)
    {
        switch (Difficulty)
        {
            case Difficulty.Easy:
                lowerBorder = 0;
                higherBorder = 100;
                return;
            case Difficulty.Medium:
                lowerBorder = -10000;
                higherBorder = 10000;
                return;
            case Difficulty.Hard:
                lowerBorder = int.MinValue;
                higherBorder = int.MaxValue;
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(Difficulty), Difficulty, null);
        }
    }
}