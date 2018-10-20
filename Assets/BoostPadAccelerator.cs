using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPadAccelerator : MonoBehaviour {

    private Rigidbody2D RigidBODY;

    private int timeElapsed;
    public int maxTime;

    // Use this for initialization
    void Start () {
        RigidBODY = GetComponent<Rigidbody2D>();
        int timeElapsed = 0;
        maxTime = 2000;
    }
    private void OnTriggerEnter2D(Collider2D collida)
    {
        GameObject theThing = collida.gameObject;
        Rigidbody2D rb = theThing.GetComponent<Rigidbody2D>();
        if (theThing.CompareTag("Player") && rb!= null)
        {
            Debug.Log("old: " + rb.velocity);
            rb.velocity = new Vector2(rb.velocity.x * 1.5f, rb.velocity.y * 1.5f);
            Debug.Log("new: "+ rb.velocity);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    private void FixedUpdate()
    {
        timeElapsed += 1;
        if (timeElapsed > maxTime)
        {
            Destroy(gameObject);
        }
    }
}
