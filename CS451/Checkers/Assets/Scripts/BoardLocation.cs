using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLocation {

	public Transform boardLocation;
	public GameObject piece;
	public int i;
	public int j;

	public BoardLocation (Transform _boardLocation, int _i, int _j){
		i = _i;
		j = _j;
		boardLocation = _boardLocation;
	}
	public BoardLocation (Transform _boardLocation, GameObject _piece, int _i, int _j){
		i = _i;
		j = _j;
		boardLocation = _boardLocation;
		piece = _piece;

	}

	public bool isEmpty(){
		return piece == null;
	}

	public bool hasEnemy(bool player){
		PieceHandler ph = piece.GetComponent<PieceHandler>(); 
		return ph.player != player;
	}
}
