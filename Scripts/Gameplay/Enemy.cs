// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;

using FuryLion.UI;

using UnityEngine;

public class Enemy : Element, IDamageTaker, IRecyclable
{
    public static event Action<Enemy> Destroyed;

    private bool _isLive;
    
    public int DamageScore;

    private bool _canShoot = true;

    private SpriteRenderer _spriteRenderer;

    public Bounds Bounds => _spriteRenderer.bounds;

    public bool IsLive => _isLive;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Transform parent, Vector3 localPosition)
    {
        _isLive = true;
        
        transform.parent = parent;
        transform.localPosition = localPosition;
    }

    private void Shoot()
    {
        _canShoot = false;
        
        var bullet = Recycler.Get<Bullet>();
        bullet.Destroyed += BulletOnDestroyed;
        bullet.transform.position = transform.position;
        bullet.Shoot(Direction.Down, this);
    }
    
    private void BulletOnDestroyed()
    {
        _canShoot = true;
    }
    
    void IDamageTaker.TakeDamage()
    {
        Destroyed?.Invoke(this);
        _isLive = false;
        Recycler.Release(this);        
    }
}