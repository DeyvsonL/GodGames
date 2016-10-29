using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    [SerializeField]
    private int type; //1 == damage, 2 == slow, 3 == stun

    private float velMover;
    private float velMoverAr;
    void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Player") {
            velMover = collider.gameObject.GetComponent<Movement>().velMover;
            velMoverAr = collider.gameObject.GetComponent<Movement>().velMoverAr;
            collider.gameObject.GetComponent<Movement>().velMover = collider.gameObject.GetComponent<Movement>().velMover / 2;
            collider.gameObject.GetComponent<Movement>().velMoverAr = collider.gameObject.GetComponent<Movement>().velMoverAr / 2;
        } else if (collider.tag == "Enemy") {
            //mudar depois
            velMover = collider.gameObject.GetComponent<Movement>().velMover;
            velMoverAr = collider.gameObject.GetComponent<Movement>().velMoverAr;
            collider.gameObject.GetComponent<Movement>().velMover = collider.gameObject.GetComponent<Movement>().velMover / 2;
            collider.gameObject.GetComponent<Movement>().velMoverAr = collider.gameObject.GetComponent<Movement>().velMoverAr / 2;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<Movement>().velMover = velMover;
            collider.gameObject.GetComponent<Movement>().velMoverAr = velMoverAr;
        } else if (collider.tag == "Enemy") {
            //mudar depois
            collider.gameObject.GetComponent<Movement>().velMover = velMover;
            collider.gameObject.GetComponent<Movement>().velMoverAr = velMoverAr;
        }
    }

}
