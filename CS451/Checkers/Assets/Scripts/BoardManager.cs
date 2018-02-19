using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
	ArrayList bluePositions = new ArrayList(new string[] {"A1","A3","A5","A7","B2","B4","B6","B8","C1","C3","C5","C7"});
	ArrayList purplePositions = new ArrayList(new string[] {"F2","F4","F6","F8","G1","G3","G5","G7","H2","H4","H6","H8"});

	public GameObject blueChecker, blueKing, purpleChecker, purpleKing;


	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			if (bluePositions.Contains (child.name)) {
				Vector3 spawnPosition = child.transform.position + new Vector3 (0f, 1f, 0f);
				Instantiate (blueChecker, spawnPosition, Quaternion.identity);

			}
			else if(purplePositions.Contains(child.name)){
				Vector3 spawnPosition = child.transform.position + new Vector3 (0f, 1f, 0f);
				Instantiate (purpleChecker, spawnPosition, Quaternion.identity);			
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
