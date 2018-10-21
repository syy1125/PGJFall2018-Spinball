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
                float x = 0;
                float y = 0;
                while (Mathf.Abs(x) < radiusMinimum || Mathf.Abs(y) < radiusMinimum)
                {
                    x = Random.insideUnitCircle.x * radiusLimit;
                    y = Random.insideUnitCircle.y * radiusLimit;
                }
                //Vector2 placement = new Vector2(Random.insideUnitCircle.x * radiusLimit, Random.insideUnitCircle.y * radiusLimit);
                Vector2 placement = new Vector2(x, y);
                GameObject bomb = (GameObject)Instantiate(bombPrefab, placement, Quaternion.identity);
            }
        }
	}
}
