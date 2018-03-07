using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour {

	// public string location; //Current location of the piece
	public bool king = false; //Is it a kinged piece?
	public bool player = false; //Player 1 is false, Player 2 is true

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
