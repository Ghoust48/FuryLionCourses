// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System.Collections.Generic;

public static class Messages
{
    private static readonly List<string> CorrectMessages = new List<string>
    {
        "Правильно!",
        "Отлично!",
        "Верно!",
        "Совершенно верно!",
        "Так держать!",
        "Замечательно!",
        "Великолепно!",
    };
    
    private static readonly List<string> IncorrectMessages = new List<string>
    {
        "К сожалению.",
        "Не верно.",
        "Не совсем так.",
        "В следующий раз получится.",
        "Стоит больше думать.",
        "Калькулятор в помощь.",
        "Не надо так.",
    };
    
    public static string GetRandomCorrectMessage()
    {
        return CorrectMessages.Random();
    }

    public static string GetRandomIncorrectMessage()
    {
        return IncorrectMessages.Random();
    }
}