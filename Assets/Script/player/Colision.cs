using UnityEngine;

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
        if ((Colider.gameObject.tag == "Mob"))
        {
            gameObject.GetComponent<HP>().damage();
        }
    }
}
