using UnityEngine;

public class CollisionStunBullet : MonoBehaviour {

    [SerializeField]
    private float stunTime = 1;

    void OnCollisionEnter(Collision collider) {
        if ((collider.gameObject.tag == "Mob")) {
            collider.gameObject.GetComponent<Mob>().Stun(stunTime);
        }
        if ((collider.gameObject.tag != "Player")) {
            Destroy(gameObject);
        }
    }
}
