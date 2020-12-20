// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;

public static class ListExtensions
{
    public static T Random<T>(this List<T> list)
    {
        return list[new Random(Guid.NewGuid().GetHashCode()).Next(0, list.Count)];
    }
}