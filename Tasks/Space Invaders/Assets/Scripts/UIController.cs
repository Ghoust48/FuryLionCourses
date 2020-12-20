// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _buttonPlay;

    public void Start()
    {
        _buttonPlay.onClick.AddListener(PlayPressed);
    }
    
    public void PlayPressed()
    {
        SceneManager.LoadScene("GameScene");
    }
}
