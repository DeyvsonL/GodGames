using UnityEngine;
using System.Collections;

public class ColisionBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collider)
    {
        if ((collider.gameObject.tag != "Player") && (collider.gameObject.tag == "Enemy"))
        {
            collider.gameObject.GetComponent<HP>().damage();
        }
        
        if ((collider.gameObject.tag != "Player"))
        {
            Destroy(gameObject);
        }
    }
}
