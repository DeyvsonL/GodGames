﻿using UnityEngine;
using System.Collections;

public class PlayerHook : MonoBehaviour {

	public float velLançar;
	public float tamanhoCorda;
	public float forçaCorda;
	public float peso;

	private GameObject player;
	private Rigidbody corpoRigido;
	private SpringJoint efeitoCorda;

	private float distanciaDoPlayer;

	private bool atirarCorda;
	public static bool cordaColidiu;
    private LineRenderer lrCorda;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		corpoRigido = GetComponent<Rigidbody>();
		efeitoCorda = player.GetComponent<SpringJoint>();

		atirarCorda = true;
		cordaColidiu = false;

        lrCorda = GetComponent<LineRenderer>();
        lrCorda.SetWidth(0.05f, 0.05f);
        lrCorda.SetColors(Color.blue, Color.blue);

    }

    // Update is called once per frame
    void Update () {
		distanciaDoPlayer = Vector3.Distance(transform.position, player.transform.position);

		if(Input.GetMouseButtonDown(0)){
			atirarCorda = false;
		}

        if (atirarCorda) {
            AtirarGancho();
        } else {
            RecolherGancho();
        }

        lrCorda.SetPosition(0, transform.position);
        lrCorda.SetPosition(1, player.transform.position);

    }
	void OnTriggerEnter(Collider coll){
		if(coll.tag != "Player" && coll.name != "Platform" && coll.name != "Floor"){
			Debug.Log(coll.name);
			cordaColidiu = true;
		}
	}

	public void AtirarGancho(){
		if(distanciaDoPlayer <= tamanhoCorda){
			if(!cordaColidiu){
				transform.Translate(0, 0, velLançar*Time.deltaTime);
			}

			else{
				efeitoCorda.connectedBody = corpoRigido;
				efeitoCorda.spring = forçaCorda;
				efeitoCorda.damper = peso;
			}
		}

		if(distanciaDoPlayer > tamanhoCorda){
			atirarCorda = false;
		}
	}

	public void RecolherGancho(){
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 25*Time.deltaTime);
		cordaColidiu = false;

		if(distanciaDoPlayer <= 2){
			Destroy(gameObject);
		}
	}
} 