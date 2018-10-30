using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
	[SerializeField]
	private byte maxPlayersPerRoom = 4;

	private string gameVersion = "1";
	
	private RoomOptions roomOptions;
	private bool connecting;

#region UnityMonoBehaviourCallbacks
	void Awake()
	{
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	void Start()
	{
		roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = maxPlayersPerRoom;
	}
#endregion

	public void Connect()
	{
		connecting = true;
		if(PhotonNetwork.IsConnected)
		{
			PhotonNetwork.JoinRandomRoom();
		}
		else
		{
			PhotonNetwork.GameVersion = gameVersion;
			PhotonNetwork.ConnectUsingSettings();
		}
	}

#region PhotonCallbacks
	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster");
		if(connecting)
		{
			PhotonNetwork.JoinRandomRoom();
		}
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		Debug.Log("OnDisconnected for reason " + cause);
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("OnJoinRandomFailed");
		PhotonNetwork.CreateRoom(null, roomOptions);
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		PhotonNetwork.LoadLevel("NetworkedTesting");
	}
#endregion
}
