using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class Player : NetworkBehaviour
{
    public float maxHealth = 100;
    public float maxMana = 100;
    private Camera cam;
    public Camera Cam
    {
        get { return cam; }
    }
    [SerializeField]
    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }
    [SerializeField]
    private float currentMana;
    public float CurrentMana
    {
        get { return currentMana; }
    }

    private bool dead;
    public bool Dead { get { return dead; } }

    private Animator anim;
    private NetworkManager networkManager;
    private bool playedDead;
    private GateToTheHell gate;
    void Start()
    {
        maxHealth = PlayerConfig.maxHealth;
        maxMana = PlayerConfig.maxMana;

        currentHealth = maxHealth;
        currentMana = maxMana;
        anim = GetComponentInChildren<Animator>();
        dead = false;
    }

    override public void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GameObject camController = GameObject.FindGameObjectWithTag("CamController");
        FreeLookCam flc = camController.GetComponent<FreeLookCam>();
        flc.Target = gameObject.transform;
        flc.gameStart();
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvas.GetComponent<ShowHPMana>().Player = this;
        cam = Camera.main;
        dead = false;
        playedDead = false;
        gate = GameObject.Find("GateToTheHell").GetComponent<GateToTheHell>();
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if (networkManager == null) networkManager = GameObject.Find("LobbyManager").GetComponent<NetworkManager>();
    }

    void Update()
    {
        if (dead)
        {
            if (!playedDead) StartCoroutine(Die());
            playedDead = true;
            if (Input.GetButtonDown("EndGame"))
            {
                Debug.Log("Enter");
                networkManager.StopHost();
            }
            else
            {
                return;
            }
            return;
        }
        if (gate.QtdMobs <= 0) dead = true;
        fillMana(PlayerConfig.manaRegen * Time.deltaTime);
    }

    public void takeDamage(float damage)
    {
        if (dead) return;
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            currentHealth = 0;
            dead = true;
        }
        else if (!dead)
        {
            anim.SetTrigger("ReceiveHit");
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Death");
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().enabled = false;
    }

    public void fillHealth(float health){
 		if (!dead) {
 			currentHealth += health;
 			if (currentHealth > maxHealth) {
 				currentHealth = maxHealth;
 			}
 		}
 	}

    public void takeMana(float manaCost)
    {
        if (dead) return;
        currentMana -= manaCost;
        if (currentMana <= 0)
        {
            currentMana = 0;
            dead = true;
        }
    }

    public void fillMana(float mana)
    {
        if (dead) return;
        currentMana += mana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
}
