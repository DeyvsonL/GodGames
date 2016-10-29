using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

	public float velMover;
	public float velMoverAr;
	public float forçaBalançar;

	private bool noChao;
	private bool pendurado;


	public float speed;
	public float jump;

	private int floorCount;

	private Rigidbody rigidbody;
	// Use this for initialization
	void Start ()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update ()
	{

		RaycastHit raio;
		noChao = Physics.Raycast (transform.position, -transform.up, out raio, 1.5f);

		pendurado = Hook.cordaColidiu;

		float h = Input.GetAxis ("Horizontal");
		float w = Input.GetAxis ("Vertical");


		Vector3 velocity = rigidbody.velocity;
		
		
		velocity.x = Input.GetAxis ("Horizontal") * speed;
		velocity.z = Input.GetAxis ("Vertical") * speed;


		if (floorCount > 0 && Input.GetButton ("Jump")){
//		if(floorCount == 0 && Input.GetButton("Jump")){ 
			Debug.Log("Entrou");
			transform.Translate(0, jump*Time.deltaTime,0);
			velocity.y = jump;
			noChao = false;
		}
		
//		if (Input.GetKey (KeyCode.LeftShift)) {
//			velocity.x = Input.GetAxis ("Horizontal") * speed * 1.6f;
//			velocity.z = Input.GetAxis ("Vertical") * speed * 1.6f;
//			Debug.Log ("Shift key was released.");
//		}
//
//
//
//		if (Input.GetKeyUp (KeyCode.LeftShift)) {
//			velocity.x = Input.GetAxis ("Horizontal") * speed;
//			velocity.z = Input.GetAxis ("Vertical") * speed;
//		}
//
//		rigidbody.velocity = velocity;


		if (floorCount > 0 ) {
			if (h != 0) {
				transform.Translate (h * velMover * Time.deltaTime, 0, 0);
			}

			if(w != 0){
				transform.Translate (0, 0, w*velMover*Time.deltaTime);

			}
		} else if (!noChao && pendurado) {
			GetComponent<Rigidbody> ().AddForce (transform.right * h * forçaBalançar);
		} else if (!noChao && !pendurado) {
			if (h != 0) {
				transform.Translate (h * velMoverAr * Time.deltaTime, 0, 0);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Floor"){
			floorCount++;

			noChao = true;
		}
		
//		if (other.gameObject.name == "Key")
//		{
////			hasKey = true;
//			other.transform.parent = transform;
//			//GameObject.Destroy(other.gameObject);
//		}
//		if (other.gameObject.name == "Gate" && hasKey)
//		{
//			SceneManager.LoadScene("Game");
//		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Floor")
			floorCount--;
	}

}