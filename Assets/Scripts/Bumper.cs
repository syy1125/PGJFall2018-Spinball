using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounceable { }

public class Bumper : MonoBehaviour 
{
	public float bounceForce;

	void OnTriggerEnter2D(Collider2D col)
	{
		//Debug.Log("bounce col");
		Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
		if(rb != null)
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(bounceForce * (col.gameObject.transform.position - this.transform.position) );
			//rb.AddForce( (this.transform.position - rb.gameObject.transform.position) * bounceForce );
		}
	}
}
