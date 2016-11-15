using UnityEngine;

public class ColisionBullet : MonoBehaviour
{
    [SerializeField]
    private int damage;

    void OnCollisionEnter(Collision collider)
    {
        if ((collider.gameObject.tag != "Player") && (collider.gameObject.tag == "Mob"))
        {
			collider.gameObject.GetComponent<Mob>().takeDamage(damage);
        }
        if (collider.gameObject.tag != "Player"){
            Destroy(gameObject);
        }
    }
}
