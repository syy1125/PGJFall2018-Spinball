using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text Clock;
    public float timer = 0f;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");

        Clock.text = string.Format("{0}:{1}", minutes, seconds);
	}
}
