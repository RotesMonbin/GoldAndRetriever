using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargementArrow : MonoBehaviour {

    [SerializeField]
    private bool actif = false;

    private float timeMax = 10 ;
    private float t = 0;
	// Use this for initialization
	void Start () {
        this.transform.localScale = new Vector3(1, 0, 1);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(actif)
        {
            t += Time.deltaTime / timeMax;
            this.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(1, 0, 1), t);
            if (this.transform.localScale.y < 0.01f)
            {
                actif = false;
                t = 0;
            }
            //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + (timeMax * 0.025f) / 0.5f);
        }
	}

    public void lancementChargement(float timeMax_)
    {
        timeMax = timeMax_; 
        this.transform.localScale = new Vector3(1, 1, 1);
        actif = true; 
    }
}
