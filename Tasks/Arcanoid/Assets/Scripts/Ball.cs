
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
	private int _angle;
	private bool _isActive;
	public static int CountHeart;
	private Heart _heart;
	private Vector2 _position;
	private Rigidbody2D _rigidbody2D;
	private Block _block;
	public GameObject platform;

	private void Awake ()
	{
		_angle = 250;
		CountHeart = 3;
		_heart = FindObjectOfType<Heart>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_position = transform.position;
		_isActive = false;
	}
	
	private void Update ()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (!_isActive)
			{
				if (Random.Range(0, 2) == 0)
					_angle *= -1;
				
				_rigidbody2D.WakeUp();
				_rigidbody2D.AddForce(new Vector2(_angle, 150));
				_isActive = true;
			}
		}
		
		if (!_isActive && platform != null)
		{
			_rigidbody2D.Sleep();
			_position.x = platform.transform.position.x;
			transform.position = _position;
		}

		if (transform.position.y <= -5)
		{
			_isActive = false;
			_position.x = platform.transform.position.x;
			_position.y = platform.transform.position.y + 0.35f;
			transform.position = _position;
			
			CountHeart--;
			
			if(CountHeart < 1)
				SceneManager.LoadScene("GameOver");
			
			_heart.DestroyHeart();
			
			_rigidbody2D.Sleep();
		}

		if (FindObjectOfType<Block>() == null)
		{
			SceneManager.LoadScene("Win");
		}
	}
}
