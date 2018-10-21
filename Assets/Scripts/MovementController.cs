using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
	[FormerlySerializedAs("InputStrength")]
	public float BaseInputStrength;

	public string HorizontalAxisName;
	public string VerticalAxisName;
	public float MaxSpinsPerSecond;
	public QuantumGyroBlade QGB;
	public Sprite QGBSprite;

	private Rigidbody2D _rigidbody2D;
	private GameObject _sprite;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_sprite = transform.GetChild(0).gameObject;
		_sprite.GetComponent<SpriteRenderer>().sprite = QGBSprite;
	}

	void Update()
	{
		_sprite.transform.Rotate(
			0,
			0,
			Mathf.Min(_rigidbody2D.velocity.magnitude * 0.5f, MaxSpinsPerSecond) * Time.deltaTime * 360
		);
	}

	void FixedUpdate()
	{
		float horiz = Input.GetAxis(HorizontalAxisName);
		float vert = Input.GetAxis(VerticalAxisName);
		Vector2 movementForce = new Vector2(horiz, vert).normalized * BaseInputStrength * QGB.Acceleration;
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