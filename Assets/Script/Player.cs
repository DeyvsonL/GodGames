using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class Player : NetworkBehaviour {
    public float health = 100;
    public float mana = 100;
    private Camera cam;
	public Camera Cam{
		get { return cam;}
	}
	[SerializeField]
    private float currentHealth;
	public float CurrentHealth {
		get{ return currentHealth; }
	}
	[SerializeField]
	private float currentMana;
	public float CurrentMana {
		get{ return currentMana; }
	}

	private bool dead;

    void Start() {
        currentHealth = health;
        currentMana = mana;
    }

    override public void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
		GameObject camController = GameObject.FindGameObjectWithTag ("CamController");
		FreeLookCam flc = camController.GetComponent<FreeLookCam>();
        flc.Target = gameObject.transform;
        flc.gameStart();
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        canvas.GetComponent<ShowHPMana>().Player = this;
		cam = Camera.main;
    }

	public void takeDamage(float damage){
		currentHealth -= damage;
		if (currentHealth <= 0) {
			currentHealth = 0;
			dead = true;
		}
	}
}
