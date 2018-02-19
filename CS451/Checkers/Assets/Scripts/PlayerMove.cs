using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{

	public NetworkIdentity myId;
	
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
	void Start()
	{
		if (isServer)
			GetComponent<MeshRenderer>().material.color = Color.black;
	}

	void Update()
	{
		if (!isLocalPlayer)
			return;
		//if (isClient)
		//	print ("I am the host");
		
		
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
	}
}