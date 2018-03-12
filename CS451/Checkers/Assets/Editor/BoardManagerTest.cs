using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class BoardManagerTest {
	private BoardManager BM1;
	private GameObject gO;

	/*
	 * Can't test any functions that call resetBoardDisplay due to meshRenderer expectations for children
	 */

	[SetUp]
	protected void SetUp() {
		gO = new GameObject ();
		for (int i = 0; i < 8; i++) {
			for(int j = 0; j < 8; j++){
				GameObject child = new GameObject ();
				MeshRenderer cMR = child.AddComponent<MeshRenderer> ();
				MeshFilter cMF = child.AddComponent<MeshFilter> ();
				child.transform.parent = gO.transform;
			}
		}
		BM1 = gO.AddComponent<BoardManager>();
		BM1.blueChecker = new GameObject ();
		BM1.purpleChecker = new GameObject ();
		BM1.setupBoard ();
	}

	[Test]
	public void constructor() {
		Assert.AreEqual (5.0f, BM1.smooth);
		Assert.IsTrue (BM1.currentPlayer);
		BM1.toggleCurrentPlayer ();
		Assert.IsFalse (BM1.currentPlayer);
	}

	[Test]
	public void toggle() {
		Assert.IsTrue (BM1.currentPlayer);
		BM1.toggleCurrentPlayer ();
		Assert.IsFalse (BM1.currentPlayer);
	}

//	[Test]
	// Function not in use
//	public void HasLegalMoves() {
//		Assert.IsTrue (BM1.hasLegalMoves());
//	}

	[Test]
	public void getLegalMoves() {
		// Calls getLocation
		// Calls calculateLegalMoves
		// Calls inRange
		// Calls displayLegalMoves
		// Calls resetBoardDisplay
		List<PieceMove> list = BM1.getLegalMoves (BM1.getLocation("A1"));
	}

	[Test]
	public void movePiece() {
		// Calls getMove
		// Calls resetBoardDisplay
		BM1.movePiece("A1");
	}

	[Test]
	public void isLocationEmpty() {
		// Calls getLocation
		Assert.IsTrue(BM1.isLocationEmpty("A1"));
		Assert.IsFalse(BM1.isLocationEmpty("A2"));
	}

	[Test]
	public void currentLegalMove() {
		// Calls getLocation
		Assert.IsFalse(BM1.isCurrentLegalMove("A2"));
	}
}
