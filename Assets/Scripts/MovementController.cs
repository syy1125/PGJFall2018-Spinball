using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public float InputStrength;
	public string HorizontalAxisName;
	public string VerticalAxisName;
	public float maxSpinsPerSecond;

	private Rigidbody2D _rigidbody2D;
	private GameObject sprite;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		sprite = transform.GetChild(0).gameObject;
	}

	void Update()
	{
		sprite.transform.Rotate(0, 0, Mathf.Min(_rigidbody2D.velocity.magnitude * 0.5f, maxSpinsPerSecond) * Time.deltaTime * 360);
	}

	void FixedUpdate()
	{
		float horiz = Input.GetAxis(HorizontalAxisName);
		float vert = Input.GetAxis(VerticalAxisName);
		Vector2 movementForce = new Vector2(horiz, vert).normalized * InputStrength;
		float degrees = Vector2.Angle(_rigidbody2D.velocity, movementForce);
		if (150 > degrees)
		{
			_rigidbody2D.AddForce(movementForce);
		}
		else
		{
			_rigidbody2D.AddForce(movementForce * 0.6f);
		}
	}
}