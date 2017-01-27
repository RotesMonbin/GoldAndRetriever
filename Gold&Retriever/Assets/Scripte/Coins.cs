using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (0, 5.0f, 0)); 
	}

}
