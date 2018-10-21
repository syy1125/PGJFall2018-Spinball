using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour 
{
	public float collisionDamping;

	private Rigidbody2D _rigidbody2D;

	void Start () 
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
		if(rb != null && other.CompareTag("Player"))
		{
			// Debug.Log("mine : " + _rigidbody2D.velocity);
			// Debug.Log("other: " + rb.velocity);
			float mag = _rigidbody2D.velocity.magnitude * collisionDamping;
			_rigidbody2D.velocity = (transform.position - other.transform.position) * rb.velocity.magnitude * collisionDamping;
			rb.velocity = (other.transform.position - transform.position) * mag;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
		if(rb != null && other.CompareTag("Player"))
		{
			// Debug.Log("mine : " + _rigidbody2D.velocity);
			// Debug.Log("other: " + rb.velocity);
			float mag = _rigidbody2D.velocity.magnitude * collisionDamping;
			_rigidbody2D.velocity = (transform.position - other.transform.position) * rb.velocity.magnitude * collisionDamping;
			rb.velocity = (other.transform.position - transform.position) * mag;
		}
	}
}
