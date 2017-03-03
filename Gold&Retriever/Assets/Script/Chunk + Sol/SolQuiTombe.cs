using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolQuiTombe : MonoBehaviour {

    [SerializeField]
    private LayerMask player;

    [SerializeField]
    private GameObject box;

    [SerializeField]
    private float pointDeVie = 100; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {

        if (Physics2D.OverlapBox(new Vector2(box.transform.position.x, box.transform.position.y), new Vector2(1, 0.2330246f), 0, player))
        {
            pointDeVie--; 
            if(pointDeVie < 0)
            {
                Destroy(this.gameObject); 
            }
        }
    }
}
