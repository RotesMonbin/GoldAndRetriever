using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeMonterDescente : MonoBehaviour {

    [SerializeField]
    private float distance;
    [SerializeField]
    private float speed;

    private bool hautOui;
    private float yPlatInit;
    // Use this for initialization
    void Start () {
        yPlatInit = this.transform.position.y;
        hautOui = false;
    }

    // Update is called once per frame
    void Update () {

        float yMax = yPlatInit + distance;
        if (this.transform.position.y > yMax)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            hautOui = true;
        }
        if (this.transform.position.y <= yPlatInit || !hautOui)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            
        }

    }
}
