// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;

internal static class Program
{
    public static void Main()
    {
        StatisticsManager.Init();
        
        ShowModeSelectionDialog();
    }

    private static void ShowModeSelectionDialog()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Пожалуйста, выберите действие:\n");

            Console.WriteLine("1. Просмотреть статистику.");
            Console.WriteLine("2. Получить новое задание.");
            Console.WriteLine("0. Выход.\n");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowStatisticsDialog();
                    break;
                case "2":
                    ShowTaskDifficultyDialog();
                    break;
                case "0":
                    Console.WriteLine("Спасибо за использование приложения!");
                    Environment.Exit(0);
                    return;
                default:
                    ShowUnknownSelectionMessage();
                    continue;
            }
        }
    }

    private static void ShowStatisticsDialog()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Пожалуйста, выберите параметр выборки статистики:\n");

            Console.WriteLine("1. За все время");
            Console.WriteLine("2. По сложности");
            Console.WriteLine("3. По типу операции");
            Console.WriteLine("0. Назад\n");

            switch (Console.ReadLine())
            {
                case "1":
                    StatisticsManager.ShowTotal();
                    ShowModeSelectionDialog();
                    return;
                case "2":
                    ShowStatisticsByDifficultyDialog();
                    ShowModeSelectionDialog();
                    return;
                case "3":
                    ShowStatisticsByTypeDialog();
                    ShowModeSelectionDialog();
                    return;
                case "0":
                    ShowModeSelectionDialog();
                    return;
                default:
                    ShowUnknownSelectionMessage();
                    continue;
            }
        }
    }

    private static void ShowStatisticsByDifficultyDialog()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Пожалуйста, выберите сложность:\n");

            Console.WriteLine("1. Легко");
            Console.WriteLine("2. Средне");
            Console.WriteLine("3. Сложно");
            Console.WriteLine("0. Назад\n");

            switch (Console.ReadLine())
            {
                case "1":
                    StatisticsManager.ShowByDifficulty(Difficulty.Easy);
                    ShowModeSelectionDialog();
                    return;
                case "2":
                    StatisticsManager.ShowByDifficulty(Difficulty.Medium);
                    ShowModeSelectionDialog();
                    return;
                case "3":
                    StatisticsManager.ShowByDifficulty(Difficulty.Hard);
                    ShowModeSelectionDialog();
                    return;
                case "0":
                    ShowStatisticsDialog();
                    return;
                default:
                    ShowUnknownSelectionMessage();
                    continue;
            }
        }
    }

    private static void ShowStatisticsByTypeDialog()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Пожалуйста, выберите тип операции:\n");

            Console.WriteLine("1. Суммирование");
            Console.WriteLine("2. Вычитание");
            Console.WriteLine("3. Умножение");
            Console.WriteLine("4. Деление");
            Console.WriteLine("0. Назад\n");

            switch (Console.ReadLine())
            {
                case "1":
                    StatisticsManager.ShowByOperation(OperationType.Summation);
                    ShowModeSelectionDialog();
                    return;
                case "2":
                    StatisticsManager.ShowByOperation(OperationType.Substraction);
                    ShowModeSelectionDialog();
                    return;
                case "3":
                    StatisticsManager.ShowByOperation(OperationType.Multiplication);
                    ShowModeSelectionDialog();
                    return;
                case "4":
                    StatisticsManager.ShowByOperation(OperationType.Division);
                    ShowModeSelectionDialog();
                    return;
                case "0":
                    ShowStatisticsDialog();
                    return;
                default:
                    ShowUnknownSelectionMessage();
                    continue;
            }
        }
    }

    private static void ShowTaskDifficultyDialog()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("Пожалуйста, выберите сложность задания:\n");

            Console.WriteLine("1. Легко");
            Console.WriteLine("2. Средне");
            Console.WriteLine("3. Сложно");
            Console.WriteLine("4. Случайно");
            Console.WriteLine("5. Перемешать");
            Console.WriteLine("0. Назад\n");

            switch (Console.ReadLine())
            {
                case "1":
                    new OperationsQueue(Difficulty.Easy).Run();
                    ShowModeSelectionDialog();
                    return;
                case "2":
                    new OperationsQueue(Difficulty.Medium).Run();
                    ShowModeSelectionDialog();
                    return;
                case "3":
                    new OperationsQueue(Difficulty.Hard).Run();
                    ShowModeSelectionDialog();
                    return;
                case "4":
                    new OperationsQueue(OperationsQueue.GetRandomDifficulty()).Run();
                    ShowModeSelectionDialog();
                    return;
                case "5":
                    new OperationsQueue().Run();
                    ShowModeSelectionDialog();
                    return;
                case "0":
                    ShowModeSelectionDialog();
                    return;
                default:
                    ShowUnknownSelectionMessage();
                    continue;
            }
        }
    }

    private static void ShowUnknownSelectionMessage()
    {
        Console.WriteLine("Неопознанная команда. Нажмите любую кнопку, чтобы попробовать еще раз...");
        Console.ReadKey();
    }
}