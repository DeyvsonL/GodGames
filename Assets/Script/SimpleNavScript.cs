using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

//

public class SimpleNavScript : NetworkBehaviour {
	public Transform[] possiblePaths;
	public float randomizedDistance = 0;

	private Transform path;
	private int pathIndex = 1; // Zero is the Path parent itself
	private int pathLength = 0;
	private Vector3 destination;
	private Transform[] waypoints;
	public float speed;

	private float distance;

	private Rigidbody body;
	private Vector3 currentVelocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		//agent.stoppingDistance = randomizedDistance/2 + 0.5f;

		int length = possiblePaths.Length;

		if (length == 0) {
			this.enabled = false;
			print (gameObject.name + " navscript disabled");
			return;
		}

		int pathChoice = Random.Range (0, length);
		path = possiblePaths [pathChoice];

		waypoints = path.GetComponentsInChildren<Transform> ();
		pathLength = waypoints.Length;

		body = GetComponentInChildren<Rigidbody> ();
		destination = body.position;
	}

	void LateUpdate () {
		if (!isServer) {
			return;
		}

		if (Reached ()) {
			if (pathIndex == pathLength) {
				this.enabled = false;
				return;
			}
			destination = waypoints [pathIndex].position;
			Vector3 randomized = Random.insideUnitCircle * randomizedDistance;
			randomized.y = 0;
			destination += randomized;
			//agent.SetDestination (destination);
			string message = string.Format ("{0} going from {1} to {2} {3}({4})\n",
				name, 
				transform.position,
				destination,
				waypoints [pathIndex].name,
				pathIndex);
			//print (message);
			pathIndex++;

		} else {
			// check if is stuck?
			// http://answers.unity3d.com/questions/396867/getting-navmeshagents-to-avoid-obstacles-effective.html

			Vector3 direction = destination - body.position;
			Vector3 targetPosition = body.position + direction * speed;
			Vector3 smoothedTargetPosition = Vector3.SmoothDamp (body.position, targetPosition, ref currentVelocity, 0.1f, speed, Time.fixedDeltaTime);
			Debug.DrawLine (body.position, targetPosition, Color.red);
			body.MovePosition (smoothedTargetPosition);

		}

	}

	bool Reached(){
		Vector3 from = body.position;
		from.y = 0;
		Vector3 to = destination;
		destination.y = 0;
		distance = Vector3.Distance (from, to);
		return distance <= 0.5f;
	}

}
