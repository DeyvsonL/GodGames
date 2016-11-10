using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{

    // primeiro criaremos as variaveis 
    public Texture2D ManaT;
    public Texture2D painel;
    public int mp = 100;
    public int Mp
    {
        get { return mp; }
        set { mp = value; }
    }
    public int maxMana = 100;
    public int MaxMana
    {
        get { return maxMana; }
        set { maxMana = value; }
    }

    void Start()
    {

    }

    void Update()
    {
        /*
        // Area Para Eu Testar As Barras Nao e nescessario Copiar essa parte
        if (Input.GetKeyDown(KeyCode.Alpha1) && mp > 0)
        {
            mp = mp - 55;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && mp > 0)
        {
            mp = mp + 55;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && mp > 0)
        {
            mp = mp - 15;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && mp < maxMana)
        {
            mp = mp + 20;
        }

        if (mp > maxMana)
        {
            mp = maxMana;
        }
        if (mp < 0)
        {
            mp = 0;
        }
        */
    }
    /*
    void OnGUI()
    {
        ResolucaoMestre.AutoResize(1024, 768);
        GUI.BeginGroup(new Rect(150, 120, painel.width, painel.height));

        GUI.DrawTexture(new Rect(0, 0, ManaT.width * mp / maxMana, ManaT.height), ManaT);
        GUI.Label(new Rect(0, 0, 200, 100), "<Size=25>Mana " + mp + "/" + maxMana + "</Size>");
        GUI.DrawTexture(new Rect(0, 0, painel.width, painel.height), painel);

        GUI.EndGroup();
    }
    */
}
