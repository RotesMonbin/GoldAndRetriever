using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemmeBleuHUD : MonoBehaviour {

    public GameObject gameObjgem_full;
    public GameObject gameObjgem_empty;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
       
    }

    public void gemaff()
    {
        GameObject g =  Instantiate(gameObjgem_full);

        Vector3 temp = this.transform.position;
        g.transform.parent = this.transform;
        g.transform.position = temp;
    }
}
