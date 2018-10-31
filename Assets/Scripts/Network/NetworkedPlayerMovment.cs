using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class NetworkedPlayerMovment : NetworkBehaviour
{
	[FormerlySerializedAs("InputStrength")]
	public float BaseInputStrength;

	public string HorizontalAxisName;
	public string VerticalAxisName;
	public QuantumGyroBlade QGB;

	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if(!isLocalPlayer)
		{
			return;
		}

		float horiz = Input.GetAxisRaw(HorizontalAxisName);
		float vert = Input.GetAxisRaw(VerticalAxisName);
		
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