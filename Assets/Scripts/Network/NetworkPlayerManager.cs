using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPlayerManager : MonoBehaviourPunCallbacks
{
	public static GameObject localPlayer;

	void Start () 
	{
		if(photonView.IsMine)
		{
			localPlayer = this.gameObject;	
		}
		Camera.main.GetComponent<CameraController>().players.Add(this.gameObject);
	}

	// void OnDestroy()
	// {
	// 	Camera.main.GetComponent<CameraController>().players.Remove(this.gameObject);
	// }

}
