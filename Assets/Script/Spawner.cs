using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEditor;

public class Spawner : NetworkBehaviour {

	public GameObject objectToSpawn;
	public int quantity;
	public float spawnInterval = 5f;
	public Transform[] possiblePaths;

	private int spawned;
	private float elapsed;

	public Wave[] waves;
	private int waveIndex;
	public static int count;
	private int waveSize;

	private int groupIndex;

	private int spawnIndex;
	private int spawnCnt;

	[System.Serializable]
	public struct Spawn {
		public GameObject objectToSpawn;
		public int quantity;
		public float spawnInterval;
	}

	[System.Serializable]
	public struct SpawnGroup {
		public Spawn[] objectsToSpawn;
		public bool randomize;
		public int loops;
		public float groupInterval;
	}

	[System.Serializable]
	public struct Wave {
		public SpawnGroup[] spawnGroups;
		public float waveInterval;
	}
		

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

        if (!isServer) {
            return;
        }

		if (spawned == quantity) {
			enabled = false;
		}

		elapsed += Time.deltaTime;
		if (elapsed > spawnInterval) {
			elapsed -= spawnInterval;
			spawned++;
			GameObject spawnedObject = Instantiate (objectToSpawn, gameObject.transform.position, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(spawnedObject);


        }
	}
}