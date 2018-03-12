using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PieceHandler : NetworkBehaviour {

	// public string location; //Current location of the piece
	public bool king = false; //Is it a kinged piece?
	public bool instantiateKing = false; //Is it a kinged piece?
	public bool player = false; //Player 1 is false, Player 2 is 
	public float smooth = 5.0f;

	[SyncVar]
	public Vector3 target;
	public float currentLerpTime = 1.0f;
	public float lerpTime = 1.0f;

	// Use this for initialization
	void Start () {
		target = transform.position;	
	}

	// Update is called once per frame
	//void Update () {
	//transform.position =
	//lerps piece towards a target position
	//	currentLerpTime += Time.deltaTime;

	//	if(currentLerpTime > lerpTime){
	//		currentLerpTime = lerpTime;
	//	}
	//	float perc = currentLerpTime / lerpTime;

	//	if(currentLerpTime < lerpTime/4){
	//		target.y = transform.position.y + 1.5f;
	//	} else {
	//		target.y = transform.position.y;
	//	}
	//
	//	transform.position = Vector3.Lerp( transform.position, target, easeInOut(currentLerpTime));
	//}

	public float easeInOut(float t, float e = 2f)
	{
		return Mathf.Pow(t, e) / (Mathf.Pow(t, e) + Mathf.Pow(1f - t, e));
	}

	public void setPlayer(bool _player){
		player = _player;
	}

	public void Move(Vector3 moveTo)
	{
		transform.position = moveTo;

	}

	public void PrintMe()
	{
		Debug.Log ("I'm here");
	}

}
