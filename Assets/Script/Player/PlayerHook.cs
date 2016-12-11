using UnityEngine;

public class PlayerHook : MonoBehaviour {

	public float launchSpeed;
	public float ropeLength;
	public float ropeForce;
	public float weight;

	private GameObject player;
	private GameObject target;
	private Rigidbody hookBody;
	private SpringJoint ropeEffect;
    private Rigidbody playerRigidBody;
    /* Hook pull direction is defined by weight difference between target and player
	 * 	target is pulled if targetWeight < playerWeight
	 * 	player is pulled if playerWeight < targetWeight
	 * 	hook is pulled if playerWeight == targetWeight
	 */
    private int hookPullDirection;
	private const int PULL_TARGET = 1;
	private const int PULL_PLAYER = -1;
	private const int PULL_HOOK = 0;
	private float pullSpeed = 25;

	private float playerDistance;

	private bool launchRope;
	public static bool ropeCollided;
    private LineRenderer lrRope;

    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		hookBody = GetComponent<Rigidbody>();
		ropeEffect = player.GetComponent<SpringJoint>();
        playerRigidBody = player.GetComponent<Rigidbody>();
        launchRope = true;
		ropeCollided = false;

        lrRope = GetComponent<LineRenderer>();
        lrRope.SetWidth(0.05f, 0.05f);
        lrRope.SetColors(Color.blue, Color.blue);

    }

    void Update () {
		playerDistance = Vector3.Distance(transform.position, player.transform.position);

		if(Input.GetMouseButtonDown(0)){
			launchRope = false;
		}

        if (launchRope) {
            LaunchHook();
        } else {
            RecallHook();
        }

        lrRope.SetPosition(0, transform.position);
        lrRope.SetPosition(1, player.transform.position);

    }
    
    void OnTriggerExit(Collider coll)
    {
        Debug.Log("EXIT " + coll.tag);
        if (coll.tag == "Pillar")
        {
            Destroy(coll);
        }
    }

    

    void OnTriggerEnter(Collider coll){
        if (coll.isTrigger) {
            return;
        }
		if(coll.tag != "Player" && coll.name != "Platform" && coll.name != "Floor"){
			target = coll.gameObject;
			Rigidbody targetRigidBody = target.GetComponent<Rigidbody> ();
			if (targetRigidBody && playerRigidBody) {
				if (targetRigidBody.mass < playerRigidBody.mass) {
					hookPullDirection = PULL_TARGET;
					gameObject.transform.SetParent (target.transform);
				} else if (targetRigidBody.mass > playerRigidBody.mass) {
					hookPullDirection = PULL_PLAYER;
				} else {
					hookPullDirection = PULL_HOOK;
				}
			} else {
				hookPullDirection = 0;
			}

			Debug.Log(coll.name);
			ropeCollided = true;
		}
	}

	public void LaunchHook(){
		if(playerDistance <= ropeLength){
			if(!ropeCollided){
				transform.Translate(0, 0, launchSpeed*Time.deltaTime);
			}

			else{
				ropeEffect.connectedBody = hookBody;
				ropeEffect.spring = ropeForce;
				ropeEffect.damper = weight;
			}
		}

		if(playerDistance > ropeLength){
			launchRope = false;
		}
	}

	public void RecallHook(){
		if (hookPullDirection == PULL_TARGET) {
			target.transform.position = Vector3.MoveTowards(target.transform.position, player.transform.position, pullSpeed*Time.deltaTime);
		} else if (hookPullDirection == PULL_PLAYER) {
			player.transform.position = Vector3.MoveTowards(player.transform.position, target.transform.position, pullSpeed*Time.deltaTime);
		} else if (hookPullDirection == PULL_HOOK) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, pullSpeed*Time.deltaTime);
		}

		ropeCollided = false;

		if(playerDistance <= 2){
            Destroy(gameObject);
            Destroy(gameObject);
        }
    }
} 