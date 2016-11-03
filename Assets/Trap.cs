using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    [SerializeField]
    private int type; //1 == damage, 2 == slow, 3 == stun

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player") {
            collider.gameObject.GetComponent<Movement>().Slow = 0.5f;
        } else if (collider.tag == "Enemy"){
            //mudar depois
            collider.gameObject.GetComponent<Movement>().Slow = 0.5f;
        }
    }

    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<Movement>().Slow = 0.5f;
        } else if (collider.tag == "Enemy") {
            //mudar depois
            collider.gameObject.GetComponent<Movement>().Slow = 0.5f;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<Movement>().Slow = 1;
        } else if (collider.tag == "Enemy") {
            //mudar depois
            collider.gameObject.GetComponent<Movement>().Slow = 1;
        }
    }

}
