﻿using UnityEngine;
using System.Collections;

public class Disparador : MonoBehaviour {

	[Header("Hook Gameobject")]
	public GameObject gancho;
	private GameObject auxGancho;

	public Camera m_camera;
	public Transform m_camera_transform;

	public Transform dirDoClique;
	private Transform auxDirDoClique;

	public float viewRange = 60f;

	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;

	float rotVertical = 0;

    private LineRenderer lr;
    private GameObject mark;


	void Start(){
        lr = GetComponent<LineRenderer>();
        lr.SetWidth(0.05f, 0.05f);
        mark = GameObject.FindGameObjectWithTag("mark");

    }

    // Update is called once per frame
    void Update () {

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
				Ray cameraRay = m_camera.ScreenPointToRay(Input.mousePosition);

				Debug.DrawLine(cameraRay.origin, cameraRay.origin + cameraRay.direction*10);
                RaycastHit hit;
               // RaycastHit nearestHit = new RaycastHit();
                Physics.Raycast(cameraRay, out hit , 100);

                hit.distance = m_camera.far-500;
				//foreach (RaycastHit hit in hits){
//					hit.collider.tag != "Player"
//					if(!nearestHit){/*
/*
						if((hit.distance > hit.distance) && hit.collider.tag != tag)
							nearestHit = hit;
						Debug.Log(nearestHit.transform.name);
					Debug.Log(nearestHit.point);
                    
//					}

				}*/
				Vector3 realDirection;
				realDirection = hit.point - transform.position;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, hit.point);
        mark.transform.position = hit.point;
        //	realDirection = Quaternion.AngleAxis(-5, Vector3.right) * realDirection;

        //				auxDirDoClique = Instantiate(dirDoClique, posMouse, Quaternion.identity) as Transform;
        //				auxDirDoClique = Instantiate(dirDoClique, posMouse, Quaternion.identity) as Transform;
        //				localDoClique = (auxDirDoClique.transform.position - transform.position);
        //				olharParaDir = Quaternion.LookRotation(localDoClique);
        if (auxGancho == null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        auxGancho = Instantiate(gancho, transform.position, Quaternion.LookRotation(realDirection)) as GameObject;
                    }
                }
//				Destroy(auxDirDoClique.gameObject);


//				GameObject projectile = Instantiate(gancho);
////				projectile.transform.position = transform.position+m_camera.transform.forward*2;
//				Rigidbody rb = projectile.GetComponent<Rigidbody>();
//				rb.velocity = posMouse*40;

			
	}
} 