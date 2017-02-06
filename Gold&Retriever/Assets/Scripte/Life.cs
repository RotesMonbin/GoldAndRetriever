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

        LifeAff(); 

    }
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public void LifeAff()
	{

		int nblife = managerJoueur.getLife(J1actif) ; 
		int nblifeMax = managerJoueur.getLifeMax(J1actif) ; 

		if (nblife <= -1)
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

                temp.x += pos-1 ;
                LifeGameObj.transform.parent = this.transform;
				LifeGameObj.transform.position = temp; 

				listHearth.Add (LifeGameObj); 
				pos++;
			}


			while (pos != nblifeMax+1) {
				LifeGameObj = Instantiate (gameObjLife_empty); 
				temp = this.transform.position; 

				temp.x += pos - 1; 

				LifeGameObj.transform.parent = this.transform;
				LifeGameObj.transform.position = temp; 

				listHearth.Add (LifeGameObj); 
				pos++;
			} 
		 
		}
	}
}
