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
	[SerializeField]
    private float currentMana;

	private bool dead;

    void Start() {
        currentHealth = health;
        currentMana = mana;
    }

    override public void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        FreeLookCam flc = GameObject.FindGameObjectWithTag("CamController").GetComponent<FreeLookCam>();
        flc.Target = gameObject.transform;
        flc.gameStart();
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
