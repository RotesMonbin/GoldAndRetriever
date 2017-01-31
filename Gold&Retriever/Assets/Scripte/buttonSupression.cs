using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSupression : MonoBehaviour {


    public List<GameObject> listGameObject;
    public Animator animator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            animator.SetBool("ON", true); 
            foreach(GameObject i in listGameObject)
            {
                Destroy(i); 
            }

        }
    }
}

