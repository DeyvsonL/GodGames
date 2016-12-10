using UnityEngine;
using System.Collections;

public class TrapSlow : MonoBehaviour {

    [SerializeField]
    private float slow = 0.5f;
    [SerializeField]
    private int mana;
    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }

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
