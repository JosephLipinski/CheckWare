using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour {

	// public string location; //Current location of the piece
	public bool king = false; //Is it a kinged piece?
	public bool player = false; //Player 1 is false, Player 2 is 
	public float smooth = 5.0f;
	public Vector3 target;
	public float currentLerpTime = 1.0f;
	public float lerpTime = 1.0f;

	// Use this for initialization
	void Start () {
		target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		currentLerpTime += Time.deltaTime;

		if(currentLerpTime > lerpTime){
			currentLerpTime = lerpTime;
		}
		float perc = currentLerpTime / lerpTime;

		if(currentLerpTime < lerpTime/4){
			target.y = transform.position.y + 1.5f;
		} else {
			target.y = transform.position.y;
		}


		transform.position = Vector3.Lerp( transform.position, target, easeInOut(currentLerpTime));
	}

	public void movePiece(GameObject piece, Vector3 targetPos){
		target = targetPos;	
		currentLerpTime = 0f;
	}

	public static float easeInOut(float t, float e = 2f)
    {
        return Mathf.Pow(t, e) / (Mathf.Pow(t, e) + Mathf.Pow(1f - t, e));
    }

	public void setPlayer(bool _player){
		player = _player;
	}

	// ArrayList getLegalMoves(){
	// 	ArrayList list = new ArrayList();

		
	// 	//if piece is not king and p1
	// 	if(playerNum == true && king == false){
			
	// 	}
	// 	//if piece is king and p1
	// 	if(playerNum == true && king == false){
			
	// 	}
	// 	//if piece is not king and p2
	// 	if(playerNum == false && king == false){
			
	// 	}
	// 	//if piece is king and p2
	// 	if(playerNum == false && king == true){
			
	// 	}
		
	// 	//if diagonally its empty
	// 	//if diagonally its off board
	// 	//if diagonally its enemy

		
	// }
}
