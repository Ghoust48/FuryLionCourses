// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
    [SerializeField] private Text _scoreText;
    private static int _point;

    private void Awake()
    {
        _scoreText.text = _point.ToString();
    }

    public void AddPoint(int value)
    {
        _point += value;
        _scoreText.text = _point.ToString();
    }

    public static void ResetScore()
    {
        _point = 0;
    }
}
