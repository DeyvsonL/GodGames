using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TileGround :  NetworkBehaviour
{

	public GameObject trap;
	public GameObject pillar;
    private GameObject previewPillar;
    private GameObject previewTrap;
		
	public void insertPillar(GameObject objectToSpawn, int time)
    {
		pillar = insertObject (objectToSpawn);
        Destroy(pillar, time);
    }

    public void insertPreviewPillar(GameObject objectToSpawn){
        if (pillar == null && previewPillar==null){
            previewPillar = insertObject(objectToSpawn);
        }
    }
    public void insertPreviewTrap(GameObject objectToSpawn){
        if (trap == null && previewTrap==null){
            previewTrap = insertObject(objectToSpawn);
        }
    }

    public void cleanPreview() {
        if (previewTrap != null) Destroy(previewTrap);
        if(previewPillar != null) Destroy(previewPillar);
    }

    public void insertTrap(GameObject objectToSpawn, int time){
        if (trap != null) return;
		trap = insertObject (objectToSpawn);
        //Destroy(trap, time);
	}

	private GameObject insertObject(GameObject objectToSpawn){
		float tileHeight = GetComponent<MeshFilter>().mesh.bounds.extents.y * transform.localScale.y;
		float trapHeight = objectToSpawn.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y * objectToSpawn.transform.localScale.y;

		Vector3 spawnPos = transform.position + new Vector3 (0, tileHeight + trapHeight + 0.1f, 0);

		GameObject objReturn = Instantiate (objectToSpawn, spawnPos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(objReturn);
        return objReturn;
    }

}
