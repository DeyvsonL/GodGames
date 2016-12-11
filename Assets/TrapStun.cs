using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStun : MonoBehaviour {
    public int time;
    public int timeStun;

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
            //mudar depois
            collider.gameObject.GetComponent<Mob>().Stun(timeStun);
        }
    }
}
