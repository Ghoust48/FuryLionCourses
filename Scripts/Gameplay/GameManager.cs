// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System.Collections;

using FuryLion.UI;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _livesText;

    [SerializeField] private Text _gameOverText;

    private int _score;

    private void Awake()
    {
        Player.LivesChanged += PlayerOnLivesChanged;
        Player.Destroyed += PlayerOnDestroyed;
        Enemy.Destroyed += EnemyOnDestroyed;
        
        _gameOverText.gameObject.SetActive(false);
    }

    private void PlayerOnDestroyed()
    {
        _gameOverText.gameObject.SetActive(true);
    }

    private IEnumerator Start()
    {
        yield return Recycler.PrePool(null);
    }

    private void PlayerOnLivesChanged(int lives)
    {
        _livesText.Value = $"Lives: {lives}";
    }

    private void EnemyOnDestroyed(Enemy enemy)
    {
        _score += enemy.DamageScore;

        _scoreText.Value = $"Score: {_score}";
    }
}