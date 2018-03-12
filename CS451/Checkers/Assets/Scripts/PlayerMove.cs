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
	public Material blue;
	public Material purple;
	Color myColor, colorBlue, colorPurple;


	bool testbool = false;

	[Command]
	public void CmdToggleCurrentPlayer() {
		var objNetId = board.GetComponent<NetworkIdentity> ();        // get the object's network ID
		objNetId.AssignClientAuthority (connectionToClient);    // assign authority to the player who is changing the color
		RpcTogglePlease();
        objNetId.RemoveClientAuthority (connectionToClient);    // remove the authority from the player who changed the color

	}

	[ClientRpc]
	void RpcTogglePlease() {
		bm.currentPlayer = !bm.currentPlayer;
	}

	public override void OnStartLocalPlayer()
	{
		colorBlue = blue.color;
		colorPurple = purple.color;

		if (isServer) {
			GetComponent<MeshRenderer> ().material.color = colorPurple;
			myColor = colorPurple;
		}
		else {
			GetComponent<MeshRenderer> ().material.color = colorBlue;
			myColor = colorBlue;
		}
	}

	void Start()
	{

		board = GameObject.Find("Board");
		bm = board.GetComponent<BoardManager>();
		
		if (isServer)
			GetComponent<MeshRenderer> ().material.color = purple.color;
		else {
			GetComponent<MeshRenderer> ().material.color = blue.color;
		}
	}

	void Update()
	{
		if (!isLocalPlayer)
			return;

		hoverOver();

		// print(testbool);
		print(bm.currentPlayer);

		// leftClick();

		if(bm.currentPlayer == true && myColor == colorPurple){
			leftClick();
		} else if (bm.currentPlayer == false && myColor == colorBlue){
			leftClick();
		}
	}

	void hoverOver(){

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
	}

	void leftClick(){
		if (Input.GetMouseButtonDown(0)){ //left click
			// Debug.Log(bm.currentPlayer);
			// If you click one of your pieces
			if(boardLocationName != null && !bm.isLocationEmpty(boardLocationName)){
				bm.getLegalMoves(bm.getLocation(boardLocationName));
			}

			if(boardLocationName != null && bm.isCurrentLegalMove(boardLocationName)){
				bm.movePiece(boardLocationName);
				CmdToggleCurrentPlayer();				
			}
		} 
	}
}