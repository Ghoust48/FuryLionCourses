// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

public sealed class Substraction : ArithmeticOperation
{
    protected override int Result => A - B;
    protected override char SignRepresentation => '-';
    public override OperationType OperationType => OperationType.Substraction;

    public Substraction(Difficulty difficulty) : base(difficulty)
    {
        GenerateInputData();
    }
}