// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;

public class OperationsQueue : Queue<ArithmeticOperation>
{
    private const byte MinCount = 4;
    private const byte MaxCount = 8;

    public static event Action Completed;

    private readonly byte _totalCount;

    private byte _correctCount;
    private byte _incorrectCount;

    public OperationsQueue()
    {
        _totalCount = (byte) new Random(Guid.NewGuid().GetHashCode()).Next(MinCount, MaxCount + 1);
        for (var i = 0; i < _totalCount; i++)
            Enqueue(GetRandomOperation(GetRandomDifficulty()));

        Subscribe();
    }

    public OperationsQueue(Difficulty difficulty)
    {
        _totalCount = (byte) new Random(Guid.NewGuid().GetHashCode()).Next(MinCount, MaxCount + 1);
        for (var i = 0; i < _totalCount; i++)
            Enqueue(GetRandomOperation(difficulty));

        Subscribe();
    }

    private void Subscribe()
    {
        ArithmeticOperation.Succeeded += OnOperationSucceeded;
        ArithmeticOperation.Failed += OnOperationFailed;
    }

    public void Run()
    {
        byte currentOperationNumber = 0;
        
        while (Count > 0)
        {
            Dequeue().Run(currentOperationNumber, _totalCount);
            currentOperationNumber++;
        }

        OnCompleted();
    }

    private void OnOperationSucceeded(ArithmeticOperation _)
    {
        _correctCount++;
    }
    
    private void OnOperationFailed(ArithmeticOperation _)
    {
        _incorrectCount++;
    }

    private void OnCompleted()
    {
        Completed?.Invoke();
        
        StatisticsManager.ShowDialog(_correctCount, _incorrectCount);
    }

    private static ArithmeticOperation GetRandomOperation(Difficulty difficulty)
    {
        switch (GetRandomOperationType())
        {
            case OperationType.Summation:
                return new Summation(difficulty);
            case OperationType.Substraction:
                return new Substraction(difficulty);
            case OperationType.Multiplication:
                return new Multiplication(difficulty);
            case OperationType.Division:
                return new Division(difficulty);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static OperationType GetRandomOperationType()
    {
        return (OperationType) new Random(Guid.NewGuid().GetHashCode()).Next(0,
            Enum.GetValues(typeof(OperationType)).Length);
    }

    public static Difficulty GetRandomDifficulty()
    {
        return (Difficulty) new Random(Guid.NewGuid().GetHashCode()).Next(0, Enum.GetValues(typeof(Difficulty)).Length);
    }
}