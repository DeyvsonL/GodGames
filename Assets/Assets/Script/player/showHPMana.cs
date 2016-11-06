using UnityEngine;
using System.Collections;

public class showHPMana : MonoBehaviour {
    [SerializeField]
    private Texture2D Hp;
    [SerializeField]
    private Texture2D ManaT;
    [SerializeField]
    private Texture2D painel;
    private GameObject player;
    private int maxHealth;
    private int maxMana;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void OnGUI()
    {
        if (player != null) {
            int health = player.GetComponent<HP>().Health;
            maxHealth = player.GetComponent<HP>().MaxHealth;

            int mana = player.GetComponent<Mana>().Mp;
            maxMana = player.GetComponent<Mana>().MaxMana;

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
        } else {
            int health = 0;

            int mana = 0;

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
}
