using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
	Ray ray;
	RaycastHit hit;

	public NetworkIdentity myId;
	public LayerMask mask = -1;
	public float smooth = 5.0f;
	
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

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit, 100, mask.value))
         {
             print (hit.collider.name);

             Vector3 boardLoc = new Vector3(hit.collider.gameObject.transform.position.x, 0.25f,hit.collider.gameObject.transform.position.z);

             //transform.SetPositionAndRotation(boardLoc, transform.rotation);

             transform.position = Vector3.Lerp (transform.position, boardLoc, Time.deltaTime * smooth);


         }
	}
}