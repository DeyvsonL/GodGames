using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	//private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	//private int level = 3;                                  //Current level number, expressed in game as "Day 1".
	public GameObject mobKilledCanvas;
	private Text mobKilledText;

	public GameObject portalHealthCanvas;
	private Text portalHealthText;

	public int mobsKilled = 0;
	public int mobsSpawned = 0;
	public int mobsDestroyed = 0;
	public int portalHealth = 10;

    private bool win;
    public bool Win { get { return win; } }

    //Awake is always called before any Start functions
	void Awake(){
        win = false;
        //Check if instance already exists
        if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
//		DontDestroyOnLoad(gameObject);


		//Call the InitGame function to initialize the first level 
		InitGame();

		mobKilledText = mobKilledCanvas.GetComponent<Text> ();
		portalHealthText = portalHealthCanvas.GetComponent<Text> ();
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
		if (mobKilledText) {
			mobKilledText.text = mobsKilled.ToString();
		}
	}

	public void DamagePortal(int damage){
		portalHealth -= damage;
		if (portalHealth < 0)
			portalHealth = 0;

		if (portalHealthText) {
			portalHealthText.text = portalHealth.ToString();
		}
	}

	public void countMobDestroyed(){
		mobsDestroyed++;
	}

	public void winGame(){
        win = true;
	}
}
	
