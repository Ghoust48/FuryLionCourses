// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System;
using System.Collections.Generic;
using UnityEngine;

public enum PooledObjects
{
    PlayerBullet,
    EnemyBullet
}

[Serializable]
public class PoolObjects
{
    public string tagObject;
    public GameObject pooledObject;
}

public class Pool : MonoBehaviour
{
    [SerializeField] private int _pooledAmount;
    [SerializeField] private PoolObjects[] _pooledObject;
    
    private List<GameObject> _pool;
    public static Pool Current;

    private void Awake()
    {
        Current = GetComponent<Pool>();
        _pool = new List<GameObject>();
    }

    private void Start()
    {
        for (var i = 0; i < _pooledAmount; i++)
        {
            var obj = Instantiate(_pooledObject[i].pooledObject);
            obj.SetActive(false);
            _pool.Add(obj);
        }
    }

    public GameObject GetPooledObject(int value)
    {
        foreach (var obj in _pool)
        {
            if (!obj.activeInHierarchy && obj.CompareTag(_pooledObject[value].tagObject))
            {
                return obj;
            }
        }

        return null;
    }
}
