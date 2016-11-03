using UnityEngine;
using System.Collections;

public class ColisionBullet : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<HP>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision Colider)
    {
        if ((Colider.gameObject.tag != "Player") && (Colider.gameObject.tag == "Mob"))
        {
            Colider.gameObject.GetComponent<HP>().damage();
        }

        if ((Colider.gameObject.tag != "Player"))
        {
            Destroy(gameObject);
        }
    }
    
}
