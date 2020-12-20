// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _point;
    [SerializeField] private GameObject _scoreObject;
    
    private Score _score;

    private void Start()
    {
        _score = _scoreObject.GetComponent<Score>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _score.AddPoint(_point);
        }
    }
}
