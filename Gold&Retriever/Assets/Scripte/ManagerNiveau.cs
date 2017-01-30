using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNiveau : MonoBehaviour {


	// Tableau de prefable Milieu de map : 
	public GameObject[] TabMilieu; 

	// Tableau de prefable coin de map : 
	public GameObject[] TabCoin;  
	// Player 1 
	public GameObject Player1;  
	public GameObject Player2; 

	// Use this for initialization
	void Start () {
		//Instantiate (Player1).transform.position = new Vector3(0,6,0); 
		//Instantiate (Player2).transform.position = new Vector3(0,6,0); 

		Instantiate (TabMilieu [0]).transform.position = new Vector3(0,1,0); 
		Instantiate (TabMilieu [1]).transform.position = new Vector3(40,1,0); 

		Instantiate (TabMilieu [1]).transform.position = new Vector3(40,-20,0); 

		GameObject i3 = Instantiate (TabCoin [0]); 

		Vector3 temp = i3.transform.localScale; 
		temp.x = -1; 
		i3.transform.localScale = temp; 
	
		i3.transform.position = new Vector3(100,1,0); 

	
		//Instantiate (TabMilieu [1]).transform.position = new Vector3(40,-20,0); 
	
		Instantiate (TabCoin [0]).transform.position = new Vector3(20,-20,0); 

	}
	
	// Update is called once per frame
	void Update () {
	}
}
