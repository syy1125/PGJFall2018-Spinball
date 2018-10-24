using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHUD : MonoBehaviour
{

    public Text spaceText;
    public Text abilityOneText;
    public Text abilityTwoText;

    public GameObject p1;
    public GameObject p2;

    private static SpacebarAbility space;

    // Use this for initialization
    void Start()
    {

        space = SpacebarAbility.Instance;

        if (space == null)
            Debug.Log("SpaceAbilityHUD Error: SpaceBarAbility Instance does not exist in this scene.");
    }

    private void SpaceTimer()
    {
        if (space == null)
        {
            spaceText.text = "NONE";
            return;
        }

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

    private void PlayerOneAbilityTimer()
    {
        Ability ability = p1.GetComponent<Ability>();

        if(ability == null)
        {
            abilityOneText.text = "NONE";
            return;
        }

        if (ability.ready == true)
        {
            abilityOneText.text = "READY!";
        }
        else
        {
            string seconds = (ability.cooldown - ability.stopwatch).ToString("#.0");

            abilityOneText.text = seconds;
        }
    }

    private void PlayerTwoAbilityTimer()
    {

        Ability ability = p2.GetComponent<Ability>();

        if (ability == null)
        {
            abilityOneText.text = "NONE";
            return;
        }

        if (ability.ready == true)
        {
            abilityTwoText.text = "READY!";
        }
        else
        {
            string seconds = (ability.cooldown - ability.stopwatch).ToString("#.0");

            abilityTwoText.text = seconds;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpaceTimer();
        PlayerOneAbilityTimer();
        PlayerTwoAbilityTimer();
    }
}
