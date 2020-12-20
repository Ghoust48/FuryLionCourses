// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System.Collections;
using System.Collections.Generic;

using FuryLion.UI;

using UnityEngine;

public class Swarm : MonoBehaviour
{
    [SerializeField] private float _step;
    
    private readonly List<Enemy> _enemies = new List<Enemy>();

    private int _rows = 5;
    private int _column = 11;
    
    private Bounds _bounds;

    private Direction _direction = Direction.Right;

    private void Awake()
    {
        Enemy.Destroyed += EnemyOnDestroyed;

        Rebuild();
    }

    private IEnumerator Start()
    {
        yield return Init();

        while (true)
        {
            yield return Move();
            
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    private IEnumerator Init()
    {
        var startPosition = new Vector2(-4.0f, 0f);
        var currentPosition = startPosition;

        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _column; j++)
            {
                var enemy = Recycler.Get<Enemy>();
                
                _enemies.Add(enemy);
                enemy.Init(transform, currentPosition);
                
                currentPosition.x += 0.8f;
                
                yield return new WaitForSeconds(0.05f);
            }

            currentPosition.x = startPosition.x;
            currentPosition.y += 0.8f;
        }
        
        Rebuild();
    }

    private IEnumerator Move()
    {
        var offset =  new Vector3(_step * (int) _direction, 0.0f);

        for (var i = 0; i < _enemies.Count; i++)
        {
            var enemy = _enemies[i];
            enemy.transform.localPosition += offset;
            
            if (i % 11 == 0)
                yield return new WaitForSeconds(0.02f);
        }
        
        if (_bounds.min.x + offset.x < -6 || _bounds.max.x + offset.x > 6)
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                enemy.transform.localPosition += new Vector3(0, -_step);

                yield return new WaitForSeconds(0.02f);
            }
            
            _direction = _direction == Direction.Left ? Direction.Right : Direction.Left;
            
            _bounds.center += new Vector3(0.0f, -_step);
        }
        else
            _bounds.center += offset;
        

        yield return null;
    }
    
    private void EnemyOnDestroyed(Enemy enemy)
    {
        Rebuild();
    }

    private void Rebuild()
    {
        var firstFound = false;
        
        foreach (var enemy in _enemies)
        {
            if (!firstFound && enemy.IsLive)
            {
                _bounds = enemy.Bounds;
                firstFound = true;
            }
            
            if (enemy.IsLive)
                _bounds.Encapsulate(enemy.Bounds);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
}