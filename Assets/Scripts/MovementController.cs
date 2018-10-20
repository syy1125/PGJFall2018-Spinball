using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public float InputStrength;
	public string HorizontalAxisName;
	public string VerticalAxisName;

	public float maxSpinsPerSecond;

	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		transform.Rotate(0, 0, Mathf.Min(_rigidbody2D.velocity.magnitude * 0.5f, maxSpinsPerSecond) * Time.deltaTime * 360);
	}

	void FixedUpdate()
	{
		float horiz = Input.GetAxis(HorizontalAxisName) * InputStrength;
		float vert = Input.GetAxis(VerticalAxisName) * InputStrength;
		Vector2 movementForce = new Vector2(horiz, vert);
		float degrees =Vector2.Angle(_rigidbody2D.velocity, movementForce);
		if( 150 > degrees )
		{
			_rigidbody2D.AddForce(movementForce);
		}
		else
		{
			_rigidbody2D.AddForce(movementForce * 0.6f);
		}
	}
}