using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolBrulant : MonoBehaviour {

    private ManagerJoueur playerManager;

    [SerializeField]
    private float tempsHit = 1;
    private float timehit = 0;


    // Use this for initialization
    void Awake () {
        playerManager = GameObject.Find("ManagerJoueur").GetComponent<ManagerJoueur>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
    }


    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            timehit += Time.deltaTime; 

            if(timehit > tempsHit)
            {
                timehit = 0;
                playerManager.lifeDown(1, (coll.gameObject.GetComponent<PlayerNoRb>().J1actif));
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            timehit = 0; 
        }
    }
}
