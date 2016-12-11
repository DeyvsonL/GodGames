using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour {
	public GameObject fountainHealParticlePrefab;

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Transform fountainHeal = other.transform.Find (fountainHealParticlePrefab.name);
			if (fountainHeal) {
				fountainHeal.gameObject.SetActive (true);
			} else {
				print ("fountain heal not found on player!");
			}
		}
	}

	void OnTriggerStar(Collider other){
		if (other.tag == "Player") {
			Player player = other.GetComponent<Player> ();
			player.fillMana (EnvConfig.Fountain.manaRegen);
			player.fillHealth (EnvConfig.Fountain.healthRegen);
		}
	}
		

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			Transform fountainHeal = other.transform.Find (fountainHealParticlePrefab.name);
			if (fountainHeal) {
				fountainHeal.gameObject.SetActive (true);
			}
		}
	}
}
