using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour {

	public Transform weapon;
	private Animator attackAnimator;

	// Use this for initialization
	void Start () {
		//weapon = transform.Find ("Weapon");
		attackAnimator = weapon.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			attackAnimator.SetTrigger("basicHit");
		}else if(Input.GetButtonDown("Fire2")){
			print("Attack2");
			attackAnimator.SetTrigger("combo1");
		}
		if(attackAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")){
			print("basicHit");
		}else if(attackAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")){
			print("combo2");
		}
		//			attackAnimator.SetTrigger("basicHit");
		//			attackAnimation.Play ();
		//			Animator animator = weapon.GetComponent<Animator> ();
		//			animator.Play ();
		//			animation.Play ("AttackAnimation");

	}


	void FixedUpdate (){
		
		
	}
}
