using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStun : MonoBehaviour {
    public int time;
    public int timeStun;
    public float stunInterval;
    private float lastStunTime;
    [SerializeField]
    private int mana;
    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }


    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Mob")
        {
            if (Time.time > lastStunTime + stunInterval)
            {
                collider.gameObject.GetComponent<Mob>().Stun(timeStun);
                lastStunTime = Time.time;
            }
        }
    }
}
