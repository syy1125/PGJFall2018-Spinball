using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
	public static AudioManager instance;

	private AudioSource music;
	private AudioSource effects;

	void Start () 
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		instance = null;
		DontDestroyOnLoad(gameObject);

		music.Play();
	}
}
