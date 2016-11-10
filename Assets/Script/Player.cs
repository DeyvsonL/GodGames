using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Cameras;

public class Player : NetworkBehaviour 
{ 
    override public void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        FreeLookCam flc = GameObject.FindGameObjectWithTag("CamController").GetComponent<FreeLookCam>();
        flc.Target = gameObject.transform;
        flc.gameStart();
    }
}
