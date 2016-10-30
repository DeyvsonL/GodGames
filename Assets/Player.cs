using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float velMover;
	public float velMoverAr;
	public float forçaBalançar;

	private bool noChao;
	private bool pendurado;


    public float speed;
    public float jump;

    private int floorCount;

    public bool hasKey;

    

    //    private Rigidbody rigidbody;

    void Awake()
    {
//        rigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate () {

		RaycastHit raio;
		noChao = Physics.Raycast(transform.position, -transform.up, out raio, 1.5f);

		pendurado = Hook.cordaColidiu;

		float h = Input.GetAxis("Horizontal");
		float w = Input.GetAxis("Vertical");
		if(noChao){
			if(h != 0){
				transform.Translate(h*velMover*Time.deltaTime, 0, w*velMover*Time.deltaTime);
			}
		}







//        Vector3 velocity = rigidbody.velocity;
//
//
//        velocity.x = Input.GetAxis("Horizontal")*speed;
//		velocity.z = Input.GetAxis("Vertical")*speed;

		if(floorCount >0){
			transform.Translate(h*velMover*Time.deltaTime, 0, 0);
		}
		else if(floorCount>0 && pendurado){
			GetComponent<Rigidbody>().AddForce(transform.right*h*forçaBalançar);
		}

		else if(floorCount>0 && !pendurado){
			if(h != 0){
				transform.Translate(h*velMoverAr*Time.deltaTime, 0, 0);
			}
		}


//        if (floorCount > 0 && Input.GetButton("Jump"))
//            velocity.y = jump;
//
//
//		if(Input.GetKey(KeyCode.LeftShift)){
//			velocity.x = Input.GetAxis("Horizontal")*speed*1.6f;
//			velocity.z = Input.GetAxis("Vertical")*speed*1.6f;
//			Debug.Log( "Shift key was released." );
//		}



//		if(Input.GetKeyUp(KeyCode.LeftShift)){
//			velocity.x = Input.GetAxis("Horizontal")*speed;
//			velocity.z = Input.GetAxis("Vertical")*speed;
//		}
        
//        rigidbody.velocity = velocity;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
            floorCount++;
        if (other.gameObject.name == "Key")
        {
            hasKey = true;
            other.transform.parent = transform;
            //GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.name == "Gate" && hasKey)
        {
            SceneManager.LoadScene("Game");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Floor")
            floorCount--;
    }
}
