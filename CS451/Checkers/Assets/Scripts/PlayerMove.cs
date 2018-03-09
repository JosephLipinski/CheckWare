using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
	Ray ray;
	RaycastHit hit;

	public NetworkIdentity myId;
	public LayerMask mask = -1;
	public float smooth = 5.0f;

	GameObject board;
	BoardManager bm;
	Vector3 boardLoc;
	string boardLocationName;

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
	void Start()
	{

		board = GameObject.Find("Board");
		bm = board.GetComponent<BoardManager>();
		
		if (isServer)
			GetComponent<MeshRenderer>().material.color = Color.black;
	}

	void Update()
	{
		if (!isLocalPlayer)
			return;
		//if (isClient)
		//	print ("I am the host");
		
		
		//var x = Input.GetAxis("Horizontal")*0.1f;
		//var z = Input.GetAxis("Vertical")*0.1f;

		//transform.Translate(x, 0, z);

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit, 100, mask.value)) // mouse over
         {
	    	boardLoc = new Vector3(hit.collider.gameObject.transform.position.x, 0.25f,hit.collider.gameObject.transform.position.z);
			 boardLocationName = hit.collider.name;	
			 transform.position = Vector3.Lerp (transform.position, boardLoc, Time.deltaTime * smooth);		 
         } else {
			 Plane hPlane = new Plane(Vector3.up, Vector3.zero);
			 float distance = 0; 
			if (hPlane.Raycast(ray, out distance)){
				transform.position =  Vector3.Lerp (transform.position, ray.GetPoint(distance), Time.deltaTime * smooth);
   			}
		 }
        	
		 

		 if (Input.GetMouseButtonDown(0)){ //left click

			if(!bm.isLocationEmpty(boardLocationName)){
				bm.getLegalMoves(boardLocationName);
			}

			if(bm.isCurrentLegalMove(boardLocationName)){
				bm.movePiece(boardLocationName);
			}
		}

		
		
	}
}