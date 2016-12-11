using UnityEngine;

public class ColisionBullet : MonoBehaviour
{
    [SerializeField]
    private int damage;

    void OnCollisionEnter(Collision collider)
    {
        if ((collider.gameObject.tag != "Player") && (collider.gameObject.tag == "Mob"))
        {
			collider.gameObject.GetComponentInParent<Mob>().takeDamage(damage);
			//print ("bala colidiu com " + collider.gameObject.name);
        }

        //TO DO - Destruir quando todar no player
        if (collider.gameObject.tag != "Player"){
            Destroy(gameObject);
        }
    }
}
