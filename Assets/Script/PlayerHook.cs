using UnityEngine;
using System.Collections;

public class PlayerHook : MonoBehaviour {

	public float velLançar;
	public float tamanhoCorda;
	public float forçaCorda;
	public float peso;

	private GameObject player;
	private GameObject target;
	private Rigidbody corpoRigido;
	private SpringJoint efeitoCorda;

	/* Hook pull direction is defined by weight difference between target and player
	 * 	target is pulled if targetWeight < playerWeight : hookPullDirection = 1
	 * 	player is pulled if playerWeight < targetWeight : hookPullDirection = -1
	 * 	hook is pulled if playerWeight == targetWeight : hookPullDirection = 0
	 */
	private int hookPullDirection;
	private float pullSpeed = 25;

	private float distanciaDoPlayer;

	private bool atirarCorda;
	public static bool cordaColidiu;
    private LineRenderer lrCorda;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		corpoRigido = GetComponent<Rigidbody>();
		efeitoCorda = player.GetComponent<SpringJoint>();

		atirarCorda = true;
		cordaColidiu = false;

        lrCorda = GetComponent<LineRenderer>();
        lrCorda.SetWidth(0.05f, 0.05f);
        lrCorda.SetColors(Color.blue, Color.blue);

    }

    // Update is called once per frame
    void Update () {
		distanciaDoPlayer = Vector3.Distance(transform.position, player.transform.position);

		if(Input.GetMouseButtonDown(0)){
			atirarCorda = false;
		}

        if (atirarCorda) {
            AtirarGancho();
        } else {
            RecolherGancho();
        }

        lrCorda.SetPosition(0, transform.position);
        lrCorda.SetPosition(1, player.transform.position);

    }
	void OnTriggerEnter(Collider coll){
		if(coll.tag != "Player" && coll.name != "Platform" && coll.name != "Floor"){
			target = coll.gameObject;
			Rigidbody targetRigidBody = target.GetComponent<Rigidbody> ();
			Rigidbody playerRigidBody = player.GetComponent<Rigidbody> ();
			if (targetRigidBody && playerRigidBody) {
				if (targetRigidBody.mass < playerRigidBody.mass) {
					hookPullDirection = 1;
					gameObject.transform.SetParent (target.transform);
				} else if (targetRigidBody.mass > playerRigidBody.mass) {
					hookPullDirection = -1;
				} else {
					hookPullDirection = 0;
				}
			} else {
				hookPullDirection = 0;
			}

			Debug.Log(coll.name);
			cordaColidiu = true;
		}
	}

	public void AtirarGancho(){
		if(distanciaDoPlayer <= tamanhoCorda){
			if(!cordaColidiu){
				transform.Translate(0, 0, velLançar*Time.deltaTime);
			}

			else{
				efeitoCorda.connectedBody = corpoRigido;
				efeitoCorda.spring = forçaCorda;
				efeitoCorda.damper = peso;
			}
		}

		if(distanciaDoPlayer > tamanhoCorda){
			atirarCorda = false;
		}
	}

	public void RecolherGancho(){
		if (hookPullDirection > 0) {
			target.transform.position = Vector3.MoveTowards(target.transform.position, player.transform.position, pullSpeed*Time.deltaTime);
		} else if (hookPullDirection < 0) {
			player.transform.position = Vector3.MoveTowards(player.transform.position, target.transform.position, pullSpeed*Time.deltaTime);
		} else {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, pullSpeed*Time.deltaTime);
		}

		cordaColidiu = false;

		if(distanciaDoPlayer <= 2){
			Destroy(gameObject);
		}
	}
} 