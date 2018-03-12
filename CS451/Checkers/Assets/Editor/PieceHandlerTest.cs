using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PieceHandlerTest {
	private PieceHandler pH ;

	[SetUp]
	protected void SetUp() {
		pH = new PieceHandler ();
	}

	[Test]
	public void setPlayer() {
		Assert.IsFalse (pH.player);
		pH.setPlayer (true);
		Assert.IsTrue (pH.player);
	}

	[Test]
	public void easeInOut() {
		Assert.AreEqual (Mathf.Pow (1f, 2f) / (Mathf.Pow (1f, 2f) + Mathf.Pow (1f - 1f, 2f)), pH.easeInOut (1f));
		Assert.AreEqual (Mathf.Pow (1f, 3f) / (Mathf.Pow (1f, 3f) + Mathf.Pow (1f - 1f, 3f)), pH.easeInOut (1f, 3f));
	}
}
