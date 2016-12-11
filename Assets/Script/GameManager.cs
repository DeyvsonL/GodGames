using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	//private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	//private int level = 3;                                  //Current level number, expressed in game as "Day 1".
	public GameObject mobKilledCountText;
	public int mobsKilled = 0;
	public int mobsSpawned = 0;
	public int mobsDestroyed = 0;

	//Awake is always called before any Start functions
	void Awake(){
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);


		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame(){


	}
		
	//Update is called every frame.
	void Update(){

	}

	public void countMobSpawned(){
		mobsSpawned++;
	}

	public void countMobKilled(){
		mobsKilled++;
		if (mobKilledCountText) {
			mobKilledCountText.GetComponent<Text> ().text = ""+mobsKilled;
		}
	}

	public void countMobDestroyed(){
		mobsDestroyed++;
	}
}
	
