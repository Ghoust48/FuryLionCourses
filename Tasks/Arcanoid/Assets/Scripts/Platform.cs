
using UnityEngine;

public class Platform : MonoBehaviour 
{
    [SerializeField]private float _speed;
    private Vector2 _position;

    private void Start ()
	{
		_position = transform.position;
	}
	
    private void Update ()
	{
		_position.x += Input.GetAxis("Horizontal") * _speed;
		transform.position = _position;
		
		if (_position.x < -1.95) 
			transform.position = new Vector2 (-1.95f, _position.y);
		if (_position.x > 1.95) 
			transform.position = new Vector2 (1.95f, _position.y);
		
	}
}
