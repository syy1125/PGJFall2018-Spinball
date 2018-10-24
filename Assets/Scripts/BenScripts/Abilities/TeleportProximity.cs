using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProximity : Ability {

    public float maxRadius = 10.0f;
    public float minRadius = 3.0f;

    private void Start()
    {
        cooldown = 0.1f;
        stopwatch = 0.0f;
        ready = false;
    }

    private void Awake()
    {
        switch (gameObject.name)
        {
            case "PlayerOne":
                abilityKey = "AbilityPlayerOne";
                break;
            case "PlayerTwo":
                abilityKey = "AbilityPlayerTwo";
                break;
            default:
                Debug.Log("TeleportProximity Error: PlayerName not found. Disabling component.");
                this.enabled = false;
                break;
        }
    }

    private void Teleport()
    {
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.Log("TeleportProximity Error: Rigidbody2D does not exist. Teleport failed.");
        }
        else
        {
            Vector2 placement = new Vector2(rb.transform.position.x, rb.transform.position.y)
                                    + Random.insideUnitCircle * maxRadius;

            if (Mathf.Abs(placement.x) < minRadius)
                placement.x = minRadius;
            if (Mathf.Abs(placement.y) < minRadius)
                placement.y = minRadius;

            rb.transform.position = placement;
        }
    }
	
	// Update is called once per frame
	void Update () {
        stopwatch += Time.deltaTime;

        if (stopwatch > cooldown)
            ready = true;

        if(Input.GetButtonDown(abilityKey) && ready == true)
        {
            Teleport();
            ready = false;
            stopwatch = 0.0f;
        }
	}
}
