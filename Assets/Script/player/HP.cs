using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour {

    [SerializeField]
    private int health = 100;
    public int Health {
        get { return health; }
        set { health = value; }
    }
    [SerializeField]
    private int maxHealth = 100;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
	
	void Update () {
        if (health <= 0) 
        {
            
            if(gameObject.tag == "Player") {
                gameObject.SetActive(false);
                Invoke("Reload", 5);
            } else {
                Destroy(gameObject);
            }
        }
        
    }

    public void Reload() {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void damage(int damage) {
        health -= damage;
    }

    public void damage()
    {
        health= health-50;
    }
}
