using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarExplosive : MonoBehaviour {

	[SerializeField]
	private int damage = 1;
	[SerializeField]
	private float radius = 5f;
	[SerializeField]
	private int manaCost;
	public int ManaCost
	{
		get { return manaCost; }
		set { manaCost = value; }
	}

	void Start() {
		damage = SkillConfig.ExplosivePillar.damage;
		radius = SkillConfig.ExplosivePillar.radius;
		manaCost = SkillConfig.ExplosivePillar.manaCost;
	}

	void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.tag == "Bullet")
		{
			Explode();
		}
	}

	public void Explode(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, radius);
		Mob mob;

		foreach (Collider c in colliders){
			if (c.gameObject.tag == "Mob") {
				mob = c.gameObject.GetComponent<Mob> ();
				mob.takeDamage (damage);
			}
		}
		Destroy(gameObject);
	}
}
