using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStun : MonoBehaviour {
    public int time;
    public float stunTime;
    public float stunInterval;
    private float lastStunTime;
    [SerializeField]
	private int manaCost;
    public int ManaCost
    {
		get { return manaCost; }
		set { manaCost = value; }
    }

	void Awake(){
		manaCost = SkillConfig.TrapStun.manaCost;
		stunTime = SkillConfig.TrapStun.stunTime;
		stunInterval = SkillConfig.TrapStun.stunInterval;
	}


    void OnTriggerEnter(Collider collider)
    {

		if (collider.tag == "Mob" && !collider.isTrigger)
        {
            if (Time.time > lastStunTime + stunInterval)
            {
				collider.gameObject.GetComponent<Mob>().Stun(stunTime);
                lastStunTime = Time.time;
            }
        }
    }
}
