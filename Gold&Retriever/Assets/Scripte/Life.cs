using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	private ManagerJoueur managerJoueur ; 
	public bool J1actif; 

	public GameObject gameObjLife ; 

	// Use this for initialization
	void Start () {
		managerJoueur = GameObject.Find ("ManagerJoueur").GetComponent<ManagerJoueur> ();

		int nblife = managerJoueur.getLifeMax(J1actif) ; 
		GameObject LifeGameObj; 
		while (nblife !=  0) {
			LifeGameObj = Instantiate (gameObjLife); 
			Vector3 temp = this.transform.position; 
			temp.x += nblife - managerJoueur.getLifeMax(J1actif) + 2.25f; 
			LifeGameObj.transform.parent = this.transform;
			LifeGameObj.transform.position = temp; 
			nblife--;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
