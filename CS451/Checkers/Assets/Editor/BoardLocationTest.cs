using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BoardLocationTest {
	private BoardLocation BL1;
	private BoardLocation BL2;

	[SetUp]
	protected void SetUp() {
		Transform t = new GameObject().transform;
		BL1 = new BoardLocation (t, 1, 2);
		BL2 = new BoardLocation (t, new GameObject (), 1, 2);
	}

	[Test]
	public void BoardLocationConstructors() {
		// Use the Assert class to test conditions.
		Assert.AreEqual (null, BL1.piece);
		Assert.AreEqual (1, BL1.i);
		Assert.AreEqual (2, BL1.j);
		Assert.IsTrue (BL1.isEmpty ());
		Assert.AreNotEqual (null, BL2.piece);
		Assert.AreEqual (1, BL2.i);
		Assert.AreEqual (2, BL2.j);
		Assert.IsFalse (BL2.isEmpty ());
	}
}
