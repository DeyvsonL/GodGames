using UnityEngine;
using System.Collections;

public class TileGround : Tile {

    [SerializeField]
    private bool hasTrap;
	[SerializeField]
	public GameObject pillar;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	public void insertPillar(GameObject objectToSpawn){
		float tileHeight = GetComponent<MeshFilter>().mesh.bounds.extents.y * transform.localScale.y;
		float pillarHeight = objectToSpawn.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y * objectToSpawn.transform.localScale.y;

		Vector3 spawnPos = transform.position + new Vector3 (0, tileHeight + pillarHeight + 0.1f, 0);
		pillar = Instantiate (objectToSpawn, spawnPos, Quaternion.identity) as GameObject;
	}

}
