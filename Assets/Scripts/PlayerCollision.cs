using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCollision : MonoBehaviour
{
	[FormerlySerializedAs("collisionDamping")]
	public float CollisionDamping;

	public QuantumGyroBlade QGB;

	private Rigidbody2D _rigidbody2D;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
		QuantumGyroBlade opponentQGB = other.GetComponent<MovementController>().QGB;

		if (rb != null && other.CompareTag("Player"))
		{
			AudioManager.instance.PlayClangSound();
			float mag = _rigidbody2D.velocity.magnitude;
			_rigidbody2D.velocity = (transform.position - other.transform.position)
			                        * rb.velocity.magnitude
			                        * CollisionDamping
			                        * opponentQGB.Power
			                        / QGB.Resistance;
			rb.velocity = (other.transform.position - transform.position)
			              * mag
			              * CollisionDamping
			              * QGB.Power
			              / opponentQGB.Resistance;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
		QuantumGyroBlade opponentQGB = other.GetComponent<MovementController>().QGB;

		if (rb != null && other.CompareTag("Player"))
		{
			AudioManager.instance.PlayClangSound();
			float mag = _rigidbody2D.velocity.magnitude;
			_rigidbody2D.velocity = (transform.position - other.transform.position)
			                        * rb.velocity.magnitude
			                        * CollisionDamping
			                        * opponentQGB.Power
			                        / QGB.Resistance;
			rb.velocity = (other.transform.position - transform.position)
			              * mag
			              * CollisionDamping
			              * QGB.Power
			              / opponentQGB.Resistance;
		}
	}
}