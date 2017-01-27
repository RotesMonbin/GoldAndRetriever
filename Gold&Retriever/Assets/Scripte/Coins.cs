using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	private ManagerJoueur manager ;

	void Start () {
		manager = GameObject.Find ("ManagerJoueur").GetComponent<ManagerJoueur> ();
	}

	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, 5.0f, 0)); 
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			manager.GetComponent<ManagerJoueur> ().cashUp (1, true); 
			Destroy (this.gameObject); 
		}

		/*if (coll.gameObject.tag == "Player") {
			manager.GetComponent<ManagerJoueur> ().cashUp (1, false);
			Destroy (this.gameObject); 
		}*/
	}
}
