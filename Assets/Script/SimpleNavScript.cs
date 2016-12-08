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
	private NavMeshAgent agent;
	private NavMeshPath agentPath;

	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		agent = GetComponentInChildren<NavMeshAgent> ();
		agent.updatePosition = false;
		agent.updateRotation = false;
		agent.avoidancePriority = Random.Range (1, 99);
		agentPath = new NavMeshPath ();
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
		if (!isServer || !IsOnGround()) {
			return;
		}

		NavMeshHit myDestinationHit, myPositionHit;
		bool canReachDestination = NavMesh.SamplePosition (destination, out myDestinationHit, 2f, NavMesh.AllAreas);

		if (Reached () || (!canReachDestination && pathIndex != pathLength-1)) {
			if (pathIndex == pathLength) {
				agent.enabled = false;
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

			//agent.CalculatePath (destination, agentPath);

			agent.SetDestination (destination);

			Vector3 direction = destination - body.position;
			direction.y = 0;
			direction.Normalize ();
			direction = agent.desiredVelocity.normalized;
			//print (direction + " " + agent.path.status + " " + agent.CalculatePath (destination, agentPath));
			agent.nextPosition = body.position;

			Vector3 targetPosition = body.position + body.velocity;
			RaycastHit frontRay, leftRay, rightRay;
			bool hasHitFront, hasHitLeft, hasHitRight;

			hasHitFront = Physics.SphereCast (body.position, 0.5f, body.velocity, out frontRay, 1f);
			Debug.DrawLine (body.position, body.position + body.velocity.normalized, Color.black);

			if (hasHitFront) {
				Vector3 leftDirection = RotateLeft (direction);
				Vector3 rightDirection = RotateRight (direction);
				hasHitLeft = Physics.SphereCast (body.position, 0.5f, leftDirection, out leftRay, 1f);
				hasHitRight = Physics.SphereCast (body.position, 0.5f, rightDirection, out rightRay, 1f);

				if (hasHitLeft && hasHitRight) {
					if (leftRay.distance > rightRay.distance) {
						direction = leftDirection;
					} else {
						direction = rightDirection;
					}
				} else if (hasHitLeft) {
					direction = rightDirection;
				} else {
					direction = leftDirection;
				}
			} 

			if (body.velocity.magnitude < speed)
				body.AddForce (direction * acceleration * 10);
			else if (body.velocity.magnitude > speed)
				body.velocity = body.velocity.normalized * speed;
			
			//print(string.Format("velocity:{0}\ndir*speed:{1}\nvelMagnitude:{2}", body.velocity, direction*speed, body.velocity.magnitude));

			Debug.DrawLine (body.position, targetPosition, Color.red);

			currentVelocity = body.velocity;

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

	bool IsOnGround(){
		return Mathf.Abs (body.velocity.y) < 0.1f;
	}

	Vector3 RotateLeft(Vector3 vector){
		return Quaternion.Euler (0, -45, 0) * vector;
	}

	Vector3 RotateRight(Vector3 vector){
		return Quaternion.Euler (0, 45, 0) * vector;
	}
		
	bool RandomBoolean(){
		return Random.value > 0.5f;
	}
}
