using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceAbilityHUD : MonoBehaviour {

    public Text timerText;

    private static SpacebarAbility space;

	// Use this for initialization
	void Start () {

        space = SpacebarAbility.Instance;

        if (space == null)
            Debug.Log("SpaceAbilityHUD Error: SpaceBarAbility Instance does not exist in this scene.");
	}
	
	// Update is called once per frame
	void Update () {
        
        if (space.ready == true)
        {
            timerText.text = "SPACEBAR READY!";
        }
        else
        {
            string seconds = (space.cooldown - space.stopwatch).ToString("#.00");

            timerText.text = seconds;
        }
	}
}
