using System;
using UnityEngine;
using UnityEngine.Serialization;
using Photon.Pun;

public class NetworkPlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
	[FormerlySerializedAs("InputStrength")]
	public float BaseInputStrength;

	public string HorizontalAxisName;
	public string VerticalAxisName;
	public QuantumGyroBlade QGB;

	private Rigidbody2D _rigidbody2D;
	private Vector3 networkPosition;
	private Quaternion networkRotation;
	private Vector2 networkVelocity;
	private float lastNetworkUpdate;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if(!photonView.IsMine && PhotonNetwork.IsConnected)
		{
			this.transform.position = SmoothTransform();
			this.transform.rotation = SmoothRotation();
			_rigidbody2D.velocity = networkVelocity;
		}
	}

	private Vector3 SmoothTransform()
	{
		if(Vector3.Distance(networkPosition, this.transform.position) >= 2)
		{
			return networkPosition;
		}
		return Vector3.Lerp(transform.position, networkPosition, Time.deltaTime);
	}

	private Quaternion SmoothRotation()
	{
		return networkRotation;
		//return Quaternion.RotateTowards(this.transform.rotation, networkRotation, Time.deltaTime * 100f);
	}

	void FixedUpdate()
	{
		if(!photonView.IsMine && PhotonNetwork.IsConnected)
		{
			return;
		}

		float horiz = Input.GetAxisRaw(HorizontalAxisName);
		float vert = Input.GetAxisRaw(VerticalAxisName);
		
		Vector2 movementForce =new Vector2(horiz, vert).normalized 
		                       * BaseInputStrength 
		                       * QGB.Acceleration;
		float degrees = Vector2.Angle(_rigidbody2D.velocity, movementForce);
		if (150 > degrees)
		{
			_rigidbody2D.AddForce(movementForce);
		}
		else
		{
			_rigidbody2D.AddForce(movementForce * 0.6f);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		//Debug.Log("PhotonSerialize at " + Time.time);
		if (stream.IsWriting)
		{
			//We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(_rigidbody2D.velocity);
			stream.SendNext(transform.rotation);
		}
		else
		{
			//Network player, receive data
			networkPosition = (Vector3)stream.ReceiveNext();
			networkVelocity = (Vector2)stream.ReceiveNext();
			networkRotation = (Quaternion)stream.ReceiveNext();
			lastNetworkUpdate = Time.time;
		}
	}
}