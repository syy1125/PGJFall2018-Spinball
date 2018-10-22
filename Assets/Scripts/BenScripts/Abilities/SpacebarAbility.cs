using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacebarAbility : MonoBehaviour {

    public float cooldown = 10.0f;
    public bool ready;
    public float stopwatch;

    public static SpacebarAbility Instance;

    private static StageRotator stage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            Debug.Log("SpacebarAbility Error: Instance of SpacebarAbility already exists. Deleting this gameObject.");
        }

        stopwatch = 0.0f;
        ready = false;
    }

    // Use this for initialization
    void Start () {

        stopwatch = 0.0f;
        stage = StageRotator.Instance;

        if (stage == null)
            Debug.Log("SpacebarAbility Error: StageRotator does not exist in this scene.");
	}

    private void FlipRotation()
    {
        stage.rotationalSpeed *= -1;
        stage.rampModifier *= -1;
        stage.maxSpeed *= -1;
    }
	
    private 

	// Update is called once per frame
	void Update () {

        stopwatch += Time.deltaTime;

        if (stopwatch > cooldown)
            ready = true;

		if (Input.GetKeyDown(KeyCode.Space) && ready == true)
        {
            FlipRotation();
            stopwatch = 0.0f;
            ready = false;
        }
	}
}
