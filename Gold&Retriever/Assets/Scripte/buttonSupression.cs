using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSupression : MonoBehaviour {


    public List<GameObject> listGameObject;
    public Animator animator;

    [SerializeField]
    private int type = 0; // 0 bouton normal , 1 bouton double 

    [SerializeField]
    private BoutonDouble pereBoutonDouble = null;

    [SerializeField]
    private int boutonNum = 0;

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
            if (type == 0)
            {
                animator.SetBool("ON", true);
                foreach (GameObject i in listGameObject)
                {
                    Destroy(i);
                }
            }
            
        }
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (type == 0)
            {
                animator.SetBool("ON", true);
                foreach (GameObject i in listGameObject)
                {
                    Destroy(i);
                }
            }

            if (type == 1)
            {
                animator.SetBool("ON", true);
                if (boutonNum == 0)
                    pereBoutonDouble.setActif1();

                else if (boutonNum == 1)
                    pereBoutonDouble.setActif2();

            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (type == 1)
            {
                animator.SetBool("OFF", true);
                if (boutonNum == 0)
                    pereBoutonDouble.setDesActif1();

                else if (boutonNum == 1)
                    pereBoutonDouble.setDesActif2();
            }
        }
    }


}

