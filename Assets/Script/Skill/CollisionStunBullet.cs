using UnityEngine;

public class CollisionStunBullet : MonoBehaviour {

    [SerializeField]
    private float stunTime = 1;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int mana;
    public int Mana
    {
        get { return mana; }
        set { mana = value; }
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
