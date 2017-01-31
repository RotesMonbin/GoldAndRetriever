using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lave : MonoBehaviour {

	public GameObject laveGO;

	private List<GameObject> tabLave; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		laveLancement ();
	}


	public void laveLancement()
	{
		GameObject lave = Instantiate (laveGO); 
		Vector3 temp = this.transform.position; 
		temp.x = temp.x + 2;
		lave.transform.position = temp; 

		tabLave.Add (lave); 
	}
}
