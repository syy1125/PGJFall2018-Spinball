using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatwo : MonoBehaviour {

    private static StageRotator stage;
    private float rotationalSpeed;

    private void Start()
    {
        stage = StageRotator.Instance;

        if (stage == null)  //Checks existence of StageRotator in scene
            Debug.Log("Rotatwo Error: StageRotator does not exist in the scene.");
    }

    // Update is called once per frame
    void Update()
    {
        rotationalSpeed = stage.rotationalSpeed;
        transform.RotateAround(Vector3.zero, Vector3.forward, rotationalSpeed * Time.deltaTime);   
    }
}
