using UnityEngine;
using System.Collections;

public class TrapDamage : MonoBehaviour {

    [SerializeField]
    private int damage = 1;
    [SerializeField]
    public float damageInterval;
    private float lastDamageTime;
    [SerializeField]
    private int mana;
    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public int time;
    

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
