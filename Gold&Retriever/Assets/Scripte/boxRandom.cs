using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxRandom : MonoBehaviour {

    public bool jeter;

    public List<GameObject> listObj;

  
    public GameObject particule;
    // Use this for initialization
    void Start () {
        jeter = false; 

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Pickup" )
        {
           
            GameObject item = Instantiate(randomListObject());
            Vector3 temp = this.transform.position ;
            item.transform.position = temp;

            GameObject particulego = Instantiate(particule);
            temp = this.transform.position;
            particulego.transform.position = temp;

            Destroy(this.gameObject);
        }
    }

    GameObject randomListObject()
    {
        int r = Random.Range(0,listObj.Count) ;

        return listObj[r]; 

    }

}
