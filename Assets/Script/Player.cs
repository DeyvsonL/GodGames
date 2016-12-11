using System.Collections;
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
    public bool Dead { get { return dead; } }

    Animator anim;

    void Start() {
        currentHealth = health;
        currentMana = mana;
        anim = GetComponentInChildren<Animator>();
        dead = false;
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
        dead = false;
    }

	public void takeDamage(float damage){
		currentHealth -= damage;
		if (currentHealth <= 0 && !dead) {
			currentHealth = 0;
			dead = true;
            StartCoroutine(Die());
            
        }
        else if(!dead){
            anim.SetTrigger("ReceiveHit");
        }
	}

    IEnumerator Die(){
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Death");
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().enabled = false;
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
