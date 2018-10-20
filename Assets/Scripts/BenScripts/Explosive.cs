using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

    public int health = 3;
    public float blastForce = 0.0f;
    public float bounceForce = 0.0f;
    public float blastRadius = 1.0f;
    public LayerMask mask = -1;

    private void Explosion()
    {
        ContactFilter2D contact = new ContactFilter2D();
        contact.layerMask = mask;
        Collider2D[] results = new Collider2D[10];

        int hitByBlast = Physics2D.OverlapCircle(this.transform.position, blastRadius, contact, results);
        if (hitByBlast > 0)
        {
            for (int i = 0; i < hitByBlast; ++i)
            {
                Rigidbody2D rb = results[i].GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(rb.position - new Vector2(transform.position.x, transform.position.y) * blastForce);
                }
            }
        }
    }

    private void Bounce(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(rb.position - new Vector2(transform.position.x, transform.position.y) * bounceForce);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            --health;

            if (health == 0)
            {
                Explosion();
                Destroy(this.gameObject);
            }
            else
            {
                Bounce(collision);
            }
        }
    }
}
