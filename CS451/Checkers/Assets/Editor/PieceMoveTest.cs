using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PieceMoveTest {
	private PieceMove pM1 ;
	private PieceMove pM2 ;

	[SetUp]
	protected void SetUp() {
		GameObject go = new GameObject ();
		BoardLocation bl = new BoardLocation (go.transform, 1, 1);
		BoardLocation bl2 = new BoardLocation (go.transform, 2, 2);
		pM1 = new PieceMove (bl);
		pM2 = new PieceMove (bl, bl2);
	}

	[Test]
	public void constructor() {
		Assert.IsNull (pM1.pieceTaken);
		Assert.IsFalse (pM1.kingPiece);
		Assert.IsNotNull (pM2.pieceTaken);
		Assert.IsFalse (pM2.kingPiece);
	}
}
