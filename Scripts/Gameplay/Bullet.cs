// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;

using UnityEngine;

using FuryLion.UI;

public class Bullet : Element, IReleasable
{
   [SerializeField] private Sprite _up;
   [SerializeField] private Sprite _down;
   
   [SerializeField] private float _speed;
   [SerializeField] private float _verticalBounds;
   
   public event Action Destroyed;
   
   private SpriteRenderer _spriteRenderer;
   private Rigidbody2D _rigidbody;

   private Direction _direction;

   private IDamageTaker _owner;
   
   private void Awake()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _rigidbody = GetComponent<Rigidbody2D>();
   }

   public void Shoot(Direction direction, IDamageTaker owner)
   {
      _owner = owner;
      
      _direction = direction;

      _spriteRenderer.sprite = direction == Direction.Down ? _down : _up;
   }

   private void FixedUpdate()
   {
      var positionY = _rigidbody.position.y + _speed * (int)_direction * Time.fixedDeltaTime;
      
      _rigidbody.MovePosition(new Vector2(_rigidbody.position.x, positionY));

      if (Mathf.Abs(positionY) > _verticalBounds)
         Destroy();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      var damageTaker = other.GetComponent<IDamageTaker>();
      if (damageTaker == null)
         return;
      
      if (damageTaker == _owner)
         return;
      
      if (_owner is Enemy && damageTaker is Enemy)
         return;
      
      damageTaker.TakeDamage();
      Destroy();
   }

   private void OnCollisionEnter2D(Collision2D other)
   { 
      Destroy();
   }

   private void Destroy()
   {
      Destroyed?.Invoke();
      Recycler.Release(this);
   }

   public void OnReleased()
   {
      Destroyed = null;
   }
}