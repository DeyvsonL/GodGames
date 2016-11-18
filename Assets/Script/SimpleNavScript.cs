using UnityEngine;
using System.Collections;

public class SimpleNavScript : MonoBehaviour {
	public Transform[] possiblePaths;

	private NavMeshAgent agent;
	private Transform path;
	private int pathIndex = 1; // Zero is the Path parent itself
	private int pathLength = 0;
	private Vector3 destination;
	private Transform[] waypoints;

	private float distance;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();

		int length = possiblePaths.Length;

		if (length == 0)
			return;

		int pathChoice = Random.Range (0, length);
		path = possiblePaths [pathChoice];

		waypoints = path.GetComponentsInChildren<Transform> ();
		pathLength = waypoints.Length;

		destination = transform.position;
		agent.SetDestination (destination);
	}

	void LateUpdate () {
		if (pathIndex == pathLength)
			return;

		if (Reached ()) {
			destination = waypoints [pathIndex].position;
			agent.SetDestination (destination);
			string message = string.Format ("{0} going from {1} to {2} {3}({4})\n",
				name, 
				transform.position,
				destination,
				waypoints[pathIndex].name,
				pathIndex);
			//print (message);
			pathIndex++;
		}
	}

	bool Reached(){
		if (agent.hasPath){
			distance = Vector3.Distance (this.transform.position, agent.destination);
			return distance <= agent.stoppingDistance;
		}

		return true;
	}

}
