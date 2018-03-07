using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	List<List<BoardLocation>> board;

	bool currentPlayer = false;

	public GameObject blueChecker, blueKing, purpleChecker, purpleKing;

	// Use this for initialization
	void Start () {
		setupBoard();
		//getLegalMoves("C2");
		hasLegalMoves();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setupBoard() {

		char[][] boardSetup = new char[][] {
			new char[]{' ', '1', ' ', '1', ' ', '1', ' ', '1'}, //A
			new char[]{'1', ' ', '1', ' ', '1', ' ', '1', ' '}, //B
			new char[]{' ', '1', ' ', '1', ' ', '1', ' ', '1'}, //C
			new char[]{' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, //D
			new char[]{' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, //E
			new char[]{'2', ' ', '2', ' ', '2', ' ', '2', ' '}, //F
			new char[]{' ', '2', ' ', '2', ' ', '2', ' ', '2'}, //G
			new char[]{'2', ' ', '2', ' ', '2', ' ', '2', ' '}  //H
		};

		board = new List<List<BoardLocation>>();

		//maps each location on a 2d list to a BoardLocation object
		for(int i = 0; i < 8; i++){
			board.Add(new List<BoardLocation>());
			for(int j = 0; j < 8; j++){
				Transform child = transform.GetChild((i*8) + j);
				Vector3 spawnPosition = child.transform.position + new Vector3 (0f, 1f, 0f);
				if(boardSetup[i][j] == '1'){
					GameObject P1_Piece = Instantiate (blueChecker, spawnPosition, Quaternion.identity);
					board[i].Add(new BoardLocation(child, P1_Piece, i, j)); // stores cube location and player1 piece
				} else if(boardSetup[i][j] == '2'){
					GameObject P2_Piece = Instantiate (purpleChecker, spawnPosition, Quaternion.identity);
					board[i].Add(new BoardLocation(child, P2_Piece, i, j)); // stores cube location and player2 piece
				} else if(boardSetup[i][j] == ' '){
					board[i].Add(new BoardLocation(child, i, j)); // stores each cube locaiton that is empty					
				}
			}
		}
	}

	bool hasLegalMoves(){
		// List<BoardLocation> allLegalMoves = new List<BoardLocation>();
		bool hasLegalMove = false;
		foreach(Transform child in transform){
			List<BoardLocation> locationMoves = getLegalMoves(child.name);
			if(locationMoves.Count > 0) hasLegalMove = true;
		}
		print(hasLegalMove);
		return hasLegalMove;
	}	

	List<BoardLocation> getLegalMoves(string location){
		BoardLocation boardLocation = getLocation(location);
		List<BoardLocation> legalMoves = new List<BoardLocation>();
		if(!boardLocation.empty()){ //if there is a piece at this location

			PieceHandler ph = boardLocation.piece.GetComponent<PieceHandler>();
			if(ph.player == currentPlayer){
				int i = boardLocation.i;
				int j = boardLocation.j;

				calculateLegalMoves(ref i, ref j, ref legalMoves, ref ph, 1, 1);  //if the NE space is empty
				calculateLegalMoves(ref i, ref j, ref legalMoves, ref ph, 1, -1); //if the NW space is empty
				if(ph.king == true){
					calculateLegalMoves(ref i, ref j, ref legalMoves, ref ph, -1, 1);  //if the SE space is empty
					calculateLegalMoves(ref i, ref j, ref legalMoves, ref  ph, -1, -1); //if the SW space is empty
				}
			}
		}
		
		//print("CURRENT LOCATION: ("+boardLocation.i+", " +boardLocation.j+")");		
		if(legalMoves.Count == 1){
			print("LEGAL LOCATION 1: ("+legalMoves[0].i+", " +legalMoves[0].j+")");
		}
		if(legalMoves.Count == 2){
			print("LEGAL LOCATION 1: ("+legalMoves[0].i+", " +legalMoves[0].j+")");
			print("LEGAL LOCATION 2: ("+legalMoves[1].i+", " +legalMoves[1].j+")");
		}
		return legalMoves;
	}

	void calculateLegalMoves(ref int i, ref int j, ref List<BoardLocation> legalMoves, ref PieceHandler ph, int iOffset, int jOffset){
		int playerModifier = currentPlayer ? -1 : 1; //If its player2 go in opposite direction
		iOffset *= playerModifier;
		jOffset *= playerModifier;
		if(inRange(i + iOffset, 0, 8) && inRange(j + jOffset, 0, 8) && board[i + iOffset][j + jOffset].empty()){  //checks if diagonal is on board and if the space is empty
			legalMoves.Add(board[i + iOffset][j + jOffset]);
		} else if(inRange(i + iOffset, 0, 8) && inRange(j + jOffset, 0, 8) && board[i + iOffset][j + jOffset].hasEnemy(currentPlayer) && inRange(i + (iOffset*2), 0, 8) && inRange(j + (jOffset*2), 0, 8) && board[i + (iOffset*2)][j + (jOffset*2)].empty()){ //checks if diagonal is on board and space is enemy, then if the next diagonal is on board and empty
			legalMoves.Add(board[i + (iOffset*2)][j + (jOffset*2)]); 
		}
	}

	bool inRange(int x, int min, int max){
		return x >= min && x < max; 
	}

	//get legal moves for a piece
	//get all legal moves for all pieces
	//display legal moves for a piece

	BoardLocation getLocation(string location){
		//char letter = location[0];
		int x = location[0] - 65;
		int z = location[1] - 49;
		return board[x][z];
	}

}
