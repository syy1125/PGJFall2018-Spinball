using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProximity : Ability {

    public float cooldown = 5.0f;
    public float maxRadius = 5.0f;
    public bool ready;

    private float stopwatch;

    private void Start()
    {
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
            Vector2 newPosition = new Vector2(rb.transform.position.x, rb.transform.position.y)
                                    + Random.insideUnitCircle * maxRadius;

            rb.transform.position = newPosition;
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
