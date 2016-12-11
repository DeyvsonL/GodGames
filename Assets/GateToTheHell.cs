using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GateToTheHell : MonoBehaviour {

    [SerializeField]
    private int qtdMobs = 10;
    public int QtdMobs {
        get { return qtdMobs; }
    }
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
                text.text = "0";
            }
        }
    }
}
