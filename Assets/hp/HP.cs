using UnityEngine;
using System.Collections;

public class HP : MonoBehaviour {

    // primeiro criaremos as variaveis 
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
    [SerializeField]
    private GameObject target;

    void Start () {
	
	}
	
	void Update () {
        /*
        // Area Para Eu Testar As Barras Nao e nescessario Copiar essa parte
        if (Input.GetKeyDown(KeyCode.D) && health > 0)
        {
            health = health - 55;
        }
        if (Input.GetKeyDown(KeyCode.S) && health < maxHealth)
        {
            health = health + 5;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            maxHealth = maxHealth + 10;
        }
        */
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void damage()
    {
        health= health-50;
    }
    /*
    void OnGUI()
	{
		ResolucaoMestre.AutoResize (1024,768);
		GUI.BeginGroup (new Rect(150,50,painel.width,painel.height));

		GUI.DrawTexture (new Rect(0,0,Hp.width * health / maxHealth, Hp.height),Hp);
		GUI.Label (new Rect(0,0,200,100),"<Size=25>Hp "+ health + "/"+ maxHealth + "</Size>");
		GUI.DrawTexture (new Rect(0,0,painel.width,painel.height), painel);

		GUI.EndGroup ();
	}
    */
}
