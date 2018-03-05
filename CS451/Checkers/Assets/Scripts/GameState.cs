using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameState : NetworkBehaviour {
	public enum state {noPlayers, onePlayer, readyToGo, Done} ;
	public static state myState;



	// Use this for initialization
	void Start () {
		myState = state.noPlayers;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlayerConnected()
	{
		print (myState.ToString ());

		if (myState == state.noPlayers) {
			myState = state.onePlayer;
		}
		else{
			myState = state.readyToGo;
		}

		print (myState.ToString ());
	}
}
