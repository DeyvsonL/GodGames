using UnityEngine;
using System.Collections;

public class CollisionPushBullet : MonoBehaviour {

	[SerializeField]
	private float pushForce = 50f;

	void OnCollisionEnter(Collision collider)
	{
		print (collider.gameObject.name);
		if ((collider.gameObject.tag == "Mob"))
		{
			Vector3 direction = GetComponent<Transform> ().forward;

			direction.y = 0;
			direction /= direction.magnitude;

			collider.gameObject.GetComponent<Rigidbody> ().AddForce (direction * pushForce);
		}
			
		if ((collider.gameObject.tag != "Player")) {
			Destroy(gameObject);
		}
	}
}
