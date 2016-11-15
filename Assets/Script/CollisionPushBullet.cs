using UnityEngine;
using System.Collections;

public class CollisionPushBullet : MonoBehaviour {

	void OnCollisionEnter(Collision collider){
		if ((collider.gameObject.tag != "Player")) {
			Destroy(gameObject);
		}
	}
}
