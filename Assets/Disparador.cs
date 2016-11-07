using UnityEngine;
using UnityEngine.UI;

public class Disparador : MonoBehaviour
{
    private const string FIRE1 = "Fire1";
    private const string FIRE2 = "Fire2";
    private GameObject auxGancho;
    
    public float velocityBullet;
    private Camera m_camera;

	public Transform dirDoClique;
	private Transform auxDirDoClique;
    [SerializeField]
    private Transform rightHand;

	public float viewRange = 60f;

	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	float rotVertical = 0;

	private LineRenderer lrMark;
	private GameObject mark;
	private LineRenderer lrGancho;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject bulletStun;
    [SerializeField]
    private GameObject hook;
    [SerializeField]
	private GameObject pillarToSpawn;
    [SerializeField]
	private GameObject trapSlow;
    [SerializeField]
    private GameObject trapDamage;


    public Button[] gameObjectsSkill;

    public int skill;
    private readonly int BULLET = 1;
    private readonly int PILLAR = 2;
    private readonly int TRAP = 3;
    private readonly int HOOK = 4;
    public int Skill
    {
        get { return Skill; }
    }

    public void alterSkill(int newSkill)
    {
        skill = newSkill;
    }

    void Start(){
        m_camera = Camera.main;
        if (m_camera != null)
            Debug.Log("Camera encontrada");
        lrMark = GetComponent<LineRenderer>();
        lrMark.SetWidth(0.05f, 0.05f);
        lrMark.SetColors(Color.red, Color.red);
        mark = GameObject.FindGameObjectWithTag("mark");
        skill = 1;
        GameObject[] go = GameObject.FindGameObjectsWithTag("BtnSkill");
        gameObjectsSkill = new Button[go.Length];
        for (int i = 0; i<go.Length; i++)
        {
            gameObjectsSkill[i] = go[i].GetComponent<Button>();
        }

        for (int i = 0; i < gameObjectsSkill.Length-1; i++)
        {
            for(int j=i+1;j< gameObjectsSkill.Length; j++)
            {
                if (string.Compare( gameObjectsSkill[j].gameObject.name,  gameObjectsSkill[i].gameObject.name, false) < 0)
                {
                    Button goAux = gameObjectsSkill[j];
                    gameObjectsSkill[j] = gameObjectsSkill[i];
                    gameObjectsSkill[i] = goAux;

                }
                
            }
        }
    }

    void Update() {

        selectSkill();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, 100);
        if (hit.collider == null)
        {
            hit.point = Camera.main.transform.position + Camera.main.transform.forward * 100f;
        }
        else
        {
            hit.distance = m_camera.farClipPlane;
        }

        Vector3 realDirection;
        realDirection = hit.point - rightHand.position;
        lrMark.SetPosition(0, rightHand.position);
        lrMark.SetPosition(1, hit.point);
        mark.transform.position = hit.point;
       
        if (Input.GetButtonDown(FIRE1)) {
             
            if (skill == HOOK)
            { //Skill ancora
                if (auxGancho == null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    print(hitObject.name);
                    if (hitObject.tag == "Pillar")
                    {
                        auxGancho = Instantiate(hook, transform.position, Quaternion.LookRotation(realDirection)) as GameObject;
                    }
                }
            }
            else if (skill == PILLAR) // Skill contonetes
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.tag == "Tile")
                {
                    TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
                    if (tileGround.pillar == null)
                    {
                        tileGround.insertPillar(pillarToSpawn);
                    }
                }
                if (hitObject.tag == "TopWall")
                {
                    TopWall topWall = hitObject.GetComponentInParent<TopWall>();
                    if (topWall.pillar == null)
                    {
                        topWall.insertPillar(pillarToSpawn);
                    }
                }
            }
            else if (skill == TRAP) // Skill trap
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.tag == "Tile")
                {
                    TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
                    if (tileGround.trap == null)
                    {
                        tileGround.insertTrap(trapSlow);
                    }
                }
            }
            else if(skill == BULLET) // Skill bullet
            {
                GameObject bulletAux = Instantiate(bullet, rightHand.position, Quaternion.LookRotation(realDirection)) as GameObject;
                bulletAux.GetComponent<Rigidbody>().velocity = realDirection * velocityBullet;
            }

        }
        if (Input.GetButtonDown(FIRE2)) {
			print ("FIRE2");
            if (skill == TRAP) // Skill trap
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.tag == "Tile") {
                    TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
                    if (tileGround.trap == null) {
                        tileGround.insertTrap(trapDamage);
                    }
                }
            }
            if (skill == BULLET) // Skill bullet
          {
                GameObject bulletAux = Instantiate(bulletStun, rightHand.position, Quaternion.LookRotation(realDirection)) as GameObject;
                bulletAux.GetComponent<Rigidbody>().velocity = realDirection * velocityBullet;
            }

        }
	}

    private void selectSkill() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            skill = 1;
            for (int i = 0; i < gameObjectsSkill.Length; i++) {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[0].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            skill = 2;
            for (int i = 0; i < gameObjectsSkill.Length; i++) {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[1].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            skill = 3;
            for (int i = 0; i < gameObjectsSkill.Length; i++) {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[2].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            skill = 4;
            for (int i = 0; i < gameObjectsSkill.Length; i++) {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[3].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            skill++;
            if (skill > gameObjectsSkill.Length)
                skill = 1;
            for (int i = 0; i < gameObjectsSkill.Length; i++) {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[skill - 1].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            skill--;
            if (skill == 0)
                skill = gameObjectsSkill.Length;

            for (int i = 0; i < gameObjectsSkill.Length; i++) {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }

            gameObjectsSkill[skill - 1].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
}
