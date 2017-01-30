using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	private ManagerJoueur managerJoueur ; 
	public bool J1actif; 

	public GameObject gameObjLife_full ; 
	public GameObject gameObjLife_empty ; 

	private List<GameObject> listHearth ; 

	// Use this for initialization
	void Start () {
		listHearth = new List<GameObject>() ; 
		managerJoueur = GameObject.Find ("ManagerJoueur").GetComponent<ManagerJoueur> ();

		int nblife = managerJoueur.getLifeMax(J1actif) ; 
		GameObject LifeGameObj; 
		while (nblife !=  0) {
			LifeGameObj = Instantiate (gameObjLife_full); 
			Vector3 temp = this.transform.position; 
			temp.x += nblife - managerJoueur.getLifeMax(J1actif) + 2.25f; 
			LifeGameObj.transform.parent = this.transform;
			LifeGameObj.transform.position = temp; 

			listHearth.Add (LifeGameObj); 
			nblife--;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public void LifeAff()
	{

		int nblife = managerJoueur.getLife(J1actif) ; 
		int nblifeMax = managerJoueur.getLifeMax(J1actif) ; 

		if (nblife <= 0)
			return;
		else {
			for (int i = 0; i < listHearth.Count; i++)
				Destroy (listHearth [i]); 
		
			listHearth = new List<GameObject> (); 
			managerJoueur = GameObject.Find ("ManagerJoueur").GetComponent<ManagerJoueur> ();


			GameObject LifeGameObj; 
			Vector3 temp; 

			int pos = 1; 

			while (pos != nblife+1) {
				LifeGameObj = Instantiate (gameObjLife_full); 
				temp = this.transform.position; 

				temp.x += pos - managerJoueur.getLifeMax (J1actif) + 2.25f; 

				LifeGameObj.transform.parent = this.transform;
				LifeGameObj.transform.position = temp; 

				listHearth.Add (LifeGameObj); 
				pos++;
			}


			while (pos != nblifeMax+1) {
				LifeGameObj = Instantiate (gameObjLife_empty); 
				temp = this.transform.position; 

				temp.x += pos - managerJoueur.getLifeMax (J1actif) + 2.25f; 

				LifeGameObj.transform.parent = this.transform;
				LifeGameObj.transform.position = temp; 

				listHearth.Add (LifeGameObj); 
				pos++;
			} 
		 
		}
	}
}
