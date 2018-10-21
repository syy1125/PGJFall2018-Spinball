using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script rotates around the z-axis

public class StageRotator : MonoBehaviour {

    public int countdown = 10;  //Time in seconds before stage begins to rotate
    public float rampModifier = 1.0f;   //speed increase modifier
    public float rotationalSpeed = 1.0f;
    public float maxSpeed = 60.0f;

    private float stopwatch;

    public static StageRotator Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        stopwatch = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        stopwatch += Time.deltaTime;

        if (stopwatch >= countdown)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotationalSpeed, Space.World);
            if (rotationalSpeed < maxSpeed)
                rotationalSpeed += Time.deltaTime * rampModifier;
        }
    }
}
