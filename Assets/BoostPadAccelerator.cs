using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPadAccelerator : MonoBehaviour {

    public float maxTime = 15;

    private Rigidbody2D RigidBODY;
    private float timeElapsed;

    void Start () {
        RigidBODY = GetComponent<Rigidbody2D>();
        timeElapsed = 0;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject theThing = collider.gameObject;
        Rigidbody2D rb = theThing.GetComponent<Rigidbody2D>();
        if (theThing.CompareTag("Player") && rb != null)
        {
            Debug.Log("old: " + rb.velocity);
            rb.velocity = new Vector2(rb.velocity.x * 1.5f, rb.velocity.y * 1.5f);
            Debug.Log("new: "+ rb.velocity);
        }
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed > maxTime)
        {
            Destroy(gameObject);
        }
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(this.transform.position, 80);
        Vector3 closestPos = Vector2.zero;
        float minDist = float.MaxValue;
        for(int x = 0; x < nearby.Length; ++x)
        {
            if(nearby[x].CompareTag("Player") && (nearby[x].transform.position - this.transform.position).magnitude < minDist)
            {
                minDist = (nearby[x].transform.position - this.transform.position).magnitude;
                closestPos = nearby[x].gameObject.transform.position;
            }
        }
        this.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, closestPos - transform.position));
    }
}
