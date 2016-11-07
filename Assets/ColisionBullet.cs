using UnityEngine;
using System.Collections;

public class ColisionBullet : MonoBehaviour
{
    [SerializeField]
    private int damage;

    void OnCollisionEnter(Collision collider)
    {
        if ((collider.gameObject.tag != "Player") && (collider.gameObject.tag == "Mob"))
        {
            collider.gameObject.GetComponent<HP>().damage(damage);
        }
        if ((collider.gameObject.tag != "Player")){
            Destroy(gameObject);
        }
    }
}
