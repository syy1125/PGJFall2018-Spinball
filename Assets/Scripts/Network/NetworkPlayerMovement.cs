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
	private Vector3 currentPosition;
	private Quaternion currentRotation;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		this.transform.position = SmoothTransform();
		this.transform.rotation = SmoothRotation();
	}

	private Vector3 SmoothTransform()
	{
		
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
		if (stream.IsWriting)
		{
			//We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		}
		else
		{
			//Network player, receive data
			currentPosition = (Vector3)stream.ReceiveNext();
			currentRotation = (Quaternion)stream.ReceiveNext();
		}
	}
}