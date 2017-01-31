using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lave : MonoBehaviour {

    private bool activation = false ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		 
        if(activation)
        {
            Vector3 temp = this.transform.position;
            temp.y = 0.01f + temp.y;
            this.transform.position = temp;
        }
	}


	public void laveLancement()
	{
        activation = true; 
	}
}
