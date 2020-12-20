// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;

using FuryLion.UI;

using UnityEngine;

public class Player : MonoBehaviour, IDamageTaker
{
    [SerializeField] private float _speed;
    [SerializeField] private float _horizontalBounds;
    [SerializeField] private int _lives;

    public static event Action Destroyed;
    public static event Action<int> LivesChanged;
    
    private int _score;
    
    private Rigidbody2D _rigidbody;

    private float _moveDirection;

    private bool _canShoot = true;
    
    private void Start()
    {
        LivesChanged?.Invoke(_lives);
        
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveDirection = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
            Shoot();
    }

    private void Shoot()
    {
        _canShoot = false;
        
        var bullet = Recycler.Get<Bullet>();
        bullet.Destroyed += BulletOnDestroyed;
        bullet.transform.position = transform.position;
        bullet.Shoot(Direction.Up, this);
    }

    private void BulletOnDestroyed()
    {
        _canShoot = true;
    }

    private void FixedUpdate()
    {
        var positionX = _rigidbody.position.x + _speed * _moveDirection * Time.fixedDeltaTime;
        positionX = Mathf.Clamp(positionX, -_horizontalBounds, _horizontalBounds);
        _rigidbody.MovePosition(new Vector2(positionX, _rigidbody.position.y));
    }

    void IDamageTaker.TakeDamage()
    {
        if (_lives <= 0)
            return;
        
        _lives -= 1;

        LivesChanged?.Invoke(_lives);
        
        if (_lives <= 0)
            Destroyed?.Invoke();
    }
}