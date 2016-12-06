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
	public float acceleration;

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

	void FixedUpdate () {
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
			direction.y = 0;
			direction.Normalize ();
			Vector3 targetPosition = body.position + direction * acceleration;

			if (body.velocity.magnitude < speed)
				body.AddForce (direction * acceleration * 10);
			else if (body.velocity.magnitude > speed)
				body.velocity = body.velocity.normalized * speed;
			
			//print(string.Format("velocity:{0}\ndir*speed:{1}\nvelMagnitude:{2}", body.velocity, direction*speed, body.velocity.magnitude));

			Debug.DrawLine (body.position, targetPosition, Color.red);

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
