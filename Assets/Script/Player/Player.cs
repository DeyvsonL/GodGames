using System.Collections;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class Player : MonoBehaviour
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
    private bool playedDead;
    private GateToTheHell gate;
    private GameManager gameManager;
    private bool animationWinStarted;


    public AudioClip soundDie;
    public float volSoundDie;
    public AudioClip soundDamage;
    public float volSoundDamage;
    public AudioClip soundMusicGame;
    public float volSoundMusicGame;
    private AudioSource source;

    void Start()
    {
        gameManager = GameManager.instance;
        
        maxHealth = PlayerConfig.maxHealth;
        maxMana = PlayerConfig.maxMana;

        currentHealth = maxHealth;
        currentMana = maxMana;
        anim = GetComponentInChildren<Animator>();
        dead = false;

        source = GetComponent<AudioSource>();

        source.PlayOneShot(soundMusicGame, volSoundMusicGame);
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animationWinStarted = false;

    }

    void Update()
    {


        if (gameManager.Win){
            if (!animationWinStarted){
                StartCoroutine(Win());
            }
            animationWinStarted = true;
            return;
        }
        if (dead) {
            if (!playedDead) {
                StartCoroutine(Die());
            }
            playedDead = true;
            return;
        }
		if (GameManager.instance.lose) { 
			Lose ();
		}
        fillMana(PlayerConfig.manaRegen * Time.deltaTime);
    }

    public void takeDamage(float damage)
    {

        if (dead || gameManager.Win) return;
        source.PlayOneShot(soundDamage, volSoundDamage);
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            currentHealth = 0;
            source.Stop();
            source.PlayOneShot(soundDie, volSoundDie);
            dead = true;
			GameManager.instance.LoseGame ();
        }
        else if (!dead)
        {
            anim.SetTrigger("ReceiveHit");
        }
    }

    IEnumerator Win(){
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Victory");
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator Die(){
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Death");
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().enabled = false;
    }

    public void fillHealth(float health){
 		if (!dead && !gameManager.Win) {
 			currentHealth += health;
 			if (currentHealth > maxHealth) {
 				currentHealth = maxHealth;
 			}
 		}
 	}

    public void takeMana(float manaCost)
    {
        if (dead || gameManager.Win) return;
        currentMana -= manaCost;
        if (currentMana <= 0)
        {
            currentMana = 0;
        }
    }

    public void fillMana(float mana)
    {
        if (dead || gameManager.Win) return;
        currentMana += mana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

	void Lose(){
		dead = true;
	}
}
