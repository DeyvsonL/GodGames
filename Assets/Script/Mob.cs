﻿using UnityEngine;

public class Mob : MonoBehaviour {

    private bool stunned = false;

    private float stunTime = 0;

	private float health = 10;

	[SerializeField]
	private float damage = 20;

    void Update () {
        if (stunCount())
            return;
        //se não estiver estunado, fazer o resto das ações abaixo

	}

    private bool stunCount() {
        if (stunned) {
            stunTime -= Time.deltaTime;
            if (stunTime <= 0) {
                stunned = false;
            }
        }
        return stunned;
    }

    public void Stun(float time) {
        stunned = true;
        stunTime = time;
    }

	public void takeDamage(float damage){
		health -= damage;
	}

	void OnCollisionEnter(Collision collider){
		if ((collider.gameObject.tag == "Player"))
		{
			collider.gameObject.GetComponent<Player>().takeDamage(damage);
		}
	}

}
