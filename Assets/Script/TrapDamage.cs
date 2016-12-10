using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {

    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float damageInterval = 1f;
    private float lastDamageTime;
    [SerializeField]
    private int mana;
    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    void Start() {
        lastDamageTime = Time.time;
    }
    
    void OnTriggerStay(Collider collider) {
       if(collider.tag == "Mob"){
            if (Time.time > lastDamageTime + damageInterval){
                collider.gameObject.GetComponent<Mob>().takeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
    }
}
