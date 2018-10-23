using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHUD : MonoBehaviour {

    public Text spaceText;
    public Text abilityOneText;
    public Text abilityTwoText;

    public GameObject p1;
    public GameObject p2;

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
            spaceText.text = "SPACEBAR READY!";
        }
        else
        {
            string seconds = (space.cooldown - space.stopwatch).ToString("#.00");

            spaceText.text = seconds;
        }
	}
}
