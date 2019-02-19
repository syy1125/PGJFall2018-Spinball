using UnityEngine;

public class DeathOnCollision : MonoBehaviour
{
	[SerializeField]
	private Transform spawnPoint;


	private void OnTriggerEnter2D(Collider2D col)
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