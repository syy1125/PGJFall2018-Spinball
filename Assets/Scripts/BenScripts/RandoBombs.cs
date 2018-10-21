using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandoBombs : MonoBehaviour {

    public GameObject bombPrefab;

    public int countdown = 0;       //time before bomb dropping begin
    public float cooldown = 0.0f;   //time between bomb drops

    public float radiusLimit = 0.0f;
    public float radiusMinimum = 0.0f;

    private float stopwatch;
    private float bombStopwatch;


	// Use this for initialization
	void Start ()
    {
        stopwatch = 0.0f;
        bombStopwatch = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        stopwatch += Time.deltaTime;
        bombStopwatch += Time.deltaTime;

        if (stopwatch >= countdown)
        {
            if (bombStopwatch > cooldown)
            {
                bombStopwatch = 0;

                Vector2 placement = Random.insideUnitCircle * radiusLimit;

                while (Mathf.Abs(placement.x) < radiusMinimum && Mathf.Abs(placement.y) < radiusMinimum)
                    placement = Random.insideUnitCircle * radiusLimit;
                
                GameObject bomb = (GameObject)Instantiate(bombPrefab, placement, Quaternion.identity);
            }
        }
	}
}
