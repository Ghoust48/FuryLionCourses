// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;

public class Division : ArithmeticOperation
{
    protected override int Result => A / B;
    protected override char SignRepresentation => '/';
    public override OperationType OperationType => OperationType.Division;

    public Division(Difficulty difficulty) : base(difficulty)
    {
        GenerateInputData();
    }

    protected sealed override void GenerateInputData()
    {
        switch (Difficulty)
        {
            case Difficulty.Easy:
                B = Random.Next(1, 5);
                A = B * Random.Next(1, 5);
                break;
            case Difficulty.Medium:
                B = Random.Next(1, 100);
                A = B * Random.Next(1, 100);
                break;
            case Difficulty.Hard:
                B = Random.Next(1, (int) Math.Sqrt(int.MaxValue));
                A = B * Random.Next(1, int.MaxValue / B);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (Random.Next(0, 2) == 0)
            A *= -1;
        if (Random.Next(0, 2) == 0)
            B *= -1;
    }
}