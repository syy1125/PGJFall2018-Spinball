using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnCollision : MonoBehaviour {
    [SerializeField] Transform spawnPoint;

    
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered!");
        if (col.transform.CompareTag("Player"))
            col.transform.position = spawnPoint.position;
    }
    

    /*
     void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fuck you");
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }  
    */
    
}
