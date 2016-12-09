using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GateToTheHell : MonoBehaviour {

    [SerializeField]
    private int qtdMobs = 10;
    Text text;

    private void Start(){
        text = GameObject.Find("MobsCountText").GetComponent<Text>();
        text.text = qtdMobs.ToString();
    }

    private void OnTriggerEnter(Collider other){
		if(!other.isTrigger && other.gameObject.tag == "Mob"){
            qtdMobs--;
            text.text = qtdMobs.ToString();
            Destroy(other.transform.parent.gameObject);
            if (qtdMobs <= 0){
                endGame();
            }
        }
    }

    private void endGame(){
        /*int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);*/
    }
}
