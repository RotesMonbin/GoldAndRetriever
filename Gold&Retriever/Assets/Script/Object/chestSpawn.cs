using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chestSpawn : MonoBehaviour
{
    public List<GameObject> objlist;
    int randomNbTaper;

    [SerializeField]
    private GameObject parent; 

    // Use this for initialization
    void Start()
    {
        randomNbTaper = Random.Range(1,3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.tag == "Player")
        {
            
            if(randomNbTaper != 0)
            {
                int r2 = Random.Range(5, 10);
                for (int i = 0; i < r2; i++)
                {
                    GameObject gameobjTemp = Instantiate(objlist[0]);
                   
                    int r = Random.Range(-3, 3);
                    gameobjTemp.GetComponent<Rigidbody2D>().velocity = new Vector2(r, 5.0f);
                    Vector3 temp = this.transform.position;
                    temp.y = temp.y + 1;
                    gameobjTemp.transform.position = temp;
                }
                randomNbTaper--;
            }else
            {
                Destroy(parent.gameObject); 
                Destroy(this.gameObject);
            }
          
          

        }

    }

}