using UnityEngine;
using System.Collections;

public class TrapSlow : MonoBehaviour {

    [SerializeField]
    private float slow = 0.5f;

    void OnTriggerEnter(Collider collider){
        if(collider.tag == "Player") {
            collider.gameObject.GetComponent<PlayerMovement>().Slow = slow;
        } else if (collider.tag == "Mob"){
            //mudar depois
            collider.gameObject.GetComponent<SimpleNavScript>().ActualSpeed = slow;
        }
    }

    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<PlayerMovement>().Slow = slow;
        } else if (collider.tag == "Mob") {
            //mudar depois
            collider.gameObject.GetComponent<SimpleNavScript>().ActualSpeed = slow;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<PlayerMovement>().Slow = 1;
        } else if (collider.tag == "Mob") {
            //mudar depois
            collider.gameObject.GetComponent<SimpleNavScript>().ActualSpeed = 1;
        }
    }

}
