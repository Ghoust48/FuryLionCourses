// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float _speed;
	private Rigidbody2D _rigidbody2D;
	
	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		StartCoroutine(DestroyBullet());
	}
	
	private void FixedUpdate()
	{
		_rigidbody2D.position += Vector2.up * _speed;
	}

	private IEnumerator DestroyBullet()
	{
		yield return new WaitForSeconds(0.8f);
		
		gameObject.SetActive(false);
	}	
}
