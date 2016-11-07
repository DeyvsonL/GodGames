using UnityEngine;
using UnityStandardAssets.Cameras;

public class Player : MonoBehaviour 
{ 
    void Awake()
    {
        FreeLookCam flc = GameObject.FindGameObjectWithTag("CamController").GetComponent<FreeLookCam>();
        flc.Target = gameObject.transform;
        flc.gameStart();
    }
}
