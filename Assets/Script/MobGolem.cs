using UnityEngine;
using UnityEngine.Networking;

public class MobGolem : Mob {
	public GameObject miniGolem;
	private Rigidbody body;

	void Start(){
		SimpleNavScript golemNavScript = GetComponent<SimpleNavScript> ();
		SimpleNavScript miniGolemNavScript = miniGolem.GetComponent<SimpleNavScript> ();
		if (golemNavScript && miniGolemNavScript) {
			miniGolemNavScript.possiblePaths = golemNavScript.possiblePaths;
		}
		body = GetComponentInChildren<Rigidbody> ();
	}

	override
	public void takeDamage(float damage){
		health -= damage;
		//print (gameObject.name + " newHealth:" + health);

		if (health <= 0) {
			//TODO: fix spawn position
			Vector3 position = transform.position;
			Vector3 randomVector1 = Random.insideUnitSphere;
			Vector3 randomVector2 = Random.insideUnitSphere;
			randomVector1.y = randomVector2.y = 0;

			GameObject spawnedObject = Instantiate (miniGolem, position + randomVector1, Quaternion.identity) as GameObject;
			//spawnedObject.GetComponentInChildren<Rigidbody> ().transform = position + randomVector1;
			NetworkServer.Spawn(spawnedObject);
			spawnedObject = Instantiate (miniGolem, position + randomVector2, Quaternion.identity) as GameObject;
			//spawnedObject.GetComponentInChildren<Rigidbody> ().transform = position + randomVector2;
			NetworkServer.Spawn(spawnedObject);
			Destroy (gameObject);
		}
	}

}
