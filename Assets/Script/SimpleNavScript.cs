using UnityEngine;
using System.Collections;

public class SimpleNavScript : MonoBehaviour {
	public Transform[] possiblePaths;
	public float separationRadius = 3f;

	private NavMeshAgent agent;
	private Transform path;
	private int pathIndex = 0;
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
	}

	// Update is called once per frame


	void LateUpdate () {
		if (pathIndex == pathLength)
			return;

		if (Reached ()) {
			destination = waypoints [pathIndex].position;
			agent.SetDestination (destination);
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
