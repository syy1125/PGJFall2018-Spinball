using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpinballNetworkManager : NetworkManager
{
	public override void OnServerAddPlayer(NetworkConnection connection, short playerControllerId)
	{
		base.OnServerAddPlayer(connection, playerControllerId);
		for(int x = 0; x < connection.playerControllers.Count; ++x)
		{
			if(connection.playerControllers[x].gameObject.CompareTag("Player"))
			{
				Camera.main.GetComponent<CameraController>().players.Add(connection.playerControllers[x].gameObject);
			}
		}
	}

	public override void OnServerConnect(NetworkConnection connection)
	{
		base.OnServerConnect(connection);
		Debug.Log("connection!" + connection);
	}
}
