﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// Nav Movement requires the GameObject to have a NavMeshAgent
[RequireComponent (typeof (NavMeshAgent))]
public class SimpleNavScript : NetworkBehaviour {
	public Transform[] possiblePaths;
	public float randomizedDistance = 0;

	private NavMeshAgent agent;
	private Transform path;
	private int pathIndex = 1; // Zero is the Path parent itself
	private int pathLength = 0;
	private Vector3 destination;
	private Transform[] waypoints;

	private float distance;

	// Use this for initialization
	void Start () {
        if (!isServer) {
            return;
        }
		agent = GetComponent<NavMeshAgent> ();
		agent.stoppingDistance = randomizedDistance/2 + 0.5f;

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
        if (!isServer) {
            return;
        }
        if (pathIndex == pathLength)
			return;

		if (Reached ()) {
			destination = waypoints [pathIndex].position;
			Vector3 randomized = Random.insideUnitCircle * randomizedDistance;
			randomized.y = 0;
			destination += randomized;
			agent.SetDestination (destination);
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
