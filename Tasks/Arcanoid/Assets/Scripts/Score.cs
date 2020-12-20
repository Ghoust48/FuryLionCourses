
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
    [SerializeField]private Text _scoreText;
    private static int _point = 0;

    private void Awake()
    {
        _scoreText.text = _point.ToString();
    }

    public void AddPoint(int value)
    {
        _point += value;
        _scoreText.text = _point.ToString();
    }
}
