using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeMonterDescente : MonoBehaviour {

    [SerializeField]
    private float distance;
    [SerializeField]
    private float speed;

    private bool hautOui;
    private float xPlatInit;
    private float yPlatInit;
    private float tempy; 
    // Use this for initialization
    void Start () {
        xPlatInit = this.transform.position.x;
        yPlatInit = this.transform.position.y;
        hautOui = false;
        tempy = yPlatInit;

    }

    // Update is called once per frame
    void Update () {

        float yMax = yPlatInit - distance;
        float dt = speed * 0.1f; 
        
        if (tempy > yMax && !hautOui)
        {
            tempy -= dt / 2;
           this.transform.position = new Vector2(xPlatInit, tempy);
        }
        else
            hautOui = true; 
        

        if (yPlatInit > tempy && hautOui)
        {
            tempy += dt;
            this.transform.position = new Vector2(xPlatInit, tempy);
        }
        else
            hautOui = false;
        


    }
}
