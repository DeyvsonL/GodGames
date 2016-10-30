using UnityEngine;
using System.Collections;

public class showHPMana : MonoBehaviour {
    [SerializeField]
    private Texture2D Hp;
    [SerializeField]
    private Texture2D ManaT;
    [SerializeField]
    private Texture2D painel;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int health= player.GetComponent<HP>().Health;
        int maxHealth = player.GetComponent<HP>().MaxHealth;

        int mana = player.GetComponent<Mana>().Mp;
        int maxMana = player.GetComponent<Mana>().MaxMana;

        ResolucaoMestre.AutoResize(1024, 768);
        GUI.BeginGroup(new Rect(150, 50, painel.width, painel.height));

        GUI.DrawTexture(new Rect(0, 0, Hp.width * health / maxHealth, Hp.height), Hp);
        GUI.Label(new Rect(0, 0, 200, 100), "<Size=25>Hp " + health + "/" + maxHealth + "</Size>");
        GUI.DrawTexture(new Rect(0, 0, painel.width, painel.height), painel);

        GUI.EndGroup();


        ResolucaoMestre.AutoResize(1024, 768);
        GUI.BeginGroup(new Rect(150, 120, painel.width, painel.height));

        GUI.DrawTexture(new Rect(0, 0, ManaT.width * mana / maxMana, ManaT.height), ManaT);
        GUI.Label(new Rect(0, 0, 200, 100), "<Size=25>Mana " + mana + "/" + maxMana + "</Size>");
        GUI.DrawTexture(new Rect(0, 0, painel.width, painel.height), painel);

        GUI.EndGroup();
    }
}
