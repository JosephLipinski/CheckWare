using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	List<List<BoardLocation>> board;
	List<PieceMove> currentLegalMoves;
	BoardLocation selectedPiece;
	public float smooth = 5.0f;
	bool currentPlayer = false;


	public GameObject blueChecker, blueKing, purpleChecker, purpleKing;

	// Use this for initialization
	void Start () {
		setupBoard();

	}
	
	// Update is called once per frame
	void Update () {
	}

	//Maps 2d char array to a 2d list of BoardLocations that make up the board
	void setupBoard() {

		char[][] boardSetup = new char[][] {
			new char[]{' ', '1', ' ', '1', ' ', '1', ' ', '1'}, //A
			new char[]{'1', ' ', '1', ' ', '1', ' ', '1', ' '}, //B
			new char[]{' ', '1', ' ', '1', ' ', '1', ' ', '1'}, //C
			new char[]{' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, //D
			new char[]{' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '}, //E
			new char[]{'2', ' ', '2', ' ', '2', ' ', '2', ' '}, //F
			new char[]{' ', '1', ' ', '2', ' ', '2', ' ', '2'}, //G
			new char[]{' ', ' ', ' ', ' ', ' ', ' ', '2', ' '}  //H
		};

		board = new List<List<BoardLocation>>();

		//maps each location on a 2d list to a BoardLocation object
		for(int i = 0; i < 8; i++){
			board.Add(new List<BoardLocation>());
			for(int j = 0; j < 8; j++){
				Transform child = transform.GetChild((i*8) + j);
				Vector3 spawnPosition = child.transform.position + new Vector3 (0f, 0.6f, 0f);
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

	//Looks through all board locations and sees if there are possible moves
	public bool hasLegalMoves(){
		bool hasLegalMove = false;
		foreach(Transform child in transform){
			List<PieceMove> locationMoves = getLegalMoves(child.name);
			if(locationMoves.Count > 0) hasLegalMove = true;
		}
		print(hasLegalMove);
		return hasLegalMove;
	}	

	//Looks at each diagonal movements and sees if it is a legal movement and adds it to a list of PieceMoves
	public List<PieceMove> getLegalMoves(string location){
		BoardLocation boardLocation = getLocation(location);
		List<PieceMove> legalMoves = new List<PieceMove>();
		if(!boardLocation.isEmpty()){ //if there is a piece at this location

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
			selectedPiece = boardLocation;
		}
		
		if(legalMoves.Count == 1){
			print("LEGAL LOCATION 1: ("+legalMoves[0].moveTo.i+", " +legalMoves[0].moveTo.j+")");
		}
		if(legalMoves.Count == 2){
			print("LEGAL LOCATION 1: ("+legalMoves[0].moveTo.i+", " +legalMoves[0].moveTo.j+")");
			print("LEGAL LOCATION 2: ("+legalMoves[1].moveTo.i+", " +legalMoves[1].moveTo.j+")");
		}

		currentLegalMoves = legalMoves;
		displayLegalMoves(legalMoves);
		return legalMoves;
	}

	//Moves piece to location. If it is an enemy it destroys the piece. If it moves to and edge of board, it kings the piece.
	public void movePiece(string location){
		PieceMove move = getMove(location); //Converts string to PieceMove
		if(move != null){ //ugly, but probably avoiding bugs 
			//Kills enemy piece
			if(move.pieceTaken != null){
				Destroy(move.pieceTaken.piece, 0.25f);
				move.pieceTaken.piece = null;
			}

			//Kings piece
			if(move.kingPiece && currentPlayer == false){
				selectedPiece.piece.GetComponent<PieceHandler>().instantiateKing = true;
			}		


			//Moves the piece
			selectedPiece.piece.GetComponent<PieceHandler>().target = move.moveTo.boardLocation.transform.position;	
			selectedPiece.piece.GetComponent<PieceHandler>().currentLerpTime = 0f;


			//Reassigns reference
			move.moveTo.piece = selectedPiece.piece;			
			selectedPiece.piece = null;


			
			
		}	
		//resets the board values.
		resetBoardDisplay();
		selectedPiece = null;
		currentLegalMoves.Clear();
	}

	//Checks if a location is empty.
	public bool isLocationEmpty(string location){
		BoardLocation boardLocation = getLocation(location);
		return boardLocation.isEmpty();
	} 

	//Checks if a location is part of the public property currentLegalMoves
	public bool isCurrentLegalMove(string location){	
		foreach(PieceMove move in currentLegalMoves){
			if(move.moveTo.boardLocation.name == location){
				return true;
			}
		}
		return false;
	}

	//Resets the color of the board
	public void resetBoardDisplay(){
		for(int i = 0; i < 8; i++){
			for(int j = 0; j < 8; j++){
				if(((i*8) + j+i) % 2 == 0){
					board[i][j].boardLocation.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;		
				} else {
					board[i][j].boardLocation.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;															
				}
			}
		}
		
	}

	//Makes selected location show all legal moves in green
	public void displayLegalMoves(List<PieceMove> moves){
		resetBoardDisplay();
		foreach(PieceMove move in moves){
			move.moveTo.boardLocation.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		}
	}

	//Populates the legalMoves reference with legal moves based on the diagonal direction put into the parameters.
	void calculateLegalMoves(ref int i, ref int j, ref List<PieceMove> legalMoves, ref PieceHandler ph, int iOffset, int jOffset){
		int playerModifier = currentPlayer ? -1 : 1; //If its player2 go in opposite direction
		iOffset *= playerModifier;
		jOffset *= playerModifier;
		if(inRange(i + iOffset, 0, 8) && inRange(j + jOffset, 0, 8) && board[i + iOffset][j + jOffset].isEmpty()){  //checks if diagonal is on board and if the space is empty
			legalMoves.Add(new PieceMove(board[i + iOffset][j + jOffset]));
			if(i+iOffset == 0 || i+iOffset == 7 && ph.king == false){
				legalMoves[legalMoves.Count-1].kingPiece = true;
			}
		} else if(inRange(i + iOffset, 0, 8) && inRange(j + jOffset, 0, 8) && board[i + iOffset][j + jOffset].hasEnemy(currentPlayer) && inRange(i + (iOffset*2), 0, 8) && inRange(j + (jOffset*2), 0, 8) && board[i + (iOffset*2)][j + (jOffset*2)].isEmpty()){ //checks if diagonal is on board and space is enemy, then if the next diagonal is on board and empty
			legalMoves.Add(new PieceMove(board[i + (iOffset*2)][j + (jOffset*2)], board[i + iOffset][j + jOffset])); 
			if(i + (iOffset*2) == 0 || i + (iOffset*2) == 7 && ph.king == false){
				legalMoves[legalMoves.Count-1].kingPiece = true;
			}
		}
	}

	bool inRange(int x, int min, int max){
		return x >= min && x < max; 
	}

	BoardLocation getLocation(string location){
		//char letter = location[0];
		int x = location[0] - 65;
		int z = location[1] - 49;
		return board[x][z];
	}

	PieceMove getMove (string location){
		BoardLocation boardLocation = getLocation(location);
		// PieceMove moveMatch;
		foreach (PieceMove move in currentLegalMoves){
			if(move.moveTo.Equals(boardLocation)){ //checks if its the same reference/pointer
				return move;
			}
		}
		return null;
	}

}
