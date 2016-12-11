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

    Animator anim;

    void Start() {
        currentHealth = health;
        currentMana = mana;
        anim = GetComponentInChildren<Animator>();
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
		if (currentHealth <= 0 && !dead) {
			currentHealth = 0;
			dead = true;
            anim.SetTrigger("Death");
        }
        else if(!dead){
            anim.SetTrigger("ReceiveHit");
        }
	}

    public void takeMana(float manaCost)
    {
        currentMana -= manaCost;
        if (currentMana <= 0)
        {
            currentMana = 0;
            dead = true;
        }
    }
}
