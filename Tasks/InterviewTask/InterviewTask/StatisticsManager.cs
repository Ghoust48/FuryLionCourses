// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;

public static class StatisticsManager
{
    private const string FilePath = "data.bin";

    private static readonly Dictionary<Difficulty, Dictionary<OperationType, (int correct, int incorrect)>> Data;

    public static void Init()
    {
        ArithmeticOperation.Succeeded += OnOperationSucceeded;
        ArithmeticOperation.Failed += OnOperationFailed;
        OperationsQueue.Completed += OnQueueCompleted;
    }

    static StatisticsManager()
    {
        Data = Storage.Load<Dictionary<Difficulty, Dictionary<OperationType, (int, int)>>>(FilePath);

        foreach (var difficulty in (Difficulty[]) Enum.GetValues(typeof(Difficulty)))
        {
            if (!Data.ContainsKey(difficulty))
                Data.Add(difficulty, new Dictionary<OperationType, (int, int)>());

            foreach (var operation in (OperationType[]) Enum.GetValues(typeof(OperationType)))
                if (!Data[difficulty].ContainsKey(operation))
                    Data[difficulty].Add(operation, (0, 0));
        }
    }

    public static void ShowDialog(int correctCount, int incorrectCount)
    {
        Console.Clear();

        Console.WriteLine("Результаты:\n");

        var totalCount = correctCount + incorrectCount;
        if (totalCount == 0)
            Console.WriteLine("Недостаточно данных для предоставления статистики.");
        else
        {
            Console.WriteLine($"Правильно: {correctCount}");
            Console.WriteLine($"Неправильно: {incorrectCount}");
            Console.WriteLine($"Корректность: {correctCount * 100 / (correctCount + incorrectCount)}%");
        }

        Console.WriteLine("Нажмите любую кнопку для продолжения...");
        Console.ReadKey();
    }

    public static void ShowTotal()
    {
        var correctCount = 0;
        var incorrectCount = 0;

        foreach (var difficultyData in Data.Values)
        {
            foreach (var operationData in difficultyData.Values)
            {
                correctCount += operationData.correct;
                incorrectCount += operationData.incorrect;
            }
        }

        ShowDialog(correctCount, incorrectCount);
    }

    public static void ShowByDifficulty(Difficulty difficulty)
    {
        var correctCount = 0;
        var incorrectCount = 0;

        foreach (var operationData in Data[difficulty].Values)
        {
            correctCount += operationData.correct;
            incorrectCount += operationData.incorrect;
        }

        ShowDialog(correctCount, incorrectCount);
    }

    public static void ShowByOperation(OperationType operationType)
    {
        var correctCount = 0;
        var incorrectCount = 0;

        foreach (var difficultyData in Data.Values)
        {
            correctCount += difficultyData[operationType].correct;
            incorrectCount += difficultyData[operationType].incorrect;
        }

        ShowDialog(correctCount, incorrectCount);
    }

    private static void OnOperationSucceeded(ArithmeticOperation operation)
    {
        var difficulty = operation.Difficulty;
        var operationType = operation.OperationType;
        var oldValue = Data[difficulty][operationType];
        Data[difficulty][operationType] = (oldValue.correct + 1, oldValue.incorrect);
    }

    private static void OnOperationFailed(ArithmeticOperation operation)
    {
        var difficulty = operation.Difficulty;
        var operationType = operation.OperationType;
        var oldValue = Data[difficulty][operationType];
        Data[difficulty][operationType] = (oldValue.correct, oldValue.incorrect + 1);
    }

    private static void OnQueueCompleted()
    {
        Storage.Save(FilePath, Data);
    }
}