using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxRandom : MonoBehaviour {

    public bool held;
    public bool thrown;
    public List<GameObject> listObj;

  
    public GameObject particule;
    public BoxCollider2D boxColl;
    public LayerMask objects;
    public LayerMask decors;

    // Use this for initialization
    void Start () {
        held = false;
        thrown = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!held)
        {
            collideBox();
        }
    }

    void collideBox()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(boxColl.transform.position, boxColl.size, 0, objects);
        if (cols.Length>1)
        {
            destroyBox();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (thrown)
        {
            destroyBox();
        }
    }
    
    void destroyBox()
    {
        GameObject item = Instantiate(randomListObject());
        Vector3 temp = this.transform.position;
        item.transform.position = temp;

        GameObject particulego = Instantiate(particule);
        temp = this.transform.position;
        particulego.transform.position = temp;

        Destroy(this.gameObject);
    }
    GameObject randomListObject()
    {
        int r = Random.Range(0,listObj.Count) ;

        return listObj[r]; 

    }

}
