﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject objectToSpawn;
	public int quantity;
	public float spawnInterval = 5f;
	public Transform[] possiblePaths;

	private int spawned;
	private float elapsed;

	// Use this for initialization
	void Start () {
		spawned = 0;
		elapsed = 0;

		SimpleNavScript navScript = objectToSpawn.GetComponent<SimpleNavScript> ();
		if (navScript) {
			navScript.possiblePaths = possiblePaths;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spawned == quantity) {
			enabled = false;
		}

		elapsed += Time.deltaTime;
		if (elapsed > spawnInterval) {
			elapsed -= spawnInterval;
			spawned++;
			GameObject spawnedObject = Instantiate (objectToSpawn, gameObject.transform.position, Quaternion.identity) as GameObject;


		}
	}
}
