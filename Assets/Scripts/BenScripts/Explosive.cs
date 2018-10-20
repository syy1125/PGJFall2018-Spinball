using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

    public int health = 3;
    public float blastForce = 0.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            --health;

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                if (collision.transform.position.x < this.transform.position.x) //applying force in the x direction, based on relative positions of barrel and collider
                    rb.AddForce(new Vector2(-blastForce, 0));
                else
                    rb.AddForce(new Vector2(blastForce, 0));

                if (collision.transform.position.y < this.transform.position.y) //applying force in the y direction, based on relative positions of barrel and collider
                    rb.AddForce(new Vector2(0, -blastForce));
                else
                    rb.AddForce(new Vector2(0, blastForce));
            }
            
            if (health == 0)
                Destroy(this.gameObject);
        }
    }
}
