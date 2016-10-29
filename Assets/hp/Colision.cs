using UnityEngine;
using System.Collections;

public class Colision : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GetComponent<HP>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter (Collision Colider)
    {
        if (Colider.gameObject.tag == "wall")
        {
            gameObject.GetComponent<HP>().damage();
        }
        if ((Colider.gameObject.tag == "mob"))
        {
            Colider.gameObject.GetComponent<HP>().damage();
        }
    }
}
