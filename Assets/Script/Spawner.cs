using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEditor;

public class Spawner : NetworkBehaviour {

	public Transform[] possiblePaths;

	private float elapsed;

	public Wave[] waves;
	private int waveIndex;
	public static int count;
	private int waveSize;

	private int groupIndex;
	private SpawnGroup[] groups;

	private Spawn[] spawns;
	private int spawnIndex;
	private int spawnCnt;

	private float waveInterval;
	private float groupInterval;
	private float spawnInterval;
	private GameObject spawnObject;
	private int spawnLimit;

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
		elapsed = 0;
		spawnCnt = 0;
		GetCurrentSpawn ();
	}

	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}
			
		if (waveIndex == waves.Length) {
			this.enabled = false;
			return;
		}

		if (groupIndex == waves [waveIndex].spawnGroups.Length) {
			elapsed += Time.deltaTime;
			if (elapsed > waveInterval) {
				elapsed -= waveInterval;
				waveIndex++;
				groupIndex = 0;
				spawnIndex = 0;
				spawnCnt = 0;
				GetCurrentSpawn ();
			}
		} else {
			if (spawnIndex == waves [waveIndex].spawnGroups [groupIndex].objectsToSpawn.Length) {
				elapsed += Time.deltaTime;
				if (elapsed > groupInterval) {
					elapsed -= groupInterval;
					groupIndex++;
					spawnIndex = 0;
					spawnCnt = 0;
					GetCurrentSpawn ();
				}
			} else {
				if (spawnCnt == spawnLimit) {
					spawnIndex++;
					spawnCnt = 0;
					GetCurrentSpawn ();
				} else {
					elapsed += Time.deltaTime;

					if (elapsed > spawnInterval) {
						elapsed -= spawnInterval;
						spawnCnt++;
						GameObject spawnedObject = Instantiate (spawnObject, gameObject.transform.position, Quaternion.identity) as GameObject;
						NetworkServer.Spawn (spawnedObject);
					}
				}
			}
		}

	}

	private void GetCurrentSpawn(){
		if (waveIndex == waves.Length 
			|| groupIndex == waves [waveIndex].spawnGroups.Length
			|| spawnIndex == waves [waveIndex].spawnGroups [groupIndex].objectsToSpawn.Length)
			return;
		
		spawnObject = waves [waveIndex].spawnGroups [groupIndex].objectsToSpawn [spawnIndex].objectToSpawn;
		spawnLimit = waves [waveIndex].spawnGroups [groupIndex].objectsToSpawn [spawnIndex].quantity;
		spawnInterval = waves [waveIndex].spawnGroups [groupIndex].objectsToSpawn [spawnIndex].spawnInterval;

		groupInterval = waves [waveIndex].spawnGroups [groupIndex].groupInterval;
		waveInterval = waves [waveIndex].waveInterval;

		SimpleNavScript navScript = spawnObject.GetComponent<SimpleNavScript> ();
		if (navScript) {
			navScript.possiblePaths = possiblePaths;
		}
	}

}