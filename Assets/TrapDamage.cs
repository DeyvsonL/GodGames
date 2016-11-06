using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {

    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float damageInterval = 1f;
    private float lastDamageTime;
    
    void OnTriggerEnter(Collider collider) {
        lastDamageTime = Time.time;
    }
    
    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player" || collider.tag == "Enemy") {
            if(Time.time > lastDamageTime + damageInterval) {
                collider.gameObject.GetComponent<HP>().damage(damage);
                lastDamageTime = Time.time;
            } else {
                Debug.Log(lastDamageTime + " " + (Time.time + damageInterval));
            }
            
        }
    }
    /*
    void OnTriggerExit(Collider collider) {
        if (collider.tag == "Player") {
            collider.gameObject.GetComponent<Movement>().Slow = 1;
        } else if (collider.tag == "Enemy") {
            //mudar depois
            collider.gameObject.GetComponent<Movement>().Slow = 1;
        }
    }
    */
}
