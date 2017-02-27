using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSimple : MonoBehaviour
{ 
    [Header("Animation")]
    [SerializeField]
    private Animator anime;

    [Header("Associer à l'araigné")]
    [SerializeField]
    private float distance;
    [SerializeField]
    private float speed = 3;

    private float timeActif;
    private bool posbool;
    private Vector3 posInit;

    private float timeDelta = 0;

    // Use this for initialization
    void Start()
    {
        timeActif = 0;
        posInit = this.transform.position;
        posbool = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float dt = 0.1f * speed;
        float posYMax = posInit.y - distance;
        float posYActu = this.transform.position.y;

        if (posYActu > posYMax && !posbool)
        {
            this.transform.position = new Vector2(this.transform.position.x, posYActu - dt);
            timeActif += dt;
        }
        else
        {
            posbool = true;
        }


        if (posbool && posYActu < posInit.y)
        {
            this.transform.position = new Vector2(this.transform.position.x, posYActu + dt);
            timeActif += dt;
        }
        else
        {
            posbool = false;
            }
        
    }

  


    #region dead
    public void dead()
    {
        this.gameObject.layer = 0;
        Destroy(this.gameObject);

    }
    #endregion
}
