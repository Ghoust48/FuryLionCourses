// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;  
    [SerializeField] private AudioClip _shot;
    
    private AudioSource _audio;
    
    private Lives _lives;
    public static int CountLives = 3;
    
    private Vector2 _position;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _lives = GetComponent<Lives>();
        _position = transform.position;
    }
    
    private void OnRespawn()
    {
        _position.x = -6.5f;
        transform.position = _position;
        gameObject.SetActive(true);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            
            if (CountLives <= 3 && CountLives > 1)
            {
                CountLives--;
                _lives.Destroy();
                OnRespawn();
            }
            else
            {
                Score.ResetScore();
                CountLives = 3;
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    
    private void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            PlayerFire();
        }

        _position.x += Input.GetAxis("Horizontal") * _speed;

        _position.x = Mathf.Clamp(_position.x, -7.8f, 7.8f);
        
        transform.position = _position;
    }

    private void PlayerFire()
    {
        var obj = Pool.Current.GetPooledObject((int)PooledObjects.PlayerBullet);
        var playerPosition = transform.position;
		
        if(obj == null) 
            return;

        playerPosition.y += 0.7f;
        obj.transform.position = playerPosition;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
        
        _audio.PlayOneShot(_shot);
    }
}
