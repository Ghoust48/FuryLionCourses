// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using System.Collections;
using UnityEngine;

public class MysteryController : MonoBehaviour
{
    [SerializeField] private GameObject _scoreObject;
    private Score _score;
    
    private AudioSource _audio;
    private Vector2 _positionSpawn;
    
    private int _point;    
    
    private void Awake()
    {
        _positionSpawn = new Vector2(9.5f, 3.5f);
        _point = Random.Range(50, 250);
        _audio = GetComponent<AudioSource>();
        _score = _scoreObject.GetComponent<Score>();
    }

    private void Start()
    {
        StartCoroutine(OnSpawn());
    }

    private void Update()
    {
        if (transform.position.x > -9.5f)
            transform.position += Vector3.left * 0.05f;
        else
            _audio.Pause();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            collision.gameObject.SetActive(false);
            OnDestroyMystery();
            _score.AddPoint(_point);
        } 
    }
    
    private IEnumerator OnSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
                    
            _audio.Play();
            _point = Random.Range(50, 250);
            transform.position = _positionSpawn;
        }
    }

    private void OnDestroyMystery()
    {
        var transformPosition = transform.position;
        
        _audio.Pause();

        transformPosition.z = -15;
        transform.position = transformPosition;
    }
}
