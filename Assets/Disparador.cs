using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Disparador : MonoBehaviour
{

	[Header ("Hook Gameobject")]
	public GameObject gancho;
	private GameObject auxGancho;
    public GameObject bullet;
    public float velocityBullet;
    public Camera m_camera;
	public Transform m_camera_transform;

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

	public GameObject pillarToSpawn;
	public GameObject trapSlow;

    public Button[] gameObjectsSkill;

    public int skill;
    public int Skill
    {
        get { return Skill; }
    }

    public void alterSkill(int newSkill)
    {
        skill = newSkill;
    }

    // private int shootType = 0;


    void Start(){
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

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skill = 1;
            for (int i = 0;i< gameObjectsSkill.Length; i++){
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[0].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skill = 2;
            for (int i = 0; i < gameObjectsSkill.Length; i++)
            {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[1].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			skill = 3;
            for (int i = 0; i < gameObjectsSkill.Length; i++)
            {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[2].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skill = 4;
            for (int i = 0; i < gameObjectsSkill.Length; i++)
            {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[3].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            skill ++;
            if (skill > gameObjectsSkill.Length)
                skill = 1;
            for (int i = 0; i < gameObjectsSkill.Length; i++)
            {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }
            gameObjectsSkill[skill-1].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            skill--;
            if (skill == 0)
                skill = gameObjectsSkill.Length;

            for (int i = 0; i < gameObjectsSkill.Length; i++)
            {
                gameObjectsSkill[i].gameObject.GetComponent<Button>().interactable = true;
            }

            gameObjectsSkill[skill - 1].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }


		//		float rotHorizontal = Input.GetAxisRaw("Mouse X");
		//
		//		transform.Rotate ( 0, rotHorizontal, 0);
		//
		//
		//		rotVertical = Input.GetAxisRaw("Mouse Y");
		//
		//		rotVertical = Mathf.Clamp ( rotVertical, -viewRange, viewRange);
		//
		//		yaw += speedH * Input.GetAxis("Mouse X");
		//		pitch -= speedV * rotVertical;
		//
		//
		////		m_camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		//		transform.localEulerAngles = new Vector3(-rotVertical, transform.localEulerAngles.y, transform.localEulerAngles.z);



		//		rotationY += Input.GetAxis ("Mouse Y") * Ysensitivity;
		//
		//
		//		rotationY = Mathf.Clamp (rotationY, -15, 15);
		//		transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);

		//		if(m_camera.transform.localEulerAngles.x > viewRange)
		//		{
		//			m_camera_transform.localEulerAngles = new Vector3( viewRange, 0, 0);
		//		} else
		//		{
		//			if(m_camera_transform.localEulerAngles.x < -viewRange)
		//			{
		//				m_camera_transform.localEulerAngles = new Vector3( -viewRange, 0, 0);
		//			}
		//		}
		//
		//
		//		if(m_camera.transform.localEulerAngles.y > viewRange)
		//		{
		//			m_camera_transform.localEulerAngles = new Vector3( 0, viewRange, 0);
		//		} else
		//		{
		//			if(m_camera_transform.localEulerAngles.y < -viewRange)
		//			{
		//				m_camera_transform.localEulerAngles = new Vector3( 0, -viewRange, 0);
		//			}
		//		}


		//		Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
		//		Physics.RaycastAll();


		//				Vector3 posMouse = Input.mousePosition;
		//				posMouse.z = m_camera.nearClipPlane;
		//				Vector3 nearCameraPosition = m_camera.ScreenToWorldPoint(posMouse);
		//				Ray cameraRay = new Ray(m_camera.transform.position, nearCameraPosition - m_camera_transform.position);
		RaycastHit hit;

		// RaycastHit nearestHit = new RaycastHit();
		bool hasHit = Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, 100);
		if (hit.collider == null)
		{
			hit.point = Camera.main.transform.position + Camera.main.transform.forward * 100f;
		}
		else
		{
			hit.distance = m_camera.far;
		}
		//foreach (RaycastHit hit in hits){
		//					hit.collider.tag != "Player"
		//					if(!nearestHit){/*
		/*
=======
        //		float rotHorizontal = Input.GetAxisRaw("Mouse X");
        //
        //		transform.Rotate ( 0, rotHorizontal, 0);
        //
        //
        //		rotVertical = Input.GetAxisRaw("Mouse Y");
        //
        //		rotVertical = Mathf.Clamp ( rotVertical, -viewRange, viewRange);
        //
        //		yaw += speedH * Input.GetAxis("Mouse X");
        //		pitch -= speedV * rotVertical;
        //
        //
        ////		m_camera.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        //		transform.localEulerAngles = new Vector3(-rotVertical, transform.localEulerAngles.y, transform.localEulerAngles.z);



        //		rotationY += Input.GetAxis ("Mouse Y") * Ysensitivity;
        //
        //
        //		rotationY = Mathf.Clamp (rotationY, -15, 15);
        //		transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //		if(m_camera.transform.localEulerAngles.x > viewRange)
        //		{
        //			m_camera_transform.localEulerAngles = new Vector3( viewRange, 0, 0);
        //		} else
        //		{
        //			if(m_camera_transform.localEulerAngles.x < -viewRange)
        //			{
        //				m_camera_transform.localEulerAngles = new Vector3( -viewRange, 0, 0);
        //			}
        //		}
        //
        //
        //		if(m_camera.transform.localEulerAngles.y > viewRange)
        //		{
        //			m_camera_transform.localEulerAngles = new Vector3( 0, viewRange, 0);
        //		} else
        //		{
        //			if(m_camera_transform.localEulerAngles.y < -viewRange)
        //			{
        //				m_camera_transform.localEulerAngles = new Vector3( 0, -viewRange, 0);
        //			}
        //		}


        //		Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.red);
        //		Physics.RaycastAll();


        //				Vector3 posMouse = Input.mousePosition;
        //				posMouse.z = m_camera.nearClipPlane;
        //				Vector3 nearCameraPosition = m_camera.ScreenToWorldPoint(posMouse);
        //				Ray cameraRay = new Ray(m_camera.transform.position, nearCameraPosition - m_camera_transform.position);
        //Ray cameraRay = m_camera.ScreenPointToRay(m_camera.transform.forward);

        //Debug.DrawLine(cameraRay.origin, cameraRay.origin + cameraRay.direction * 10);
        RaycastHit hit;

        // RaycastHit nearestHit = new RaycastHit();
        bool hasHit = Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hit, 100);
        if (hit.collider == null)
        {
            hit.point = Camera.main.transform.position + Camera.main.transform.forward * 100f;
        }
        else
        {
            hit.distance = m_camera.far;
        }
        //foreach (RaycastHit hit in hits){
        //					hit.collider.tag != "Player"
        //					if(!nearestHit){/*
        /*
>>>>>>> 5a27718d54c5a38f51d4ba109827ec2995c08f6d
                                if((hit.distance > hit.distance) && hit.collider.tag != tag)
                                    nearestHit = hit;
                                Debug.Log(nearestHit.transform.name);
                            Debug.Log(nearestHit.point);

        //					}

                        }*/

        Vector3 realDirection;
        realDirection = hit.point - rightHand.position;
        lrMark.SetPosition(0, rightHand.position);
        lrMark.SetPosition(1, hit.point);
        mark.transform.position = hit.point;
        //	realDirection = Quaternion.AngleAxis(-5, Vector3.right) * realDirection;

        //				auxDirDoClique = Instantiate(dirDoClique, posMouse, Quaternion.identity) as Transform;
        //				auxDirDoClique = Instantiate(dirDoClique, posMouse, Quaternion.identity) as Transform;
        //				localDoClique = (auxDirDoClique.transform.position - transform.position);
        //				olharParaDir = Quaternion.LookRotation(localDoClique);
        if (Input.GetMouseButtonDown(0)) 
            {
            if (skill == 1)
            { //Skill ancora

                if (auxGancho == null)
                {   
                    GameObject hitObject = hit.collider.gameObject;
                    print(hitObject.name);
                    if (hitObject.tag == "Pillar")
                    {
                        auxGancho = Instantiate(gancho, transform.position, Quaternion.LookRotation(realDirection)) as GameObject;
                    }
                }
            }
            else if (skill == 2) // Skill contonetes
            {
                GameObject hitObject = hit.collider.gameObject;
                print(hitObject.name);
                if (hitObject.tag == "Tile")
                {
                    TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
                    if (tileGround.pillar == null)
                    {
                        tileGround.insertPillar(pillarToSpawn);
                    }
                }
					if(hitObject.tag == "TopWall"){
						print("Hitou");
						TopWall topWall =  hitObject.GetComponentInParent<TopWall> ();
						if (topWall.pillar == null) {
							topWall.insertPillar (pillarToSpawn);
						}
					}
            }
            else if (skill == 3) // Skill trap
            {
                GameObject hitObject = hit.collider.gameObject;
                print(hitObject.name);
                if (hitObject.tag == "Tile")
                {
                    TileGround tileGround = hitObject.GetComponentInParent<TileGround>();
                    if (tileGround.trap == null)
                    {
                        tileGround.insertTrap(trapSlow);
                    }
                }
            }else if (skill == 4) // Skill bullet
                {
                GameObject bulletAux=Instantiate(bullet, rightHand.position, Quaternion.LookRotation(realDirection)) as GameObject;
                bulletAux.GetComponent<Rigidbody>().velocity = realDirection * velocityBullet;
                }
            }
//				Destroy(auxDirDoClique.gameObject);


//				GameObject projectile = Instantiate(gancho);
////				projectile.transform.position = transform.position+m_camera.transform.forward*2;
//				Rigidbody rb = projectile.GetComponent<Rigidbody>();
//				rb.velocity = posMouse*40;

			
		}
	}
