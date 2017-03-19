using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargementArrow : MonoBehaviour {

    [SerializeField]
    private bool actif = false;

    private float timeMax = 3 ; 
	// Use this for initialization
	void Start () {
        this.transform.localScale = new Vector3(1, 0, 1);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(actif)
        {
            if (this.transform.localScale.y > 0.99f)
            {
                actif = false; 
            }
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(1, 1, 1), Time.deltaTime); 
            //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + (timeMax * 0.025f) / 0.5f);
        }
	}

    public void lancementChargement(float timeMax_)
    {
        timeMax = timeMax_; 
        this.transform.localScale = new Vector3(1, 0, 1);
        actif = true; 
    }
}
