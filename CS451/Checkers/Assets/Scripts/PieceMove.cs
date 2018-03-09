using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove {

	public BoardLocation moveTo;
	public BoardLocation pieceTaken;
	public bool kingPiece = false;

	public PieceMove(BoardLocation _moveTo){
		moveTo = _moveTo;
	}

	public PieceMove(BoardLocation _moveTo, BoardLocation _pieceTaken){
		moveTo = _moveTo;
		pieceTaken = _pieceTaken;
	}

}
