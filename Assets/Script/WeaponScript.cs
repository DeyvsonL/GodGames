using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
	public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

	    if (other.tag == "Mob")
	    {
	        other.GetComponent<Mob>().takeDamage(damage);
	    }
	}

	void OnCollisionEnter(Collision collision){
		print ("collision");

	}
}
