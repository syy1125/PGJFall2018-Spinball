using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRotatwo : MonoBehaviour {

    public bool reversal;

    private static StageRotator stage;
    private float rotationalSpeed;

    private void Awake()
    {
        stage = StageRotator.Instance;

        if (stage == null)  //Checks existence of StageRotator in scene
            Debug.Log("Rotatwo Error: StageRotator does not exist in the scene.");
    }

    // Update is called once per frame
    void Update()
    {
        if (reversal == true)
            rotationalSpeed = stage.rotationalSpeed * -1;
        else
            rotationalSpeed = stage.rotationalSpeed;

        transform.RotateAround(Vector3.zero, Vector3.forward, rotationalSpeed * Time.deltaTime);
    }
}
