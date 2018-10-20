using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour 
{
	private Rigidbody2D rigidbody;

	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject other = col.gameObject;
		Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
		if(rb != null && other.CompareTag("Player"))
		{
			Debug.Log("mine : " + rigidbody.velocity);
			Debug.Log("other: " + rb.velocity);
			float mag = rigidbody.velocity.magnitude;
			rigidbody.velocity = (transform.position - other.transform.position) * rb.velocity.magnitude;
			rb.velocity = (other.transform.position - transform.position) * mag;
		}
	}
}
