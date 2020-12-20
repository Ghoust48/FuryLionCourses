// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class Lives : MonoBehaviour 
{
    [SerializeField] private Transform[] _lives;

    public void Destroy()
    { 
        _lives[PlayerController.CountLives].gameObject.SetActive(false);
    }
}
