﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickFixo : MonoBehaviour {

    public float countdown = 840.0f;

	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
	}
}
