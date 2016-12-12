using UnityEngine;

public class CollisionStunBullet : MonoBehaviour {

    [SerializeField]
    private float stunTime = 1;
    [SerializeField]
    private int damage;
    [SerializeField]
	private int manaCost;
    public int ManaCost
    {
		get { return manaCost; }
		set { manaCost = value; }
    }

	void Start(){
		damage = SkillConfig.StunBullet.damage;
		stunTime = SkillConfig.StunBullet.stunTime;
		manaCost = SkillConfig.StunBullet.manaCost;
	}

    void OnCollisionEnter(Collision collider) {
        if ((collider.gameObject.tag != "Player") && (collider.gameObject.tag == "Mob"))
        {
            collider.gameObject.GetComponent<Mob>().Stun(stunTime);
            collider.gameObject.GetComponentInParent<Mob>().takeDamage(damage);
        }

        if (collider.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
