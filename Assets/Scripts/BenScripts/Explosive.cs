using System.Collections;
using UnityEngine;

public class Explosive : MonoBehaviour
{
	public int health = 3;
	public float blastForce;
	public float bounceForce;
	public float blastRadius = 1.0f;
	public float fadeInTime = 1.0f;
	public LayerMask mask = -1;

	public GameObject explosionObject;

	private IEnumerator Start()
	{
		GetComponent<CircleCollider2D>().enabled = false;
		yield return new WaitForSeconds(fadeInTime);
		GetComponent<CircleCollider2D>().enabled = true;
	}

	private void Update()
	{
		transform.Rotate(0, 0, Random.Range(-2, 2));
	}

	private void Explosion()
	{
		PlayBombSound();
		Instantiate(explosionObject, transform.position, Quaternion.identity);
		Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, blastRadius);
		foreach (Collider2D col in overlaps)
		{
			Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
			if (rb != null && col.gameObject.CompareTag("Player"))
			{
				rb.velocity = Vector2.zero;
				Vector2 direction = rb.position - (Vector2) transform.position;
				direction.Normalize();
				rb.AddForce(direction * blastForce, ForceMode2D.Impulse);
			}
		}
	}

	private void Bounce(Collider2D collision)
	{
		Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

		if (rb != null)
		{
			rb.velocity = Vector2.zero;
			rb.AddForce(rb.position - new Vector2(transform.position.x, transform.position.y) * bounceForce);
		}
	}


	// void OnCollisionEnter2D(Collision2D collision)
	// {
	//     if (collision.gameObject.CompareTag("Player"))
	//     {
	//         --health;

	//         if (health == 0)
	//         {
	//             Explosion();
	//             Destroy(gameObject);
	//         }
	//         else
	//         {
	//             Bounce(collision);
	//         }
	//     }
	// }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			--health;

			if (health == 0)
			{
				Explosion();
				Destroy(gameObject);
			}
			else
			{
				Bounce(collision);
			}
		}
	}

	private void PlayBombSound()
	{
		AudioManager.instance.PlayBombSound();
	}
}