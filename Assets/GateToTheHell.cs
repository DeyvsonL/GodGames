using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateToTheHell : MonoBehaviour {

    [SerializeField]
    private int qtdMobs = 10;

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Mob"){
            qtdMobs--;
            Destroy(other.transform.parent.gameObject);
            if (qtdMobs <= 0){
                endGame();
            }
        }
    }

    private void endGame(){
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
