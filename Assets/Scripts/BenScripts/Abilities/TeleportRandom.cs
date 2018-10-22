using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Teleports player onto a random location on the stage (preferably).
//Assumed that stage is in shape of a circle.
//Stage MUST HAVE the StageRotator component. If you don't want the stage to rotate, just set the StageRotator values to something impossible.

public class TeleportRandom : Ability {

    public float cooldown = 2.0f;
    public float minRadius = 6.0f;       //minRange is used to avoid bumper in the center of stage (assuming standard stage).
    public float maxRadiusCorrector = 2.0f;     //Small value to ensure player does not teleport on the very edge of the screen.

    public bool ready;

    private static StageRotator stage;
    private float stopwatch;

    private void Awake()
    {
        stage = StageRotator.Instance;

        if (stage == null)  //Checks existence of StageRotator in scene
        {
            Debug.Log("TeleportRandom Error: StageRotator does not exist in the scene. Disabling component.");
            this.enabled = false;
        }

        switch (gameObject.name)
        {
            case "PlayerOne":
                abilityKey = "AbilityPlayerOne";
                break;
            case "PlayerTwo":
                abilityKey = "AbilityPlayerTwo";
                break;
            default:
                Debug.Log("TeleportRandom Error: PlayerName not found. Disabling component.");
                this.enabled = false;
                break;
        }

        stopwatch = 0.0f;
        ready = false;
    }

    private void Teleport()
    {
        float maxRadius = (stage.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2)
                            - maxRadiusCorrector;

        Vector2 placement = Random.insideUnitCircle * maxRadius;

        while (Mathf.Abs(placement.x) < minRadius && Mathf.Abs(placement.y) < minRadius)
            placement = Random.insideUnitCircle * maxRadius;

        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.transform.position = placement;
    }

    void Update()
    {
        stopwatch += Time.deltaTime;

        if (stopwatch > cooldown)
            ready = true;

        if (Input.GetButtonDown(abilityKey) && ready == true)
        {
            Teleport();
            ready = false;
            stopwatch = 0.0f;
        }
    }




}
