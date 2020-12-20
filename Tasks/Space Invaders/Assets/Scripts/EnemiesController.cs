// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private AudioClip _shot;

    private const float minBorder = -8.3f;
    private const float maxBorder = 8.3f;
    private float _speed = 0.3f;
    private Transform _enemies;
    
    private AudioSource _audio;
   
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _enemies = GetComponent<Transform>();
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            _enemies.position += Vector3.right * _speed;
        
            foreach (Transform enemy in _enemies)
            {
                if (enemy.position.x >= maxBorder || enemy.position.x <= minBorder)
                {
                    var enemyPosition = enemy.position;
                
                    _speed = -_speed;
                    enemyPosition.x = Mathf.Clamp(enemyPosition.x, minBorder, maxBorder);                   
                    _enemies.position += Vector3.down * 0.5f;
                    break;
                }
            
                if (enemy.gameObject.activeInHierarchy)
                {
                    if (Random.value > 0.7f)
                    {
                        StartCoroutine(Fire(enemy));
                    }
                }
            }
        
            if (_enemies.position.y <= -4)
            {
                Score.ResetScore();
                PlayerController.CountLives = 3;
                SceneManager.LoadScene("GameOver");
            }

            if (ShowNotActiveEnemies() == _enemies.childCount)
            {
                SceneManager.LoadScene("GameScene");
            }

            yield return new WaitForSeconds(0.5f);   
        }
    }

    private IEnumerator Fire(Transform enemy)
    {
        var obj = Pool.Current.GetPooledObject((int)PooledObjects.EnemyBullet);
        var enemyPosition = enemy.position;
		
        if(obj == null) 
            yield break;

        enemyPosition.y -= 0.5f;
        obj.transform.position = enemyPosition;
        obj.transform.rotation = enemy.rotation;
        obj.SetActive(true);
        
        _audio.PlayOneShot(_shot);

        yield return null;
    }

    private int ShowNotActiveEnemies()
    {
        var notActiveEnemies = 0;
        
        for (var i = 0; i < _enemies.childCount; i++)
        {
            if (!_enemies.GetChild(i).gameObject.activeInHierarchy)
                notActiveEnemies++;
        }
        
        return notActiveEnemies;
    }
}
