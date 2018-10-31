using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NetworkSceneManager : MonoBehaviourPunCallbacks
{
	public static NetworkSceneManager instance;

	public GameObject playerPrefab;

	void Start()
	{
		instance = this;
		if (playerPrefab == null)
		{
			Debug.LogError("Missing playerPrefab Reference. Please set it up in GameObject 'Game Manager'");
		}
		else
		{
			Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManager.GetActiveScene().name);
			// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 3f * PhotonNetwork.LocalPlayer.ActorNumber, 0f), Quaternion.identity, 0);
		}
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("Matchmaking");
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Debug.Log("OnPlayerEnteredRoom()");
		if(PhotonNetwork.IsMasterClient)
		{
			Debug.Log("OnPlayerEnteredRoom() master client");
			LoadArena();
		}
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	private void LoadArena()
	{
		if(!PhotonNetwork.IsMasterClient)
		{
			Debug.Log("Error: trying to load scene on non-master client");
		}
		Debug.Log("Loading arena");
		PhotonNetwork.LoadLevel("NetworkedTesting");
	}
}
