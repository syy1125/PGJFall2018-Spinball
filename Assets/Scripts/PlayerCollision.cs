﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollision : MonoBehaviour
{
	[FormerlySerializedAs("collisionDamping")]
	public float CollisionDamping;

	private Rigidbody2D _rigidbody2D;
	private ParticleSystem _particleSystem;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_particleSystem = GetComponent<ParticleSystem>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
		
		if (rb != null && other.CompareTag("Player"))
		{
			QuantumGyroBlade selfQGB = GetComponent<MovementController>().QGB;
			QuantumGyroBlade opponentQGB = other.GetComponent<MovementController>().QGB;
			Feedback(other, col.contacts[0].point, _rigidbody2D.velocity.magnitude + rb.velocity.magnitude);
			float mag = _rigidbody2D.velocity.magnitude;
			_rigidbody2D.velocity = (transform.position - other.transform.position)
			                        * rb.velocity.magnitude
			                        * CollisionDamping
			                        * opponentQGB.Power
			                        / selfQGB.Resistance;
			rb.velocity = (other.transform.position - transform.position)
			              * mag
			              * CollisionDamping
			              * selfQGB.Power
			              / opponentQGB.Resistance;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

		if (rb != null && other.CompareTag("Player"))
		{
			QuantumGyroBlade selfQGB = GetComponent<MovementController>().QGB;
			QuantumGyroBlade opponentQGB = other.GetComponent<MovementController>().QGB;
			AudioManager.instance.PlayClangSound();
			float mag = _rigidbody2D.velocity.magnitude;
			_rigidbody2D.velocity = (transform.position - other.transform.position)
			                        * rb.velocity.magnitude
			                        * CollisionDamping
			                        * opponentQGB.Power
			                        / selfQGB.Resistance;
			rb.velocity = (other.transform.position - transform.position)
			              * mag
			              * CollisionDamping
			              * selfQGB.Power
			              / opponentQGB.Resistance;
		}
	}

	void Feedback(GameObject other, Vector3 toSpawn, float totalMagnitude)
	{
		AudioManager.instance.PlayClangSound();
		_particleSystem.transform.position = toSpawn;
		Camera.main.GetComponent<CameraController>().ScreenShake(totalMagnitude);
		// UnityEngine.ParticleSystem.ShapeModule editableShape = _particleSystem.shape;
		// Vector3 newRotation = new Vector3(0, 0, Vector2.SignedAngle(Vector2.down, other.transform.position - transform.position) + 45);
		// editableShape.rotation = newRotation;
		_particleSystem.Play();
	}
}