using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerTrigger : NetworkBehaviour{

	public float bulletSpeed;

	private Player player;
    private GameObject auxGancho;

    [SerializeField]
    private Transform rightHand;

	private LineRenderer lrMark;
	private GameObject mark;
	private LineRenderer lrGancho;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
	private GameObject bulletStunPrefab;
    [SerializeField]
	private GameObject hookPrefab;
    [SerializeField]
	private GameObject pillarPrefab;
    [SerializeField]
	private GameObject trapSlowPrefab;
    [SerializeField]
	private GameObject trapDamagePrefab;


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
		player = GetComponent<Player>();
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
        if (!isLocalPlayer)
            return;

        selectSkill();

        RaycastHit hit;
        Vector3 realDirection;
        calcateDirection(out hit, out realDirection);
        tryShot(hit, realDirection);
    }

    private void tryShot(RaycastHit hit, Vector3 realDirection) {
        if (Input.GetButtonDown("Fire1")) {
            skillsButtonOne(hit, realDirection);
        } else if (Input.GetButtonDown("Fire2")) {
            if (skill == TRAP) // Skill trap
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.tag == "Tile") {
                    TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
                    if (tileGround.trap == null) {
                        tileGround.insertTrap(trapDamagePrefab);
                    }
                }
            }
            if (skill == BULLET) // Skill bullet
            {
                GameObject bulletAux = Instantiate(bulletStunPrefab, rightHand.position, Quaternion.LookRotation(realDirection)) as GameObject;
                bulletAux.GetComponent<Rigidbody>().velocity = realDirection * bulletSpeed;
            }

        }
    }

    private void skillsButtonOne(RaycastHit hit, Vector3 realDirection) {
        if (skill == HOOK) {
            if (auxGancho == null) {
                GameObject hitObject = hit.collider.gameObject;
                print(hitObject.name);
                //if (hitObject.tag == "Pillar") {
                    auxGancho = Instantiate(hookPrefab, transform.position, Quaternion.LookRotation(realDirection)) as GameObject;
                //}
            }
        }
        else if (skill == PILLAR) // Skill contonetes
        {
            GameObject hitObject = hit.collider.gameObject;
            
            if (hitObject.tag == "Tile") {
                TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
                if (tileGround.pillar == null) {
                    tileGround.insertPillar(pillarPrefab);
                }
            }
            
            if (hitObject.tag == "TopWall") {
                TopWall topWall = hitObject.GetComponentInParent<TopWall>();
                if (topWall.pillar == null) {
                    topWall.insertPillar(pillarPrefab);
                }
            }
        }
        else if (skill == TRAP) // Skill trap
        {
            Cmd_spawnTrapOne(hit);
        } else if (skill == BULLET) // Skill bullet
        {
            Cmd_spawnBulletOne(realDirection);
        }
    }

    [Command]
    private void Cmd_spawnTrapOne(RaycastHit hit) {
        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.tag == "Tile") {
            TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
            if (tileGround.trap == null) {
                tileGround.insertTrap(trapSlowPrefab);
            }
        }
    }

    [Command]
    void Cmd_spawnBulletOne(Vector3 realDirection) {
        GameObject bulletAux = Instantiate(bulletPrefab, rightHand.position, Quaternion.LookRotation(realDirection)) as GameObject;
        bulletAux.GetComponent<Rigidbody>().velocity = realDirection * bulletSpeed;
        NetworkServer.Spawn(bulletAux);
    }

    private void calcateDirection(out RaycastHit hit, out Vector3 realDirection) {
		bool hasHit = Physics.Raycast(player.Cam.transform.position, player.Cam.transform.forward, out hit, 100);
        if (hit.collider == null) {
            hit.point = Camera.main.transform.position + Camera.main.transform.forward * 100f;
        } else {
			hit.distance = player.Cam.farClipPlane;
        }
        realDirection = hit.point - rightHand.position;
        lrMark.SetPosition(0, rightHand.position);
        lrMark.SetPosition(1, hit.point);
        mark.transform.position = hit.point;
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
